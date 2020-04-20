using AutoMapper;
using KoronaZakupy.Entities;
using KoronaZakupy.Models;

namespace KoronaZakupy.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterModel, Entities.UserDb.User>();
            CreateMap<Entities.UserDb.User, UserDTO>();
        }
            

    }
}
