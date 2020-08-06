using AutoMapper;

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
