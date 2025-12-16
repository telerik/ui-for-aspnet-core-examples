using System.IO;

namespace KendoUI.FileManager.BlobStorage.Helpers
{
    internal static class BlobPathHelper
    {
        public static string BuildPrefix(string? target)
        {
            return string.IsNullOrWhiteSpace(target)
                ? string.Empty
                : target.TrimEnd('/') + "/";
        }

        public static string CombinePath(string? target, string name)
        {
            var prefix = BuildPrefix(target);
            return string.IsNullOrEmpty(prefix) ? name : prefix + name;
        }

        public static string EnsureTrailingSlash(string? path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path ?? string.Empty;
            }

            return path.EndsWith('/') ? path : path + "/";
        }

        public static bool ShouldTreatAsDirectory(string name, string? isDirectoryFlag, int entry)
        {
            if (!string.IsNullOrEmpty(isDirectoryFlag) && bool.TryParse(isDirectoryFlag, out var isDirectory) && isDirectory)
            {
                return true;
            }

            if (entry == 1)
            {
                return true;
            }

            return !HasExtension(name);
        }

        public static string BuildNewPath(string targetPath, string newName, bool isDirectory)
        {
            var lastSlashIndex = targetPath.LastIndexOf('/');

            if (isDirectory)
            {
                return lastSlashIndex >= 0
                    ? targetPath.Substring(0, lastSlashIndex + 1) + newName
                    : newName;
            }

            var originalExtension = Path.GetExtension(targetPath);
            var newExtension = Path.GetExtension(newName);

            if (string.IsNullOrEmpty(newExtension) && !string.IsNullOrEmpty(originalExtension))
            {
                newName += originalExtension;
            }

            return lastSlashIndex >= 0
                ? targetPath.Substring(0, lastSlashIndex + 1) + newName
                : newName;
        }

        public static bool HasExtension(string? name)
        {
            return !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(Path.GetExtension(name));
        }
    }
}
