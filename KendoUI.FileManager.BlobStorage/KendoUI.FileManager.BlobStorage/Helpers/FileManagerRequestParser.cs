using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace KendoUI.FileManager.BlobStorage.Helpers
{
    public static class FileManagerRequestParser
    {
        public static string? ResolveTargetPath(IFormCollection form, string? models)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            var targetPath = form["path"].FirstOrDefault() ??
                             form["target"].FirstOrDefault() ??
                             form["Name"].FirstOrDefault() ??
                             form["name"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(models))
            {
                return targetPath;
            }

            try
            {
                var modelsArray = JsonSerializer.Deserialize<JsonElement[]>(models);
                if (modelsArray is not { Length: > 0 })
                {
                    return targetPath;
                }

                var firstModel = modelsArray[0];
                if (firstModel.TryGetProperty("path", out var pathElement))
                {
                    targetPath = pathElement.GetString();
                }
            }
            catch
            {
                // Intentionally swallow JSON parsing errors and fall back to form values
            }

            return targetPath;
        }
    }
}
