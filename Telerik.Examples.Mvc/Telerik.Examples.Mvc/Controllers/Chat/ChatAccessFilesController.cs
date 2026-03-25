using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace Telerik.Examples.Mvc.Controllers.Chat
{
    public class ChatAccessFilesController : Controller
    {
        private static readonly IDictionary<string, ChatUploadedFile> UploadedFiles = new Dictionary<string, ChatUploadedFile>();

        public IActionResult ChatAccessFiles()
        {
            var data = new List<ChatMessage>()
            {
                new ChatMessage {
                    Id = "1",
                    AuthorId = "2",
                    AuthorName = "Peter Smith",
                    AuthorImageUrl = "https://demos.telerik.com/aspnet-mvc/content/web/Customers/GOURL.jpg",
                    AuthorImageAltText = "Peter's profile picture",
                    Text = "Use the paperclip button in the message input area to attach files, and the file action menu to download or delete them.",
                    TimeStamp = DateTime.Now
                },
                new ChatMessage {
                    Id = "2",
                    AuthorId = "2",
                    AuthorName = "Peter Smith",
                    AuthorImageUrl = "https://demos.telerik.com/aspnet-mvc/content/web/Customers/GOURL.jpg",
                    AuthorImageAltText = "Peter's profile picture",
                    Text = "Go ahead and upload your first file. Once uploaded, I will receive it with a download link so both of us can access it.",
                    TimeStamp = DateTime.Now
                }
            };
            return View(data);
        }

        [HttpPost]
        public IActionResult SaveFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Json(new { success = false, message = "No file uploaded." });
            }

            var fileName = Path.GetFileName(file.FileName);
            var fileId = Guid.NewGuid().ToString("N");

            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            var uploadedFile = new ChatUploadedFile
            {
                FileId = fileId,
                FileName = fileName,
                ContentType = file.ContentType,
                Size = file.Length,
                Data = fileBytes
            };

            UploadedFiles[fileId] = uploadedFile;

            var fileUrl = Url.Action("DownloadFile", "ChatAccessFiles", new { fileId = fileId }, Request.Scheme);

            return Json(new
            {
                success = true,
                url = fileUrl,
                name = fileName,
                size = file.Length,
                extension = Path.GetExtension(fileName)
            });
        }

        public IActionResult DownloadFile(string fileId)
        {
            if (string.IsNullOrEmpty(fileId))
            {
                return NotFound();
            }

            if (!UploadedFiles.TryGetValue(fileId, out ChatUploadedFile uploadedFile))
            {
                return NotFound();
            }

            return File(uploadedFile.Data, uploadedFile.ContentType, uploadedFile.FileName);
        }
    }

    [Serializable]
    public class ChatUploadedFile
    {
        public string FileId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public byte[] Data { get; set; }
    }
}
