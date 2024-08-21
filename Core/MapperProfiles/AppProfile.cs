﻿using AutoMapper;
using Core.Dtos;
using Data.Entities;

namespace Core.MapperProfiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<CarsDto, Cars>().ReverseMap();

            CreateMap<Order, OrderDto>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(x => x.ProductsCount, opt => opt.MapFrom(src => src.Cars.Count));
        }
    }
}
