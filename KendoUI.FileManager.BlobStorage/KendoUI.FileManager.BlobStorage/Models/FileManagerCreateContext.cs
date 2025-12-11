using Microsoft.AspNetCore.Http;

namespace KendoUI.FileManager.BlobStorage.Models
{
    public sealed class FileManagerCreateContext
    {
        public IFormFile? UploadedFile { get; init; }
        public string? SourcePath { get; init; }
        public string? Extension { get; init; }
        public string? IsDirectoryFlag { get; init; }

        public static FileManagerCreateContext FromForm(IFormCollection form)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return new FileManagerCreateContext
            {
                UploadedFile = form.Files.FirstOrDefault(),
                SourcePath = form["path"].FirstOrDefault() ??
                             form["source"].FirstOrDefault() ??
                             form["sourcePath"].FirstOrDefault(),
                Extension = form["extension"].FirstOrDefault(),
                IsDirectoryFlag = form["isDirectory"].FirstOrDefault()
            };
        }
    }
}
