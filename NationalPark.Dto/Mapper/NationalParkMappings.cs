using AutoMapper;
using NationalPark.Dto.Request;
using NationalPark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalPark.Dto.Mapper
{
    public class NationalParkMappings : Profile
    {
        public NationalParkMappings()
        {
            CreateMap<Park, ParkRequestDto>().ReverseMap();
            CreateMap<Trail, TrailRequestDto>().ReverseMap();
            CreateMap<Trail, TrailCreateDto>().ReverseMap();
            CreateMap<Trail, TrailUpdateDto>().ReverseMap();
        }
    }
}
