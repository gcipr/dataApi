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
        public string FishType { get; set; }
    }
   
}
