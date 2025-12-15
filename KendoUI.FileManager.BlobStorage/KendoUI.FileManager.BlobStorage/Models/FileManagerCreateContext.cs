using Microsoft.AspNetCore.Http;
using System.Linq;

namespace KendoUI.FileManager.BlobStorage.Models
{
    public sealed class FileManagerCreateContext
    {
        public IFormFile? UploadedFile { get; init; }
        public string? SourcePath { get; init; }
        public string? Extension { get; init; }
        public string? IsDirectoryFlag { get; init; }

        public static FileManagerCreateContext FromRequest(FileManagerCreateRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return new FileManagerCreateContext
            {
                UploadedFile = request.Files?.FirstOrDefault(),
                SourcePath = request.Path ??
                             request.Source ??
                             request.SourcePath,
                Extension = request.Extension,
                IsDirectoryFlag = request.IsDirectoryFlag
            };
        }
    }
}
