using System.Collections.Generic;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.MediaPlayer
{
    public class MediaPlayerPlaylistModel : PageModel
    {
        private static IList<Video> videos;

        public void OnGet()
        {
            if (videos == null)
            {
                videos = new List<Video>();

                videos.Add(new Video()
                {
                    Title = "Build HIPAA-Compliant Healthcare Apps Fast",
                    Poster = "https://img.youtube.com/vi/_S63eCewxRg/1.jpg",
                    Source = "https://www.youtube.com/watch?v=dyvxivS5EcI"
                });

                videos.Add(new Video()
                {
                    Title = "ProgressNEXT 2018 Highlights",
                    Poster = "https://img.youtube.com/vi/DYsiJRmIQZw/1.jpg",
                    Source = "https://www.youtube.com/watch?v=Gp7tcOcSKAU"
                });

                videos.Add(new Video()
                {
                    Title = "Benefits of Being a Progress Partner",
                    Poster = "https://i.ytimg.com/vi/xzrHbJmQbB8/1.jpg",
                    Source = "https://www.youtube.com/watch?v=xzrHbJmQbB8"
                });

                videos.Add(new Video()
                {
                    Title = "Progress Application Server OpenEdge",
                    Poster = "https://i.ytimg.com/vi/CpHKm2NruYc/1.jpg",
                    Source = "https://www.youtube.com/watch?v=3Ce11N9udR4"
                });
            }
        }

        public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
        {
            return new JsonResult(videos.ToDataSourceResult(request));
        }
    }
}
