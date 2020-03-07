using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain.Entities
{
    public class FishingSpot
    {
        public int Id { get; set; }
        public string WaterType { get; set; }
        public string Administration { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int Tax { get; set; }
        public bool Licence { get; set; }
        public Location Locations { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public int County { get; set; }
    }
    public enum County
    {
        Alba,
        Arad,
        Arges,
        Bacău,
        Bihor,
        Bistriţa,
        Botoşani,
        Braşov,
        Brăila,
        Bucureşti,
        Buzău,
        CarasSeverin,
        Călăraşi,
        Cluj,
        Constanţa,
        Covasna,
        Dâmboviţa,
        Dolj,
        Galaţi,
        Giurgiu,
        Gorj,
        Harghita,
        Hunedoara,
        Ialomiţa,
        Iaşi,
        Ilfov,
        Maramureş,
        Mehedinţi,
        Mureş,
        Neamţ,
        Olt,
        Prahova,
        SatuMare,
        Sălaj,
        Sibiu,
        Suceava,
        Teleorman,
        Timiş,
        Tulcea,
        Vâlcea,
        Vaslui,
        Vrancea
    }
}
