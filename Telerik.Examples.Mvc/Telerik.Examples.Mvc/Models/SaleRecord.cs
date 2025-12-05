namespace Telerik.Examples.Mvc.Models
{
    public class SaleRecord
    {
        public int Id { get; set; }
        public string SalesPerson { get; set; }
        public string Region { get; set; }
        public int UnitsSold { get; set; }
        public decimal Total { get; set; }
        public string Month { get; set; }
    }
}
