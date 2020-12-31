using System.Collections.Generic;

namespace Telerik.Examples.RazorPages.Models
{
    public partial class TreeMapDataRepository
    {
        public static List<PopulationUSA> PopulationUSAData()
        {
            List<PopulationUSA> result = new List<PopulationUSA>();

            PopulationUSA usa = new PopulationUSA("Population in USA", 316128839, new List<PopulationUSA>());
            result.Add(usa);

            PopulationUSA alabama = new PopulationUSA("Alabama", 4833722, new List<PopulationUSA>());
            usa.Items.Add(alabama);
            alabama.Items.Add(new PopulationUSA("Birmingham", 212113, null));
            alabama.Items.Add(new PopulationUSA("Montgomery", 201332, null));
            alabama.Items.Add(new PopulationUSA("Mobile", 194899, null));
            alabama.Items.Add(new PopulationUSA("Huntsville", 186254, null));
            alabama.Items.Add(new PopulationUSA("Tuscaloosa", 95334, null));
            alabama.Items.Add(new PopulationUSA("Hoover", 84126, null));
            alabama.Items.Add(new PopulationUSA("Dothan", 68001, null));
            alabama.Items.Add(new PopulationUSA("Auburn", 58582, null));
            alabama.Items.Add(new PopulationUSA("Decatur", 55816, null));

            PopulationUSA alaska = new PopulationUSA("Alaska", 735132, new List<PopulationUSA>());
            usa.Items.Add(alaska);
            alaska.Items.Add(new PopulationUSA("Anchorage", 300950, null));
            alaska.Items.Add(new PopulationUSA("Badger", 20200, null));
            alaska.Items.Add(new PopulationUSA("College", 13400, null));
            alaska.Items.Add(new PopulationUSA("Fairbanks", 32324, null));
            alaska.Items.Add(new PopulationUSA("Juneau", 32660, null));
            alaska.Items.Add(new PopulationUSA("Ketchikan", 8214, null));
            alaska.Items.Add(new PopulationUSA("Sitka", 9020, null));

            PopulationUSA arizona = new PopulationUSA("Arizona", 6626624, new List<PopulationUSA>());
            usa.Items.Add(arizona);
            arizona.Items.Add(new PopulationUSA("Phoenix", 1513367, null));
            arizona.Items.Add(new PopulationUSA("Tucson", 526116, null));
            arizona.Items.Add(new PopulationUSA("Mesa", 457587, null));
            arizona.Items.Add(new PopulationUSA("Chandler", 249146, null));
            arizona.Items.Add(new PopulationUSA("Glendale", 234632, null));
            arizona.Items.Add(new PopulationUSA("Gilbert", 229972, null));
            arizona.Items.Add(new PopulationUSA("Scottsdale", 226918, null));
            arizona.Items.Add(new PopulationUSA("Tempe", 168228, null));
            arizona.Items.Add(new PopulationUSA("Peoria", 162592, null));
            arizona.Items.Add(new PopulationUSA("Surprise", 123546, null));

            PopulationUSA arkansas = new PopulationUSA("Arkansas", 2959373, new List<PopulationUSA>());
            usa.Items.Add(arkansas);
            arkansas.Items.Add(new PopulationUSA("Little Rock", 197357, null));
            arkansas.Items.Add(new PopulationUSA("Fort Smith", 87650, null));
            arkansas.Items.Add(new PopulationUSA("Fayetteville", 78960, null));
            arkansas.Items.Add(new PopulationUSA("Springdale", 75229, null));
            arkansas.Items.Add(new PopulationUSA("Jonesboro", 71551, null));
            arkansas.Items.Add(new PopulationUSA("North Little Rock", 66075, null));
            arkansas.Items.Add(new PopulationUSA("Conway", 63816, null));
            arkansas.Items.Add(new PopulationUSA("Rogers", 60112, null));
            arkansas.Items.Add(new PopulationUSA("Pine Bluff", 46094, null));
            arkansas.Items.Add(new PopulationUSA("Bentonville", 40167, null));

            PopulationUSA california = new PopulationUSA("California", 38332521, new List<PopulationUSA>());
            usa.Items.Add(california);
            california.Items.Add(new PopulationUSA("Los Angeles", 3884307, null));
            california.Items.Add(new PopulationUSA("San Diego", 1355896, null));
            california.Items.Add(new PopulationUSA("San Jose", 998537, null));
            california.Items.Add(new PopulationUSA("San Francisco", 837442, null));
            california.Items.Add(new PopulationUSA("Fresno", 509924, null));
            california.Items.Add(new PopulationUSA("Sacramento", 479686, null));
            california.Items.Add(new PopulationUSA("Long Beach", 469428, null));
            california.Items.Add(new PopulationUSA("Oakland", 406253, null));
            california.Items.Add(new PopulationUSA("Bakersfield", 363630, null));
            california.Items.Add(new PopulationUSA("Anaheim", 345012, null));
            california.Items.Add(new PopulationUSA("Santa Ana", 334227, null));

            PopulationUSA colorado = new PopulationUSA("Colorado", 5268367, new List<PopulationUSA>());
            usa.Items.Add(colorado);
            colorado.Items.Add(new PopulationUSA("Denver", 649495, null));
            colorado.Items.Add(new PopulationUSA("Colorado Springs", 439886, null));
            colorado.Items.Add(new PopulationUSA("Aurora", 345803, null));
            colorado.Items.Add(new PopulationUSA("Fort Collins", 152061, null));
            colorado.Items.Add(new PopulationUSA("Lakewood", 147214, null));
            colorado.Items.Add(new PopulationUSA("Thornton", 127359, null));
            colorado.Items.Add(new PopulationUSA("Arvada", 111707, null));
            colorado.Items.Add(new PopulationUSA("Westminster", 110945, null));
            colorado.Items.Add(new PopulationUSA("Pueblo", 108249, null));
            colorado.Items.Add(new PopulationUSA("Centennial", 106114, null));
            colorado.Items.Add(new PopulationUSA("Boulder", 103166, null));
            colorado.Items.Add(new PopulationUSA("Highlands Ranch", 102000, null));

            PopulationUSA connecticut = new PopulationUSA("Connecticut", 3596080, new List<PopulationUSA>());
            usa.Items.Add(connecticut);
            connecticut.Items.Add(new PopulationUSA("Bridgeport", 147216, null));
            connecticut.Items.Add(new PopulationUSA("New Haven", 130660, null));
            connecticut.Items.Add(new PopulationUSA("Stamford", 126456, null));
            connecticut.Items.Add(new PopulationUSA("Hartford", 125017, null));
            connecticut.Items.Add(new PopulationUSA("Waterbury", 109676, null));
            connecticut.Items.Add(new PopulationUSA("Norwalk", 87776, null));
            connecticut.Items.Add(new PopulationUSA("Danbury", 83684, null));
            connecticut.Items.Add(new PopulationUSA("New Britain", 72939, null));
            connecticut.Items.Add(new PopulationUSA("West Hartford", 63371, null));
            connecticut.Items.Add(new PopulationUSA("Bristol", 60568, null));
            connecticut.Items.Add(new PopulationUSA("Meriden", 60456, null));

            return result;
        }
    }
}