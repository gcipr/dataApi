using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Domain.Concrete;
using System.Collections.Immutable;

namespace Data.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly EFDbContext _context;
        public enum DistanceUnit { Miles, Kilometers };
        Dictionary<int, double> distancelist = new Dictionary<int, double>(); // Created a dictionary to store id of the  
                                                                              // location and the distance to our location

        public LocationsController(EFDbContext context)
        {
            _context = context;
        }

        [HttpGet] // GET: api/Locations
        public  IQueryable<object> GetLocations()
        {
            var F = _context.FishingSpots;
            var L =  _context.Locations;

            var Locations = from location in L
                                  join fishingSpot in F on location.FishingSpotId equals fishingSpot.Id
                                  select new { latitude = location.Latitude, longitude = location.Longitude, county = location.County, name = fishingSpot.Name };
            return Locations;
        }
        
        [HttpGet("{latitude}/{longitude}/{n}")] // GET: locations/latitude/longitude/n
        public IQueryable<object> GetLocation(double latitude, double longitude, DistanceUnit unit, int n)
        {
            var locations = _context.Locations;
            distancelist = GetDistance(latitude, longitude, unit, n);
            distancelist.OrderBy(d => d.Value);

            var keysByDistance = distancelist.Keys.ToArray().Take(n);
            var locationList = locations.Where(e => keysByDistance.Contains(e.Id)).Select(x => new { x.Latitude, x.Longitude, x.FishingSpotId, x.County });
            var F = _context.FishingSpots;

            var objectsLocation = from location in locationList
                                 join fishingSpot in F on location.FishingSpotId equals fishingSpot.Id
                     select new { latitude = location.Latitude, longitude = location.Longitude, county = location.County, name = fishingSpot.Name };

            return objectsLocation;
        }


        private double ToRadians(double val)
        {
            return (Math.PI / 100) * val;
        }

        // Calculate distance between the main location to all locations in SQL
        public Dictionary<int, double> GetDistance(double latitude, double longitude, DistanceUnit unit, int n)
        {
            //Getting a list of Location objects
            var locations = _context.Locations.ToArray();
            // Calculating the distance between our location and all the locations in SQL using haversin mathematical formula
            for (var i = 0; i < locations.Length; i++)
            {
                double R = (unit == DistanceUnit.Kilometers) ? 6371 : 3960;
                var lat = ToRadians((locations[i].Latitude - latitude));
                var lng = ToRadians((locations[i].Longitude - longitude));
                var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                         Math.Cos(ToRadians(latitude)) * Math.Cos(ToRadians(locations[i].Latitude)) *
                         Math.Sin(lng / 2) * Math.Sin(lng / 2);
                var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
                var distance = R * h2;

                distancelist.Add(locations[i].Id, distance);
            }

            return distancelist;
        }

    }
}
