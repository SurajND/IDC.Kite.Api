using IDC.Kite.Business.Dto.v1.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IDC.Kite.Business.Versions.v1.User
{
    public interface IUserFacade
    {
        Task<List<UserDto>> Get();
        Task<UserDto> Post(UserDto user);
        Task<UserDto> Authenticate(string username, string password);
        Task<UserDto> GetById(Guid id);
    }
}
