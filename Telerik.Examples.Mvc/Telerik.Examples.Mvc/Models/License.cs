using System.ComponentModel.DataAnnotations;

namespace Telerik.Examples.Mvc.Models
{
    public class License
    {
        public int LicenseId { get; set; }

        [UIHint("CustomerId")]
        public Customer Customer { get; set; }

        [UIHint("VendorId")]
        public Vendor Vendor { get; set; }

        [UIHint("ItemId")]
        public Item Item { get; set; }
    }
}
