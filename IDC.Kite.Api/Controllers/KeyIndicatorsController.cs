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
    public class KeyIndicatorsController : ControllerBase
    {
        private readonly KiteContext _context;

        public KeyIndicatorsController(KiteContext context)
        {
            _context = context;
        }

        // GET: api/KeyIndicators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KeyIndicator>>> GetKeyIndicators()
        {
            return await _context.KeyIndicators.ToListAsync();
        }

        // GET: api/KeyIndicators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KeyIndicator>> GetKeyIndicator(Guid id)
        {
            var keyIndicator = await _context.KeyIndicators.FindAsync(id);

            if (keyIndicator == null)
            {
                return NotFound();
            }

            return keyIndicator;
        }

        // PUT: api/KeyIndicators/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKeyIndicator(Guid id, KeyIndicator keyIndicator)
        {
            if (id != keyIndicator.Id)
            {
                return BadRequest();
            }

            _context.Entry(keyIndicator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KeyIndicatorExists(id))
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

        // POST: api/KeyIndicators
        [HttpPost]
        public async Task<ActionResult<KeyIndicator>> PostKeyIndicator(KeyIndicator keyIndicator)
        {
            _context.KeyIndicators.Add(keyIndicator);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKeyIndicator", new { id = keyIndicator.Id }, keyIndicator);
        }

        // DELETE: api/KeyIndicators/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<KeyIndicator>> DeleteKeyIndicator(Guid id)
        {
            var keyIndicator = await _context.KeyIndicators.FindAsync(id);
            if (keyIndicator == null)
            {
                return NotFound();
            }

            _context.KeyIndicators.Remove(keyIndicator);
            await _context.SaveChangesAsync();

            return keyIndicator;
        }

        private bool KeyIndicatorExists(Guid id)
        {
            return _context.KeyIndicators.Any(e => e.Id == id);
        }
    }
}
