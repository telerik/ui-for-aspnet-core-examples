using System.Collections;

namespace WebApplication3.Models
{
    public class MyResponseModel
    {
        public IEnumerable DataCollection{ get; set; }

        public int TotalRecords { get; set; }
    }
}
