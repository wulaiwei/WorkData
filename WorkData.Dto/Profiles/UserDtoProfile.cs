﻿using AutoMapper;
using EFDTO.Entity;
using System.Linq;
using WorkData.EF.Domain.Entity;

namespace WorkData.Dto.Profiles
{
    public class UserDtoProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<UserDto, User>()
                .ForMember(d => d.Roles, map => map.MapFrom(m => m.Roles.ToList()))
                .ForMember(d => d.Privileges, map => map.MapFrom(m => m.Privileges.ToList()));

            CreateMap<User, UserDto>()
                .ForMember(d => d.Roles, map => map.MapFrom(m => m.Roles.ToList()))
                .ForMember(d => d.Privileges, map => map.MapFrom(m => m.Privileges.ToList()));
        }
    }
}