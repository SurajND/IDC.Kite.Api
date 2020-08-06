using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IDC.Kite.Classes.Entity;
using IDC.Kite.DataModel;
using IDC.Kite.Business.Versions.v1.User;
using IDC.Kite.Business.Dto.v1.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using UserCredential = IDC.Kite.Business.Dto.v1.User.UserCredential;

namespace IDC.Kite.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly KiteContext _context;
        private readonly IUserFacade _facade;

        public UsersController(KiteContext context, IUserFacade facade)
        {
            _context = context;
            _facade = facade;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserCredential userParam)
        {
            var user = _facade.Authenticate(userParam.UserName, userParam.Password);

            if (user.Result == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        // GET: api/Users
        [HttpGet]
        [Authorize(Roles = RoleDto.Admin)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            try
            {
                return await _facade.Get();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occured while handling the request");
            }
           
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            var user = await _facade.GetById(id);

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, UserDto user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            user.Id = id;

            var userDto = await _facade.PatchTask(user);

            return NoContent();
        }

        // PUT: api/Users/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> ResetPassword(Guid id, [FromQuery] string oldPassword, [FromQuery] string newPassword)
        {
            var success = await _facade.ResetPassword(id, oldPassword, newPassword);
            return Ok(success);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(UserDto user)
        {
            if(user == null)
            {
                return BadRequest();
            }
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _facade.Post(user);

            return CreatedAtAction("GetUser", new { id = result.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
