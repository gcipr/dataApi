using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain.Entities
{
    // This class belongs to Portfolio project
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool IsComplete { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
