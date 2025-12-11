using KendoUI.FileManager.BlobStorage.Models;
using KendoUI.FileManager.BlobStorage.Helpers;
using Microsoft.AspNetCore.Mvc;
using KendoUI.FileManager.BlobStorage.Services;
using System.Diagnostics;

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
        public async Task<IActionResult> FileManager_Read([FromForm] string target)
        {
            try
            {
                // Retrieve the list of blobs/folders from the specified path
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
        public async Task<IActionResult> FileManager_Create([FromForm] string target, [FromForm] string name, [FromForm] int entry)
        {
            try
            {
                // Parse the form data to determine if this is a folder creation, file upload, or copy operation
                var context = FileManagerCreateContext.FromForm(Request.Form);
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
        public async Task<IActionResult> FileManager_Update()
        {
            try
            {
                var targetPath = Request.Form["path"];
                var newName = Request.Form["name"];

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
        public async Task<IActionResult> FileManager_Destroy([FromForm] string models)
        {
            try
            {
                // Parse the request to extract the path of the item to delete
                var targetPath = FileManagerRequestParser.ResolveTargetPath(Request.Form, models);
                if (string.IsNullOrEmpty(targetPath))
                {
                    var formData = string.Join(", ", Request.Form.Select(kvp => $"{kvp.Key}={kvp.Value}"));
                    return BadRequest(new { error = "No target path provided for deletion. Received: " + formData });
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
        public async Task<IActionResult> FileManager_Upload([FromForm] string target, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest(new { error = "No file uploaded" });
                }

                // Normalize the target path and upload the file to the blob container
                var resolvedTarget = NormalizeUploadTarget(target);
                var result = await _fileManagerService.UploadAsync(resolvedTarget, file);
                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upload file.");
                return BadRequest(new { error = ex.Message });
            }
        }

        private string NormalizeUploadTarget(string? target)
        {
            var resolvedTarget = target ??
                                 Request.Form["target"].FirstOrDefault() ??
                                 Request.Form["path"].FirstOrDefault() ??
                                 string.Empty;

            return string.IsNullOrEmpty(resolvedTarget)
                ? string.Empty
                : resolvedTarget.Trim('/');
        }
    }
}
