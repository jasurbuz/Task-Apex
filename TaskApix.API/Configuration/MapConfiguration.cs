using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApix.Data.Models;
using TaskApix.Dtos.CountryDto;
using TaskApix.Dtos.Region;

namespace TaskApix.API.Configuration
{
    public class MapConfiguration : Profile
    {
        public MapConfiguration()
        {
            CreateMap<Country, CountryCreation>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();

            CreateMap<Region, RegionCreation>().ReverseMap();
            CreateMap<Region, RegionDto>().ReverseMap();
        }
    }
}
