using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Domain.Concrete;
using Data.Domain.Entities;
using System.Dynamic;
using System.Collections.Immutable;
using System.Collections;

namespace Data.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly EFDbContext _context;
        public enum DistanceUnit { Miles, Kilometers };
        public LocationsController(EFDbContext context)
        {
            _context = context;
        }
        private double ToRadians(double val)
        {
            return (Math.PI / 100) * val;
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetLocations()
        {

            var list = await _context.Locations
                .Select(x => new {x.Latitude, x.Longitude, x.FishingSpot.Name, x.FishingSpotId, County = ((County)x.FishingSpot.County).ToString() })
                .ToListAsync();
            return list;
        }
        
        // GET: locations/latitude/longitude/n
        [HttpGet("latitude={latitude}/longitude={longitude}/n={n}")]
        public async Task<IEnumerable<object>> GetLocation(double latitude, double longitude, DistanceUnit unit, int n)
        {
            // Created a dictionary to store id of the location and he distance to our location
            Dictionary<int, double> idDistance = new Dictionary<int, double>();

            //Getting a list of Location objects
            var locations = await _context.Locations
                .Select(x => new { x.Id, x.Latitude, x.Longitude, x.FishingSpot.Name, x.FishingSpotId, County = ((County)x.FishingSpot.County).ToString() })
                .ToArrayAsync();

            // Calculating the distance between our location and all the locations in SQL using haversin mathematical formula
            for (var i=0; i < locations.Length; i++)
            {
                 double R = (unit == DistanceUnit.Kilometers) ? 6371 : 3960 ;
                 var lat = ToRadians((locations[i].Latitude - latitude));
                 var lng = ToRadians((locations[i].Longitude - longitude));
                 var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(ToRadians(latitude)) * Math.Cos(ToRadians(locations[i].Latitude)) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
                 var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
                 var distance = R * h2;

                idDistance.Add(locations[i].Id, distance);
            }

            idDistance.OrderBy(d => d.Value);
            var idList = idDistance.Keys.ToArray().Take(n);
            var renderedLocations = locations.Where(e => idList.Contains(e.Id));

            return renderedLocations;
        }


        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.Id == id);
        }
    }
}
