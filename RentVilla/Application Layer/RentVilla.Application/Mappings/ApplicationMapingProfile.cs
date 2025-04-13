using RentVilla.Application.Common.Mapper;
using RentVilla.Application.Dtos.Users;
using RentVilla.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Mappings
{
    public static class ApplicationMapingProfile
    {
        public static void Configure(IMapperConfiguration mapper)
        {
            mapper.CreateMap<User, UserDto>();
        }
    }
}
