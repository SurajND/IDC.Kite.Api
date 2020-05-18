using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDC.Kite.Business.Dto.v1.User;
using IDC.Kite.Business.Versions.v1.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IDC.Kite.Classes.Entity;
using IDC.Kite.DataModel;
using Microsoft.AspNetCore.Authorization;

namespace IDC.Kite.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly KiteContext _context;

        public PermissionsController(KiteContext context)
        {
            _context = context;
        }

        // GET: api/Permissions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Permission>>> GetPermissions()
        {
            return await _context.Permissions.ToListAsync();
        }

        // GET: api/Permissions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Permission>> GetPermission(Guid id)
        {
            var permission = await _context.Permissions.FindAsync(id);

            if (permission == null)
            {
                return NotFound();
            }

            return permission;
        }

        // PUT: api/Permissions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermission(Guid id, Permission permission)
        {
            if (id != permission.Id)
            {
                return BadRequest();
            }

            _context.Entry(permission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionExists(id))
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

        // POST: api/Permissions
        [HttpPost]
        public async Task<ActionResult<Permission>> PostPermission(Permission permission)
        {
            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPermission", new { id = permission.Id }, permission);
        }

        // DELETE: api/Permissions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Permission>> DeletePermission(Guid id)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission == null)
            {
                return NotFound();
            }

            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();

            return permission;
        }

        private bool PermissionExists(Guid id)
        {
            return _context.Permissions.Any(e => e.Id == id);
        }
    }
}
