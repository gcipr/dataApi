using Data.Domain.EntitiesDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Published { get; set; }
        public int Rating { get; set; }

        public DateTime DateOfReview { get; set; }
        public int FishingSpotId { get; set; }
        public bool WouldYouRecommend { get; set; }

    }
}
