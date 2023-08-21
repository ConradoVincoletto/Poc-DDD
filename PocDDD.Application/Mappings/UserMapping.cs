using AutoMapper;
using PocDDD.Application.DTOs;
using PocDDD.Domain.Entities;

namespace PocDDD.Application.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping() 
        {
            CreateMap<User, UserDTO>().ReverseMap();            
            CreateMap<User, UserToInsertDTO>().ReverseMap();

        }
    }
}
