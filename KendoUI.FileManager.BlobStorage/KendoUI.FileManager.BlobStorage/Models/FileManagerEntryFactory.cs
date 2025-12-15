namespace KendoUI.FileManager.BlobStorage.Models
{
    internal static class FileManagerEntryFactory
    {
        public static FileManagerEntry Create(
            string displayName,
            bool isDirectory,
            bool hasDirectories,
            string path,
            string extension,
            long size,
            DateTimeOffset? createdOn,
            DateTimeOffset? modifiedOn)
        {
            return new FileManagerEntry
            {
                Name = displayName ?? string.Empty,
                IsDirectory = isDirectory,
                HasDirectories = hasDirectories,
                Path = path ?? string.Empty,
                Extension = extension ?? string.Empty,
                Size = size,
                Created = ResolveLocalDate(createdOn),
                CreatedUtc = ResolveUtcDate(createdOn),
                Modified = ResolveLocalDate(modifiedOn),
                ModifiedUtc = ResolveUtcDate(modifiedOn),
                DateCreated = ResolveLocalDate(createdOn),
                DateModified = ResolveLocalDate(modifiedOn)
            };
        }

        public static FileManagerEntry Placeholder(string name, string path, string extension)
        {
            var now = DateTimeOffset.UtcNow;
            return Create(name, false, false, path, extension, 0, now, now);
        }

        private static DateTime ResolveLocalDate(DateTimeOffset? timestamp)
        {
            return timestamp?.LocalDateTime ?? DateTime.Now;
        }

        private static DateTime ResolveUtcDate(DateTimeOffset? timestamp)
        {
            return timestamp?.UtcDateTime ?? DateTime.UtcNow;
        }
    }
}
