﻿using AutoMapper;
using UdemyAuthServer.Core.DTOs;
using UdemyAuthServer.Core.Entities;

namespace UdemyAuthServer.Service.Mapping
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<UserAppDto, UserApp>().ReverseMap();

        }
    }
}
