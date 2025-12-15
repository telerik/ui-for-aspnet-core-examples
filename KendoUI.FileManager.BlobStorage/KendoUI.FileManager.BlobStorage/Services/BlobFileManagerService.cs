using System.Text;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using KendoUI.FileManager.BlobStorage.Models;
using static KendoUI.FileManager.BlobStorage.Helpers.BlobPathHelper;
using static KendoUI.FileManager.BlobStorage.Models.FileManagerEntryFactory;

namespace KendoUI.FileManager.BlobStorage.Services
{
    public sealed class BlobFileManagerService : IBlobFileManagerService
    {
        private const string PlaceholderFileName = "Somerandomfile.scg";

        private readonly BlobServiceClient _blobServiceClient;
        private readonly ILogger<BlobFileManagerService> _logger;
        private readonly string _containerName;

        public BlobFileManagerService(BlobServiceClient blobServiceClient, IConfiguration configuration, ILogger<BlobFileManagerService> logger)
        {
            _blobServiceClient = blobServiceClient;
            _logger = logger;
            var containerName = configuration["BlobStorage:ContainerName"];
            if (string.IsNullOrWhiteSpace(containerName))
            {
                throw new InvalidOperationException("Blob container name must be configured under 'BlobStorage:ContainerName'.");
            }

            _containerName = containerName.Trim();
        }

        public async Task<IEnumerable<FileManagerEntry>> ReadAsync(string? target)
        {
            var containerClient = await GetOrCreateContainerAsync();
            var prefix = BuildPrefix(target);
            return await BuildDirectoryListingAsync(containerClient, prefix);
        }

        public async Task<FileManagerEntry> CreateAsync(string? target, string name, int entry, FileManagerCreateContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var containerClient = await GetOrCreateContainerAsync();

            if (context.UploadedFile is { Length: > 0 })
            {
                return await HandleFileCreationAsync(containerClient, target, name, context.UploadedFile);
            }

            if (!string.IsNullOrWhiteSpace(context.SourcePath))
            {
                var fileExtension = context.Extension ?? Path.GetExtension(name) ?? string.Empty;
                return await HandleFileCopyAsync(containerClient, context.SourcePath!, target, name, fileExtension);
            }

            if (ShouldTreatAsDirectory(name, context.IsDirectoryFlag, entry))
            {
                return await HandleFolderCreationAsync(containerClient, target, name);
            }

            return await HandleMissingSourceFileAsync(containerClient, target, name);
        }

        public async Task<FileManagerEntry> UploadAsync(string? target, IFormFile file)
        {
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            var containerClient = await GetOrCreateContainerAsync();
            var targetPath = BuildPrefix(target);
            var fileName = file.FileName;
            var uniqueFileName = await GenerateUniqueFileNameAsync(containerClient, targetPath, fileName);
            var blobPath = string.IsNullOrEmpty(targetPath) ? uniqueFileName : targetPath + uniqueFileName;
            var blobClient = containerClient.GetBlobClient(blobPath);

            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, overwrite: false);

            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(uniqueFileName) ?? string.Empty;
            var fileExtension = Path.GetExtension(uniqueFileName) ?? string.Empty;
            var now = DateTimeOffset.UtcNow;

            return Create(
                fileNameWithoutExtension,
                false,
                false,
                blobPath,
                fileExtension,
                file.Length,
                now,
                now);
        }

        public async Task<FileManagerEntry> UpdateAsync(string targetPath, string newName)
        {
            if (string.IsNullOrWhiteSpace(targetPath) || string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException("Path and name are required for rename operation");
            }

            var containerClient = await GetOrCreateContainerAsync();

            targetPath = targetPath.Trim('/');
            var isDirectory = await IsDirectoryAsync(containerClient, targetPath);
            var newPath = BuildNewPath(targetPath, newName, isDirectory);

            if (targetPath.Equals(newPath, StringComparison.OrdinalIgnoreCase))
            {
                return await BuildExistingEntryAsync(containerClient, targetPath, isDirectory);
            }

            // Azure Blob Storage doesn't support rename - must copy to new path then delete original
            return isDirectory
                ? await RenameDirectoryAsync(containerClient, targetPath, newPath, newName)
                : await RenameFileAsync(containerClient, targetPath, newPath);
        }

