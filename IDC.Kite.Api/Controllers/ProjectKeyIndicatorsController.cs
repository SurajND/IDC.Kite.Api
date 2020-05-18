using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IDC.Kite.Classes.Entity;
using IDC.Kite.DataModel;
using Microsoft.AspNetCore.Cors;

namespace IDC.Kite.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class ProjectKeyIndicatorsController : ControllerBase
    {
        private readonly KiteContext _context;

        public ProjectKeyIndicatorsController(KiteContext context)
        {
            _context = context;
        }

        // GET: api/ProjectKeyIndicators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectKeyIndicator>>> GetProjectKeyIndicators()
        {
            return await _context.ProjectKeyIndicators.ToListAsync();
        }

        // GET: api/ProjectKeyIndicators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectKeyIndicator>> GetProjectKeyIndicator(Guid id)
        {
            var projectKeyIndicator = await _context.ProjectKeyIndicators.FindAsync(id);

            if (projectKeyIndicator == null)
            {
                return NotFound();
            }

            return projectKeyIndicator;
        }

        [HttpGet("{id}/{year}")]
        public async Task<ActionResult<IEnumerable<ProjectKeyIndicator>>> GetProjectKeyIndicatorByProjectId(Guid id, int year)
        {
            var projectKeyIndicator = _context.ProjectKeyIndicators.Where(x =>x.ProjectId == id && x.Year == year)
                .Include(x => x.KeyIndicator)
                .Include(x =>x.Project)
                .ToList();

            if (projectKeyIndicator == null)
            {
                return NotFound();
            }

            return projectKeyIndicator;
        }

        // PUT: api/ProjectKeyIndicators/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectKeyIndicator(Guid id, ProjectKeyIndicator projectKeyIndicator)
        {
            if (id != projectKeyIndicator.Id)
            {
                return BadRequest();
            }

            _context.Entry(projectKeyIndicator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectKeyIndicatorExists(id))
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

        // POST: api/ProjectKeyIndicators
        [HttpPost]
        public async Task<ActionResult<ProjectKeyIndicator>> PostProjectKeyIndicator(ProjectKeyIndicator projectKeyIndicator)
        {
            _context.ProjectKeyIndicators.Add(projectKeyIndicator);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectKeyIndicator", new { id = projectKeyIndicator.Id }, projectKeyIndicator);
        }

        // DELETE: api/ProjectKeyIndicators/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProjectKeyIndicator>> DeleteProjectKeyIndicator(Guid id)
        {
            var projectKeyIndicator = await _context.ProjectKeyIndicators.FindAsync(id);
            if (projectKeyIndicator == null)
            {
                return NotFound();
            }

            _context.ProjectKeyIndicators.Remove(projectKeyIndicator);
            await _context.SaveChangesAsync();

            return projectKeyIndicator;
        }

        private bool ProjectKeyIndicatorExists(Guid id)
        {
            return _context.ProjectKeyIndicators.Any(e => e.Id == id);
        }
    }
}
