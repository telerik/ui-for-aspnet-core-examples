namespace Telerik.Examples.Mvc.Models
{
    public class FoodModel
    {
        public int FoodID { get; set; }
        public string FoodName { get; set; }
        public int? IsPartOf { get; set; }
        public bool expanded { get; set; } = false;
        public bool hasChildren { get; set; } = false;
    }
}
