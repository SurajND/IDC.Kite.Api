using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDC.Kite.Business.Dto.v1
{
    class CreateMap
    {
        internal static void Map(IProfileExpression config)
        {
            User.CreateMap.Map(config);
        }
    }
}
