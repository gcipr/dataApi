﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain.EntitiesDTO
{
    public class ReviewDTO : IReviewDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOfReview { get; set; }
        public int FishingSpotId { get; set; }
        public bool Published { get; set; }
    }
}