using System;
using System.ComponentModel.DataAnnotations;

namespace Telerik.Examples.Mvc.Models
{
    public class MusicianViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Song Writer")]
        public string Name { get; set; }

        [UIHint("Number")]
        public int Age { get; set; }

        public string Nationality { get; set; }

        [Display(Name = "Introduction")]
        public Audio Intro { get; set; }
    }

    public class Audio
    {
        public string FileName { get; set; }
        public string Extension { get; set; }
    }
}
