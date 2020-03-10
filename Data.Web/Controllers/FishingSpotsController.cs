using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Domain.Concrete;
using Data.Domain.Entities;

namespace Data.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FishingSpotsController : ControllerBase
    {

        // Create a field to store Data sets
        private readonly EFDbContext _context;

        // Assign the object in the constructor for dependency injection
        public FishingSpotsController(EFDbContext context)
        {
            _context = context;
        }

        // GET: api/FishingSpots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetFishingSpots()
        {
            return await _context.FishingSpots
               .Select(x => new {
                   Name = x.Name, 
                   Description = x.Description, 
                   Licence = x.Licence, 
                   Url = x.Url, 
                   WaterType = x.WaterType,
                   Administration = x.Administration,
                   Tax = x.Tax,
                   FishType = x.FishType
               })
                .ToListAsync();
        }


        // GET: api/FishingSpots/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<FishingSpot>> GetFishingSpot(int id)
        {
            var fishingSpot = await _context.FishingSpots
                .Where(c => c.Id == id)
                .Include("Locations")
                .Include("Reviews")
                .ToListAsync();


            
            
            if (fishingSpot == null)
            {
                return new List<FishingSpot>().AsQueryable();
            }

            return fishingSpot;
        }

        // PUT: api/FishingSpots/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFishingSpot(int id, FishingSpot fishingSpot)
        {
            if (id != fishingSpot.Id)
            {
                return BadRequest();
            }

            _context.Entry(fishingSpot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FishingSpotExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FishingSpots
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<FishingSpot>> PostFishingSpot(FishingSpot fishingSpot)
        {
            _context.FishingSpots.Add(fishingSpot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFishingSpot", new { id = fishingSpot.Id }, fishingSpot);
        }

        // DELETE: api/FishingSpots/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FishingSpot>> DeleteFishingSpot(int id)
        {
            var fishingSpot = await _context.FishingSpots.FindAsync(id);
            if (fishingSpot == null)
            {
                return NotFound();
            }

            _context.FishingSpots.Remove(fishingSpot);
            await _context.SaveChangesAsync();

            return fishingSpot;
        }

        private bool FishingSpotExists(int id)
        {
            return _context.FishingSpots.Any(e => e.Id == id);
        }
    }
}
