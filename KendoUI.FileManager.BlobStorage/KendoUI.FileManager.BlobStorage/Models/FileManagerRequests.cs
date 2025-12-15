using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KendoUI.FileManager.BlobStorage.Models
{
    public class FileManagerReadRequest
    {
        [FromForm(Name = "target")]
        public string? Target { get; set; }

        [FromForm(Name = "path")]
        public string? Path { get; set; }
    }

    public class FileManagerCreateRequest
    {
        [Required]
        [FromForm(Name = "target")]
        public string? Target { get; set; }

        [Required]
        [FromForm(Name = "name")]
        public string? Name { get; set; }

        [FromForm(Name = "entry")]
        public int Entry { get; set; }

        [FromForm(Name = "path")]
        public string? Path { get; set; }

        [FromForm(Name = "source")]
        public string? Source { get; set; }

        [FromForm(Name = "sourcePath")]
        public string? SourcePath { get; set; }

        [FromForm(Name = "extension")]
        public string? Extension { get; set; }

        [FromForm(Name = "isDirectory")]
        public string? IsDirectoryFlag { get; set; }

        [FromForm(Name = "models")]
        public string? Models { get; set; }

        [FromForm(Name = "files")]
        public List<IFormFile>? Files { get; set; }
    }

    public class FileManagerUpdateRequest
    {
        [Required]
        public string? Path { get; set; }

        [Required]
        public string? Name { get; set; }
    }

    public class FileManagerDestroyRequest
    {
        [FromForm(Name = "models")]
        public string? Models { get; set; }

        [FromForm(Name = "path")]
        public string? Path { get; set; }

        [FromForm(Name = "target")]
        public string? Target { get; set; }

        [FromForm(Name = "name")]
        public string? Name { get; set; }
    }

    public class FileManagerUploadRequest
    {
        [FromForm(Name = "target")]
        public string? Target { get; set; }

        [FromForm(Name = "path")]
        public string? Path { get; set; }

        [Required]
        public IFormFile? File { get; set; }
    }
}
