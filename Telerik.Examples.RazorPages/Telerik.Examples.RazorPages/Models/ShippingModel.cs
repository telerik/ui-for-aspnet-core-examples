using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace Telerik.Examples.RazorPages.Models
{
    public class ShippingModel : OrderViewModel
    {
        //The name of the EditorTemplate View in /Views/Shared/EditorTemplates
        [UIHint("StateGridForeignKey")]
        [DisplayName("State ID")]
        public int? Fk_StateID { get; set; }

        //The name of the EditorTemplate View in /Views/Shared/EditorTemplates

        [UIHint("CountryGridForeignKey")]
        [DisplayName("Country")]
        public int? Fk_CountryID { get; set; }
    }
}
