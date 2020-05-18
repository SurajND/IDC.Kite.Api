using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDC.Kite.Business.Dto.v1.User
{
    internal static class CreateMap
    {
        internal static void Map(IProfileExpression config)
        {
            config.CreateMap<Classes.Entity.User, UserDto>().ReverseMap();
        }
    }
}
