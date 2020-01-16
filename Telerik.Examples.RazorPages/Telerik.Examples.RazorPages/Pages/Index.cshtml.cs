using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Telerik.Examples.RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IEnumerable<string> fileNames;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var foldersToExclude = new string[] { "Pages", "Shared", "EditorTemplates" };
            var fileNames = new List<string>();
            var directoryNames = new List<string>();
            var txtPath = @".\Pages";
            string[] files = Directory.GetFiles(txtPath, "*.cshtml", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var parentDirectory = Path.GetFileName(Path.GetDirectoryName(file));
                if (!foldersToExclude.Contains(parentDirectory) && !fileName.StartsWith("_"))
                {
                    if (!directoryNames.Contains(parentDirectory))
                    {
                        fileNames.Add(parentDirectory);
                        fileNames.Add(fileName);
                        directoryNames.Add(parentDirectory);
                    }
                    else
                    {
                        fileNames.Add(fileName);
                    }
                }
            }

            this.fileNames = fileNames;
        }
    }
}