        public async Task DeleteAsync(string targetPath)
        {
            if (string.IsNullOrWhiteSpace(targetPath))
            {
                throw new ArgumentException("No target path provided for deletion", nameof(targetPath));
            }

            var containerClient = await GetOrCreateContainerAsync();
            var isDirectory = await IsDirectoryAsync(containerClient, targetPath);

            // For folders, delete all blobs with matching prefix (recursive delete)
            if (isDirectory)
            {
                var folderPrefix = EnsureTrailingSlash(targetPath);
                var blobsToDelete = new List<string>();
                await foreach (var blobItem in containerClient.GetBlobsAsync(prefix: folderPrefix))
                {
                    blobsToDelete.Add(blobItem.Name);
                }

                foreach (var blobName in blobsToDelete)
                {
                    var blobClient = containerClient.GetBlobClient(blobName);
                    await blobClient.DeleteIfExistsAsync();
                }

                return;
            }

            var fileClient = containerClient.GetBlobClient(targetPath);
            var deleteResult = await fileClient.DeleteIfExistsAsync();
            if (!deleteResult)
            {
                throw new InvalidOperationException($"File not found: {targetPath}");
            }
        }

        private async Task<FileManagerEntry> HandleFileCreationAsync(BlobContainerClient containerClient, string? target, string name, IFormFile file)
        {
            var fileName = string.IsNullOrEmpty(name) ? file.FileName : name;
            var targetPath = CombinePath(target, fileName);
            var uniqueFileName = await GenerateUniqueFileNameAsync(containerClient, target, fileName);

            if (!string.Equals(uniqueFileName, fileName, StringComparison.Ordinal))
            {
                targetPath = CombinePath(target, uniqueFileName);
                fileName = uniqueFileName;
            }

            var blobClient = containerClient.GetBlobClient(targetPath);
            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, overwrite: false);

