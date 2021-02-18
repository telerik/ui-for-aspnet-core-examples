using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Telerik.Examples.RazorPages.Models
{
    public class UserViewModel
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Name must be at least 5 characters long")]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string BirthDate { get; set; }

        public string Gender { get; set; }
    }
}
