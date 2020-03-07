using Data.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain.EntitiesDTO
{
    public class FishingSpotDTO
    {
        public int Id { get; set; }
        public string WaterType { get; set; }
        public string Administration { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Url { get; set; }
        public int Tax { get; set; }
        public bool Licence { get; set; }
        public ILocation LocationsDTO { get; set; }
        public IEnumerable<IReviewDTO> ReviewsDTO { get; set; }
        public int County { get; set; }
    }
}
