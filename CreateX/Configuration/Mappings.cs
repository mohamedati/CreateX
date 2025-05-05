using Application.Areas.City.Commands.CreateCity;
using Application.Areas.City.Commands.UpdateCity;
using AutoMapper;
using Createx.Core.Entities;

namespace API.Configuration
{
    public class Mappings:Profile
    {
        public Mappings()
        {
            CreateMap<CreateCity, City>().ReverseMap();
            CreateMap<UpdateCityCommand, City>().ReverseMap();
            CreateMap<City, City>().ReverseMap();
        }
    }
}
