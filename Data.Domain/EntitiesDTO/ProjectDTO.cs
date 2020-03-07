using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain.EntitiesDTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool IsComplete { get; set; }
    }
}
