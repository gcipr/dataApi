﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Street { get; set; }
        public string HouseNo { get; set; }
    }
}
