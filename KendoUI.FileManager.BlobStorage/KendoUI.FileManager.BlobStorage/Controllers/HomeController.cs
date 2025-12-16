using KendoUI.FileManager.BlobStorage.Models;
using Microsoft.AspNetCore.Mvc;
using KendoUI.FileManager.BlobStorage.Services;
using System.Diagnostics;
using System.Text.Json;

namespace KendoUI.FileManager.BlobStorage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // Inject the service that handles Azure Blob Storage operations
        private readonly IBlobFileManagerService _fileManagerService;

        public HomeController(ILogger<HomeController> logger, IBlobFileManagerService fileManagerService)
        {
            _logger = logger;
            _fileManagerService = fileManagerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Alternative()
        {
            return View("Index_Alternative");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        // Handles reading files and folders from Azure Blob Storage for the FileManager
        [HttpPost]
        public async Task<IActionResult> FileManager_Read([FromForm] FileManagerReadRequest request)
        {
            try
            {
                // Retrieve the list of blobs/folders from the specified path
                var target = NormalizePath(request.Target ?? request.Path ?? string.Empty);
                var files = await _fileManagerService.ReadAsync(target);
                return Json(files);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to read FileManager contents.");
                return BadRequest(new { error = ex.Message });
            }
        }

        // Handles creating new folders or copying/pasting files in Azure Blob Storage
        [HttpPost]
        public async Task<IActionResult> FileManager_Create([FromForm] FileManagerCreateRequest request)
        {
            try
            {
                var target = NormalizePath(request.Target ??
                                            request.Path ??
                                            request.Source ??
                                            request.SourcePath ??
                                            string.Empty);
                var name = request.Name?.Trim();
                var entry = request.Entry;

                if (string.IsNullOrWhiteSpace(name))
                {
                    return BadRequest(new { error = "Name is required for create operation" });
                }

                // Parse the form data to determine if this is a folder creation, file upload, or copy operation
                var context = FileManagerCreateContext.FromRequest(request);
                var result = await _fileManagerService.CreateAsync(target, name, entry, context);
                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create entry in FileManager.");
                return BadRequest(new { error = ex.Message });
            }
        }

        // Handles renaming files or folders in Azure Blob Storage
        [HttpPost]
        public async Task<IActionResult> FileManager_Update([FromForm] FileManagerUpdateRequest request)
        {
            try
            {
                var targetPath = request.Path;
                var newName = request.Name;

                if (string.IsNullOrEmpty(targetPath) || string.IsNullOrEmpty(newName))
                {
                    return BadRequest(new { error = "Path and name are required for rename operation" });
                }

                // Rename is implemented by copying the blob to a new path and deleting the old one
                var result = await _fileManagerService.UpdateAsync(targetPath!, newName!);
                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to rename FileManager entry.");
                return BadRequest(new { error = ex.Message });
            }
        }

        // Handles deleting files or folders from Azure Blob Storage
        [HttpPost]
        public async Task<IActionResult> FileManager_Destroy([FromForm] FileManagerDestroyRequest request)
        {
            try
            {
                // Parse the request to extract the path of the item to delete
                var targetPath = ResolveTargetPath(request);
                if (string.IsNullOrEmpty(targetPath))
                {
                    return BadRequest(new { error = "No target path provided for deletion." });
                }

                await _fileManagerService.DeleteAsync(targetPath);
                return Json(Array.Empty<object>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete FileManager entry.");
                return BadRequest(new { error = ex.Message });
            }
        }

        // Handles file uploads to Azure Blob Storage
        [HttpPost]
        public async Task<IActionResult> FileManager_Upload([FromForm] FileManagerUploadRequest request)
        {
            try
            {
                if (request.File == null || request.File.Length == 0)
                {
                    return BadRequest(new { error = "No file uploaded" });
                }

                // Normalize the target path and upload the file to the blob container
                var resolvedTarget = NormalizeUploadTarget(request);
                var result = await _fileManagerService.UploadAsync(resolvedTarget, request.File);
                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upload file.");
                return BadRequest(new { error = ex.Message });
            }
        }

        private static string NormalizePath(string? target)
        {
            if (string.IsNullOrWhiteSpace(target))
            {
                return string.Empty;
            }

            return target.Trim('/');
        }

        private static string NormalizeUploadTarget(FileManagerUploadRequest request)
        {
            var resolvedTarget = request.Target ?? request.Path ?? string.Empty;
            return NormalizePath(resolvedTarget);
        }

        private static string? ResolveTargetPath(FileManagerDestroyRequest request)
        {
            var targetPath = request.Path ?? request.Target ?? request.Name;

            if (!string.IsNullOrWhiteSpace(targetPath))
            {
                return targetPath;
            }

            if (string.IsNullOrWhiteSpace(request.Models))
            {
                return null;
            }

            try
            {
                var modelsArray = JsonSerializer.Deserialize<JsonElement[]>(request.Models);
                if (modelsArray is { Length: > 0 })
                {
                    var firstModel = modelsArray[0];
                    if (firstModel.TryGetProperty("path", out var pathElement))
                    {
                        return pathElement.GetString();
                    }
                }
            }
            catch
            {
                // Swallow JSON parsing errors and fall back to form values
            }

            return null;
        }
    }
}
