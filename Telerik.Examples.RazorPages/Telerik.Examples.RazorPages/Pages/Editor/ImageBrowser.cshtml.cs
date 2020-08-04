using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.Editor
{
    [IgnoreAntiforgeryToken]
    public class ImageBrowserModel : PageModel
    {
        private readonly string ContentPath = "Images/";
        private readonly string Filter = "*.png,*.gif,*.jpg,*.jpeg";
        private IWebHostEnvironment HostingEnvironment;

        public DirectoryBrowser directoryBrowser { get; private set; }

        public ImageBrowserModel(IWebHostEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
            directoryBrowser = new DirectoryBrowser();
        }

        public void OnGet()
        {
        }

        public JsonResult OnPostRead(string path)
        {
            var fullPath = NormalizePath(path);

            if (AuthorizeRead(fullPath))
            {
                try
                {
                    var files = directoryBrowser.GetFiles(fullPath, Filter);
                    var directories = directoryBrowser.GetDirectories(fullPath);
                    var result = files.Concat(directories);

                    return new JsonResult(result.ToArray());
                }
                catch (DirectoryNotFoundException)
                {
                    throw new Exception("File Not Found");
                }
            }

            throw new Exception("Forbidden");
        }

        public JsonResult OnPostCreate(string path, FileBrowserEntry entry)
        {
            var fullPath = NormalizePath(path);
            var name = entry.Name;

            if (name.HasValue() && AuthorizeCreateDirectory(fullPath, name))
            {
                var physicalPath = Path.Combine(fullPath, name);

                if (!Directory.Exists(physicalPath))
                {
                    Directory.CreateDirectory(physicalPath);
                }

                return new JsonResult(entry);
            }

            throw new Exception("Forbidden");
        }

        public JsonResult OnPostDestroy(string path, FileBrowserEntry entry)
        {
            var fullPath = NormalizePath(path);

            if (entry != null)
            {
                fullPath = Path.Combine(fullPath, entry.Name);

                if (entry.EntryType == "f")
                {
                    DeleteFile(fullPath);
                }
                else
                {
                    DeleteDirectory(fullPath);
                }

                return new JsonResult(new object[0]);
            }

            throw new Exception("File Not Found");
        }

        public virtual ActionResult OnPostUpload(string path, IFormFile file)
        {
            var fullPath = NormalizePath(path);

            if (AuthorizeUpload(fullPath, file))
            {
                SaveFile(file, fullPath);

                var result = new FileBrowserEntry
                {
                    Size = file.Length,
                    Name = GetFileName(file)
                };

                return new JsonResult(result);
            }

            throw new Exception("Forbidden");
        }

        private string NormalizePath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return Path.GetFullPath(Path.Combine(this.HostingEnvironment.WebRootPath, ContentPath));
            }
            else
            {
                return Path.GetFullPath(Path.Combine(this.HostingEnvironment.WebRootPath, ContentPath, path));
            }
        }
        private bool AuthorizeRead(string path)
        {
            return CanAccess(path);
        }

        private bool AuthorizeCreateDirectory(string path, string name)
        {
            return CanAccess(path);
        }

        private bool CanAccess(string path)
        {
            var rootPath = Path.GetFullPath(Path.Combine(this.HostingEnvironment.WebRootPath, ContentPath));
            
            return path.StartsWith(rootPath, StringComparison.OrdinalIgnoreCase);
        }

        private void DeleteFile(string path)
        {
            if (!AuthorizeDeleteFile(path))
            {
                throw new Exception("Forbidden");
            }

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        private void DeleteDirectory(string path)
        {
            if (!AuthorizeDeleteDirectory(path))
            {
                throw new Exception("Forbidden");
            }

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        private bool AuthorizeDeleteFile(string path)
        {
            return CanAccess(path);
        }

        private bool AuthorizeDeleteDirectory(string path)
        {
            return CanAccess(path);
        }

        private bool AuthorizeUpload(string path, IFormFile file)
        {
            return CanAccess(path) && IsValidFile(GetFileName(file));
        }

        private bool IsValidFile(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            var allowedExtensions = Filter.Split(',');

            return allowedExtensions.Any(e => e.Equals("*.*") || e.EndsWith(extension, StringComparison.OrdinalIgnoreCase));
        }

        private string GetFileName(IFormFile file)
        {
            var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
            return Path.GetFileName(fileContent.FileName.ToString().Trim('"'));
        }

        private void SaveFile(IFormFile file, string pathToSave)
        {
            try
            {
                var path = Path.Combine(pathToSave, GetFileName(file));
                using (var stream = System.IO.File.Create(path))
                {
                    file.CopyTo(stream);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    public class DirectoryBrowser
    {
        public IWebHostEnvironment HostingEnvironment { get; set; }

        public IEnumerable<FileBrowserEntry> GetFiles(string path, string filter)
        {
            var directory = new DirectoryInfo(path);
            var extensions = (filter ?? "*").Split(new string[] { ", ", ",", "; ", ";" }, StringSplitOptions.RemoveEmptyEntries);
            var files = extensions.SelectMany(directory.GetFiles)
                .Select(file => new FileBrowserEntry
                {
                    Name = file.Name,
                    Size = file.Length,
                    EntryType = "f"
                });

            return files;
        }

        public IEnumerable<FileBrowserEntry> GetDirectories(string path)
        {
            var directory = new DirectoryInfo(path);

            return directory.GetDirectories()
                .Select(subDirectory => new FileBrowserEntry
                {
                    Name = subDirectory.Name,
                    EntryType = "d"
                });
        }
    }
}
