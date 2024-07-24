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
    public class BookletProfile
    {
        CreateMap<BookletDto, Booklet>();
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        CreateMap<ManagerDto, Manager>();
        CreateMap<UserDto, User>();
        CreateMap<Manager, ManagerDto>();    
      
    }
}
