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
    public class OperationalCompaniesController : ControllerBase
    {
        private readonly KiteContext _context;

        public OperationalCompaniesController(KiteContext context)
        {
            _context = context;
        }

        // GET: api/OperationalCompanies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationalCompany>>> GetOperationalCompanies()
        {
            return await _context.OperationalCompanies.ToListAsync();
        }

        // GET: api/OperationalCompanies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationalCompany>> GetOperationalCompany(Guid id)
        {
            var operationalCompany = await _context.OperationalCompanies.FindAsync(id);

            if (operationalCompany == null)
            {
                return NotFound();
            }

            return operationalCompany;
        }

        // PUT: api/OperationalCompanies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperationalCompany(Guid id, OperationalCompany operationalCompany)
        {
            if (id != operationalCompany.Id)
            {
                return BadRequest();
            }

            _context.Entry(operationalCompany).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperationalCompanyExists(id))
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

        // POST: api/OperationalCompanies
        [HttpPost]
        public async Task<ActionResult<OperationalCompany>> PostOperationalCompany(OperationalCompany operationalCompany)
        {
            _context.OperationalCompanies.Add(operationalCompany);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOperationalCompany", new { id = operationalCompany.Id }, operationalCompany);
        }

        // DELETE: api/OperationalCompanies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationalCompany>> DeleteOperationalCompany(Guid id)
        {
            var operationalCompany = await _context.OperationalCompanies.FindAsync(id);
            if (operationalCompany == null)
            {
                return NotFound();
            }

            _context.OperationalCompanies.Remove(operationalCompany);
            await _context.SaveChangesAsync();

            return operationalCompany;
        }

        private bool OperationalCompanyExists(Guid id)
        {
            return _context.OperationalCompanies.Any(e => e.Id == id);
        }
    }
}
