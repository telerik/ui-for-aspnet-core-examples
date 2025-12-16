using KendoUI.FileManager.BlobStorage.Models;
using Microsoft.AspNetCore.Http;

namespace KendoUI.FileManager.BlobStorage.Services
{
    public interface IBlobFileManagerService
    {
        Task<IEnumerable<FileManagerEntry>> ReadAsync(string? target);
        Task<FileManagerEntry> CreateAsync(string? target, string name, int entry, FileManagerCreateContext context);
        Task<FileManagerEntry> UploadAsync(string? target, IFormFile file);
        Task<FileManagerEntry> UpdateAsync(string targetPath, string newName);
        Task DeleteAsync(string targetPath);
    }
}
