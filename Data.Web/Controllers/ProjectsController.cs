using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Domain.Concrete;
using Data.Domain.Entities;
using Data.Domain.EntitiesDTO;

namespace Data.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly EFDbContext _context;

        public ProjectsController(EFDbContext context)
        {
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects()
        {
            return await _context.Projects
                .Select(x => ProjectToDTO(x))
                .ToListAsync();
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> GetProject(long id)
        {
            var project = await _context.Projects
                .Where(x => x.Id == id)
                .Select(x => ProjectToDTO(x))
                .SingleAsync();

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(long id, ProjectDTO projectDTO)
        {
            if (id != projectDTO.Id)
            {
                return BadRequest();
            }

            var project = await _context.Projects.FindAsync(id);
            if(project == null)
            {
                return NotFound();
            }
            project.Name = projectDTO.Name;
            project.Description = projectDTO.Description;
            project.IsComplete = projectDTO.IsComplete;
            project.Url = projectDTO.Url;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) when (!ProjectExists(id))
            {
                return NotFound();
            }
            return NoContent();

        }

        // POST: api/Projects
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject(ProjectDTO projectDTO)
        {
            var project = new Project
            {
                Name = projectDTO.Name,
                IsComplete = projectDTO.IsComplete,
                Description = projectDTO.Description,
                Url = projectDTO.Url
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetProject),
                new { id = project.Id },
                ProjectToDTO(project));
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Project>> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(long id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
        private static ProjectDTO ProjectToDTO(Project project) => new ProjectDTO
       {
           Id = project.Id,
           Name = project.Name,
           IsComplete = project.IsComplete,
           Description = project.Description,
           Url = project.Url
       };
    }
}
