﻿using AutoMapper;
using KoronaZakupy.Entities;
using KoronaZakupy.Models;
using KoronaZakupy.Services.Interfaces;

namespace KoronaZakupy.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterModel, Entities.UserDb.User>();

            CreateMap<Entities.UserDb.User, UserDTO>()
                .ForMember(dest => dest.UserId,
                    opt => opt.MapFrom(src => src.Id));     
        }
            

    }
}
