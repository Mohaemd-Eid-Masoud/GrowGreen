using AutoMapper;
using GrowGreen.Application.DTOs;
using GrowGreen.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowGreen.Application.Mapping
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CartItem, CartDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();

        }
    }
}
