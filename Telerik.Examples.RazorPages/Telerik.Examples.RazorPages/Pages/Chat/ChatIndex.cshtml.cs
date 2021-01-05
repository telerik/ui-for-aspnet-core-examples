using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Telerik.Examples.RazorPages.Pages.Chat
{   
    public class ChatIndexModel : PageModel
    {
        public bool Toggable { get; set; }
        public bool Scrollable { get; set; }

        public string Placeholder { get; set; }

        public string SendButton { get; set; }

        public void OnGet()
        {
            Toggable = true;
            Scrollable = true;
            Placeholder = "Custom placeholder";
            SendButton = "Custom send button message";
        }
    }
}