            var properties = await blobClient.GetPropertiesAsync();
            var fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName) ?? string.Empty;
            var fileExt = Path.GetExtension(fileName) ?? string.Empty;

            return Create(
                fileNameWithoutExt,
                false,
                false,
                targetPath,
                fileExt,
                file.Length,
                properties.Value.CreatedOn,
                properties.Value.LastModified);
        }

        private async Task<FileManagerEntry> HandleFileCopyAsync(BlobContainerClient containerClient, string sourcePath, string? target, string name, string fileExtension)
        {
            var fullFileName = name + fileExtension;
            var targetPath = CombinePath(target, fullFileName);
            var sourceBlobClient = containerClient.GetBlobClient(sourcePath);

            if (!await sourceBlobClient.ExistsAsync())
            {
                throw new InvalidOperationException($"Source file not found: {sourcePath}");
            }

            var targetBlobClient = containerClient.GetBlobClient(targetPath);
            if (await targetBlobClient.ExistsAsync())
            {
                var uniqueName = await GenerateUniqueFileNameAsync(containerClient, target, fullFileName);
                targetPath = CombinePath(target, uniqueName);
                targetBlobClient = containerClient.GetBlobClient(targetPath);
                fullFileName = uniqueName;
                name = Path.GetFileNameWithoutExtension(uniqueName);
                fileExtension = Path.GetExtension(uniqueName) ?? fileExtension;
            }

            var sourceProperties = await sourceBlobClient.GetPropertiesAsync();
            // Copy blob using Azure's server-side copy operation (efficient, no download/upload)
            var copyOperation = await targetBlobClient.StartCopyFromUriAsync(sourceBlobClient.Uri);
            await copyOperation.WaitForCompletionAsync();

            if (!await targetBlobClient.ExistsAsync())
            {
                throw new InvalidOperationException($"Failed to copy file to: {targetPath}");
            }

            var newProperties = await targetBlobClient.GetPropertiesAsync();
            return Create(
                name,
                false,
                false,
                targetPath,
                fileExtension,
                newProperties.Value.ContentLength,
                sourceProperties.Value.CreatedOn,
                newProperties.Value.LastModified);
        }

        private async Task<FileManagerEntry> HandleMissingSourceFileAsync(BlobContainerClient containerClient, string? target, string name)
        {
            var targetPath = CombinePath(target, name);
            var extension = Path.GetExtension(name) ?? string.Empty;
            return Placeholder(name, targetPath, extension);
        }

        private async Task<FileManagerEntry> HandleFolderCreationAsync(BlobContainerClient containerClient, string? target, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidOperationException("Folder name cannot be empty");
            }

            var uniqueName = await GenerateUniqueFolderNameAsync(containerClient, target, name);
            var folderPath = CombinePath(target, uniqueName) + "/";
            var success = await CreateFolderBlobAsync(containerClient, folderPath);

            if (!success)
            {
                throw new InvalidOperationException($"Failed to create folder '{uniqueName}'");
            }

            return Create(
                uniqueName,
                true,
                false,
                folderPath,
                string.Empty,
                0,
                DateTimeOffset.UtcNow,
                DateTimeOffset.UtcNow);
        }

        private async Task<FileManagerEntry> BuildExistingEntryAsync(BlobContainerClient containerClient, string path, bool isDirectory)
        {
            if (isDirectory)
            {
                var folderPath = EnsureTrailingSlash(path);
                var folderClient = containerClient.GetBlobClient(folderPath);
                BlobProperties? properties = null;

                try
                {
                    if (await folderClient.ExistsAsync())
                    {
                        properties = (await folderClient.GetPropertiesAsync()).Value;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to load folder metadata for {Path}", folderPath);
                }

                return Create(
                    Path.GetFileName(path),
                    true,
                    false,
                    path,
                    string.Empty,
                    0,
                    properties?.CreatedOn,
                    properties?.LastModified);
            }

            var blobClient = containerClient.GetBlobClient(path);
            var blobProperties = await blobClient.GetPropertiesAsync();
            var fileName = Path.GetFileName(path) ?? string.Empty;
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName) ?? string.Empty;
            var extension = Path.GetExtension(fileName) ?? string.Empty;

            return Create(
                fileNameWithoutExtension,
                false,
                false,
                path,
                extension,
                blobProperties.Value.ContentLength,
                blobProperties.Value.CreatedOn,
                blobProperties.Value.LastModified);
        }

        private async Task<FileManagerEntry> RenameDirectoryAsync(BlobContainerClient containerClient, string targetPath, string newPath, string newName)
        {
            var folderPrefix = EnsureTrailingSlash(targetPath);
            var newFolderPath = EnsureTrailingSlash(newPath);
            BlobProperties? folderMarkerProperties = null;

            try
            {
                var folderMarkerClient = containerClient.GetBlobClient(folderPrefix);
                if (await folderMarkerClient.ExistsAsync())
                {
                    folderMarkerProperties = (await folderMarkerClient.GetPropertiesAsync()).Value;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to retrieve folder metadata during rename for {Path}", targetPath);
            }

            await foreach (var blobItem in containerClient.GetBlobsAsync(prefix: folderPrefix))
            {
                var oldBlobClient = containerClient.GetBlobClient(blobItem.Name);
                var relativePath = blobItem.Name.Length > folderPrefix.Length
                    ? blobItem.Name.Substring(folderPrefix.Length)
                    : string.Empty;

                var newBlobPath = string.IsNullOrEmpty(relativePath)
                    ? newFolderPath
                    : newFolderPath + relativePath;

                var newBlobClient = containerClient.GetBlobClient(newBlobPath);
                var copyOperation = await newBlobClient.StartCopyFromUriAsync(oldBlobClient.Uri);
                await copyOperation.WaitForCompletionAsync();

                if (await newBlobClient.ExistsAsync())
                {
                    await oldBlobClient.DeleteIfExistsAsync();
                }
            }

            var now = DateTime.UtcNow;
            return Create(
                newName,
                true,
                false,
                newPath,
                string.Empty,
                0,
                folderMarkerProperties?.CreatedOn ?? now,
                now);
        }

        private async Task<FileManagerEntry> RenameFileAsync(BlobContainerClient containerClient, string targetPath, string newPath)
        {
            var oldBlobClient = containerClient.GetBlobClient(targetPath);
            if (!await oldBlobClient.ExistsAsync())
            {
                throw new InvalidOperationException($"Source file not found: {targetPath}");
            }

            var newBlobClient = containerClient.GetBlobClient(newPath);
            var originalProperties = await oldBlobClient.GetPropertiesAsync();
            var copyOperation = await newBlobClient.StartCopyFromUriAsync(oldBlobClient.Uri);
            await copyOperation.WaitForCompletionAsync();

            if (!await newBlobClient.ExistsAsync())
            {
                throw new InvalidOperationException($"Failed to rename file to: {newPath}");
            }

            await oldBlobClient.DeleteIfExistsAsync();
            var newProperties = await newBlobClient.GetPropertiesAsync();
            var resultFileName = Path.GetFileName(newPath) ?? string.Empty;
            var resultFileNameWithoutExt = Path.GetFileNameWithoutExtension(resultFileName) ?? string.Empty;
            var resultExtension = Path.GetExtension(newPath) ?? string.Empty;

            return Create(
                resultFileNameWithoutExt,
                false,
                false,
                newPath,
                resultExtension,
                newProperties.Value.ContentLength,
                originalProperties.Value.CreatedOn,
                DateTimeOffset.UtcNow);
        }

        private async Task<BlobContainerClient> GetOrCreateContainerAsync()
        {
            // Get reference to blob container and create it if it doesn't exist
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.None);
            return containerClient;
        }

        private static string BuildPrefix(string? target)
        {
            return string.IsNullOrWhiteSpace(target)
                ? string.Empty
                : target.TrimEnd('/') + "/";
        }

        private static string CombinePath(string? target, string name)
        {
            var prefix = BuildPrefix(target);
            return string.IsNullOrEmpty(prefix) ? name : prefix + name;
        }

        private static string EnsureTrailingSlash(string? path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path ?? string.Empty;
            }

            return path.EndsWith('/') ? path : path + "/";
        }

        private static DateTime ResolveLocalDate(DateTimeOffset? timestamp)
        {
            return timestamp?.LocalDateTime ?? DateTime.Now;
        }

        private static DateTime ResolveUtcDate(DateTimeOffset? timestamp)
        {
            return timestamp?.UtcDateTime ?? DateTime.UtcNow;
        }

        private async Task<List<FileManagerEntry>> BuildDirectoryListingAsync(BlobContainerClient containerClient, string prefix)
        {
            var files = new List<FileManagerEntry>();
            var processedFolders = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            await foreach (var blobItem in containerClient.GetBlobsAsync(prefix: prefix))
            {
                var blobName = blobItem.Name;
                var relativePath = prefix.Length >= blobName.Length ? string.Empty : blobName.Substring(prefix.Length);

                if (string.IsNullOrEmpty(relativePath))
                {
                    continue;
                }

                if (relativePath.Contains('/'))
                {
                    var slashIndex = relativePath.IndexOf('/') ;
                    var folderName = relativePath.Substring(0, slashIndex);
                    var folderPath = prefix + folderName + "/";

                    if (processedFolders.Add(folderName))
                    {
                        var hasSubdirectories = await HasSubdirectoriesAsync(containerClient, folderPath);
                        files.Add(Create(folderName, true, hasSubdirectories, folderPath, string.Empty, 0, blobItem.Properties.CreatedOn, blobItem.Properties.LastModified));
                    }

                    continue;
                }

                if (relativePath.EndsWith('/'))
                {
                    var folderName = relativePath.TrimEnd('/');
                    var folderPath = EnsureTrailingSlash(blobName);

                    if (processedFolders.Add(folderName))
                    {
                        var hasSubdirectories = await HasSubdirectoriesAsync(containerClient, folderPath);
                        files.Add(Create(folderName, true, hasSubdirectories, folderPath, string.Empty, 0, blobItem.Properties.CreatedOn, blobItem.Properties.LastModified));
                    }

                    continue;
                }

                if (relativePath.Equals(PlaceholderFileName, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var fileNameWithoutExt = Path.GetFileNameWithoutExtension(relativePath) ?? string.Empty;
                var fileExt = Path.GetExtension(relativePath) ?? string.Empty;
                files.Add(Create(fileNameWithoutExt, false, false, blobName, fileExt, blobItem.Properties.ContentLength ?? 0, blobItem.Properties.CreatedOn, blobItem.Properties.LastModified));
            }

            return files;
        }

        private async Task<string> GenerateUniqueFileNameAsync(BlobContainerClient containerClient, string? target, string baseFileName)
        {
            var prefix = BuildPrefix(target);
            var filePath = prefix + baseFileName;
            var blobClient = containerClient.GetBlobClient(filePath);

            if (!await blobClient.ExistsAsync())
            {
                return baseFileName;
            }

            var nameWithoutExtension = Path.GetFileNameWithoutExtension(baseFileName);
            var extension = Path.GetExtension(baseFileName);
            var counter = 1;

            string uniqueName;
            do
            {
                uniqueName = $"{nameWithoutExtension} ({counter}){extension}";
                filePath = prefix + uniqueName;
                blobClient = containerClient.GetBlobClient(filePath);
                counter++;
            } while (await blobClient.ExistsAsync());

            return uniqueName;
        }

        private async Task<string> GenerateUniqueFolderNameAsync(BlobContainerClient containerClient, string? target, string baseName)
        {
            var prefix = BuildPrefix(target);
            var folderPath = prefix + baseName + "/";
            var hardcodedFilePath = folderPath + PlaceholderFileName;
            var blobClient = containerClient.GetBlobClient(hardcodedFilePath);

            if (!await blobClient.ExistsAsync())
            {
                return baseName;
            }

            var counter = 1;
            string uniqueName;
            do
            {
                uniqueName = $"{baseName} ({counter})";
                folderPath = prefix + uniqueName + "/";
                hardcodedFilePath = folderPath + PlaceholderFileName;
                blobClient = containerClient.GetBlobClient(hardcodedFilePath);
                counter++;
            } while (await blobClient.ExistsAsync());

            return uniqueName;
        }

        private async Task<bool> CreateFolderBlobAsync(BlobContainerClient containerClient, string folderPath)
        {
            try
            {
                var normalizedPath = EnsureTrailingSlash(folderPath);
                var hardcodedFilePath = normalizedPath + PlaceholderFileName;
                var hardcodedFileClient = containerClient.GetBlobClient(hardcodedFilePath);

                if (await hardcodedFileClient.ExistsAsync())
                {
                    return true;
                }

                var hardcodedFileContent = Encoding.UTF8.GetBytes("This is a placeholder file to ensure proper folder structure in Azure Blob Storage.");
                using var hardcodedFileStream = new MemoryStream(hardcodedFileContent);
                await hardcodedFileClient.UploadAsync(hardcodedFileStream, overwrite: false);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create folder blob at {FolderPath}", folderPath);
                return false;
            }
        }

        private async Task<bool> IsDirectoryAsync(BlobContainerClient containerClient, string path)
        {
            if (path.EndsWith('/'))
            {
                return true;
            }

            var blobClient = containerClient.GetBlobClient(path);
            if (await blobClient.ExistsAsync())
            {
                return false;
            }

            var folderMarkerClient = containerClient.GetBlobClient(path + "/");
            if (await folderMarkerClient.ExistsAsync())
            {
                return true;
            }

            var prefix = path + "/";
            await foreach (var _ in containerClient.GetBlobsAsync(prefix: prefix))
            {
                return true;
            }

            return !HasExtension(path);
        }

        private async Task<bool> HasSubdirectoriesAsync(BlobContainerClient containerClient, string folderPath)
        {
            try
            {
                var prefix = EnsureTrailingSlash(folderPath);
                await foreach (var blobItem in containerClient.GetBlobsAsync(prefix: prefix))
                {
                    var blobName = blobItem.Name;
                    var relativePath = blobName.Substring(prefix.Length);

                    if (string.IsNullOrEmpty(relativePath))
                    {
                        continue;
                    }

                    if (relativePath.Contains('/') || relativePath.EndsWith('/'))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to determine subdirectories for {Folder}", folderPath);
                return false;
            }
        }

    }
}
