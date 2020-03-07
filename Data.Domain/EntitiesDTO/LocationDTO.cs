using Data.Domain.Abstract;
using Data.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain.EntitiesDTO
{
    public class LocationDTO :ILocation
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int FishingSpotId { get; set; }
        public FishingSpot FishingSpot { get; set; }
    }
}
