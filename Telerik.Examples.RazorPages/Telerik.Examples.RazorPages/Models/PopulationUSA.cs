using System.Collections.Generic;

namespace Telerik.Examples.RazorPages.Models
{
    public class PopulationUSA
    {
        public PopulationUSA(string name, int value, List<PopulationUSA> items)
        {
            Name = name;
            Value = value;
            Items = items;
        }

        public string Name { get; set; }
        public int Value { get; set; }

        public List<PopulationUSA> Items { get; set; }
    }
}