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

        // GET: api/ProjectKeyIndicatorByAllOpco
        [Route("ProjectKeyIndicatorByAllOpco")]
        public async Task<ActionResult> ProjectKeyIndicatorByAllOpco()
        {
            var indicatorsByOpco = await _context.OperationalCompanies.Select(v => new
            {
                opco = v.Id,
                value = _context.ProjectKeyIndicatorYears.Include(y => y.Project)
                    .Where(p => p.Project.OperationalCompanyId == v.Id).GroupBy(x => x.KeyIndicator).ToList()
            }).ToListAsync();

            return Ok(indicatorsByOpco);
        }

        //
        // GET: api/ProjectKeyIndicatorByAllOpcoForAYear
        [HttpGet("ProjectKeyIndicatorByAllOpcoForAYear")]
        public async Task<ActionResult> ProjectKeyIndicatorByAllOpcoForAYear([FromQuery] int year)
        {
            var indicatorsByOpco = await _context.OperationalCompanies.Select(v => new
            {
                opco = v.Id,
                OperationalCompanyName = v.OperationalCompanyName,
                value = _context.ProjectKeyIndicatorYears.Include(y => y.Project)
                    .Where(p => p.Project.OperationalCompanyId == v.Id && p.Year == year)
                    .GroupBy(x => x.KeyIndicator)
                    .Select(x => new { keyIndicatorName = x.Key.Indicator, value = x.Average(y => y.Value), keyIndicatorId = x.Key.Id })
                    .ToList()
            }).ToListAsync();

            return Ok(indicatorsByOpco);
        }

        // GET: api/ProjectKeyIndicatorForAnOpco
        [Route("ProjectKeyIndicatorForAnOpco")]
        [HttpGet]
        public async Task<ActionResult> ProjectKeyIndicatorForAnOpco([FromQuery] Guid opco)
        {
            var opcoIndicator = new
            {
                opco,
                value = await _context.ProjectKeyIndicatorYears.Include(y => y.Project)
                    .Where(p => p.Project.OperationalCompanyId == opco)
                    .GroupBy(x => x.KeyIndicator)
                    .Select(x => new { keyIndicatorId = x.Key.Id, keyIndicatorName = x.Key.Indicator, value = x.Average(y => y.Value) })
                    .ToListAsync()
            };

            return Ok(opcoIndicator);
        }

        //
        // GET: api/ProjectKeyIndicatorForAnOpcoForAYear
        [Route("ProjectKeyIndicatorForAnOpcoForAYear")]
        [HttpGet]
        public async Task<ActionResult> ProjectKeyIndicatorForAnOpcoForAYear([FromQuery] Guid opco, [FromQuery] int year)
        {
            var opcoIndicator = new
            {
                opco,
                value = await _context.ProjectKeyIndicatorYears.Include(y => y.Project)
                    .Where(p => p.Project.OperationalCompanyId == opco && p.Year == year)
                    .GroupBy(x => x.KeyIndicator)
                    .Select(x => new { keyIndicatorId = x.Key.Id, keyIndicatorName = x.Key.Indicator, value = x.Average(y => y.Value) })
                    .ToListAsync()
            };

            return Ok(opcoIndicator);
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
