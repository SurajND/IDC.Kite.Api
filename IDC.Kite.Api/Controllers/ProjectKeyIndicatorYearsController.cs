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
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class ProjectKeyIndicatorYearsController : ControllerBase
    {
        private readonly KiteContext _context;

        public ProjectKeyIndicatorYearsController(KiteContext context)
        {
            _context = context;
        }

        // GET: api/ProjectKeyIndicatorYears
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectKeyIndicatorYear>>> GetProjectKeyIndicatorYears()
        {
            return await _context.ProjectKeyIndicatorYears.ToListAsync();
        }

        // GET: api/ProjectKeyIndicatorYears/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectKeyIndicatorYear>> GetProjectKeyIndicatorYear(Guid id)
        {
            var projectKeyIndicatorYear = await _context.ProjectKeyIndicatorYears.FindAsync(id);

            if (projectKeyIndicatorYear == null)
            {
                return NotFound();
            }

            return projectKeyIndicatorYear;
        }

        // PUT: api/ProjectKeyIndicatorYears/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectKeyIndicatorYear(Guid id, ProjectKeyIndicatorYear projectKeyIndicatorYear)
        {
            if (id != projectKeyIndicatorYear.Id)
            {
                return BadRequest();
            }

            _context.Entry(projectKeyIndicatorYear).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectKeyIndicatorYearExists(id))
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

        // POST: api/ProjectKeyIndicatorYears
        [HttpPost]
        public async Task<ActionResult<ProjectKeyIndicatorYear>> PostProjectKeyIndicatorYear(ProjectKeyIndicatorYear projectKeyIndicatorYear)
        {
            _context.ProjectKeyIndicatorYears.Add(projectKeyIndicatorYear);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectKeyIndicatorYear", new { id = projectKeyIndicatorYear.Id }, projectKeyIndicatorYear);
        }

        // DELETE: api/ProjectKeyIndicatorYears/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProjectKeyIndicatorYear>> DeleteProjectKeyIndicatorYear(Guid id)
        {
            var projectKeyIndicatorYear = await _context.ProjectKeyIndicatorYears.FindAsync(id);
            if (projectKeyIndicatorYear == null)
            {
                return NotFound();
            }

            _context.ProjectKeyIndicatorYears.Remove(projectKeyIndicatorYear);
            await _context.SaveChangesAsync();

            return projectKeyIndicatorYear;
        }

        private bool ProjectKeyIndicatorYearExists(Guid id)
        {
            return _context.ProjectKeyIndicatorYears.Any(e => e.Id == id);
        }
    }
}
