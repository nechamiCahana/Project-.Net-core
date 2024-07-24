using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Dtos;
using MODELS.Models;
using AutoMapper;


namespace DAL.Profiles
{
    public class BookletProfile:Profile
    {
        public BookletProfile()
        {
            CreateMap<Booklet,BookletDto>().ReverseMap();
            CreateMap<BookletDto, Booklet>().ReverseMap();
            CreateMap<Orders,OrderDto>().ReverseMap();
            CreateMap<OrderDto, Orders>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<ManagerDto, Manager>();
            CreateMap<Manager, ManagerDto>();
        }
       
    }
}
