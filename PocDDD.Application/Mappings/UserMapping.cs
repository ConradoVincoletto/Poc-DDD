using AutoMapper;
using PocDDD.Application.DTOs;
using PocDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocDDD.Application.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping() 
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, PaginationDTO>().ReverseMap();
            CreateMap<User, ServiceResponseDTO<int>>().ReverseMap();
            CreateMap<User, UserToInsertDTO>().ReverseMap();

        }
    }
}
