using System.ComponentModel.DataAnnotations;

namespace Telerik.Examples.Mvc.Models
{
    public class UserDetailsModel
    {
        public string Type
        {
            get;
            set;
        }
        public AccountDetailsModel AccountDetails
        {
            get;
            set;
        }

        public CompanyDetailsModel CompanyDetails
        {
            get;
            set;
        }
    }
    public class AccountDetailsModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }
    }

    public class CompanyDetailsModel
    {
        [Required]
        public string Country { get; set; }

        [Required]
        public string CompanyName { get; set; }

    }
}
