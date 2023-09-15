using AutoMapper;
using PocDDD.Application.DTOs;
using PocDDD.Domain.Entities;

namespace PocDDD.Application.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping() 
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.OrderDTOs, src => src.MapFrom(opt => opt.Orders));
            CreateMap<UserDTO, User>();
            CreateMap<User, UserToInsertDTO>().ReverseMap();

            CreateMap<Order, OrderDTO>().ReverseMap();

        }
    }
}
