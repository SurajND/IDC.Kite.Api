using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Claros.Instrument.Measurement.Business.Mapping;
using IDC.Kite.Business.Dto.v1.User;
using IDC.Kite.Business.Helper.WebApi.Helpers;
using IDC.Kite.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IDC.Kite.Business.Versions.v1.User
{
    public class UserFacade : IUserFacade
    {
        private readonly KiteContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserFacade(KiteContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _mapper = MappingManager.AutoMapper;
            _appSettings = appSettings.Value;
        }
        public async Task<List<UserDto>> Get()
        {
           var result = await _context.Users.
                Include(x => x.Role).ToListAsync();
            return _mapper.Map<List<Classes.Entity.User>, List<UserDto>>(result);
        }

        public async Task<UserDto> Post(UserDto user)
        {
            var userData = _mapper.Map<UserDto, Classes.Entity.User>(user);
            _context.Users.Add(userData);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<UserDto> Authenticate(string username, string password)
        {
            try
            {
                var user = _context.Users.Include(x => x.Role).SingleOrDefault(x => x.Email == username && x.Password == password);

                // return null if user not found
                if (user == null)
                    return null;

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.Role, user.Role.RoleType)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                // remove password before returning
                user.Password = null;

                var result = _mapper.Map<Classes.Entity.User, UserDto>(user);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<UserDto> GetById(Guid id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            // return user without password
            if (user != null)
                user.Password = null;

            var userDto = _mapper.Map<Classes.Entity.User, UserDto>(user);
            return userDto;
        }
    }
}
