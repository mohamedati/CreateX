using Application.Areas.City.Commands;
using AutoMapper;
using Createx.Core.Entities;

namespace API.Configuration
{
    public class Mappings:Profile
    {
        public Mappings()
        {
            CreateMap<CreateCity, City>().ReverseMap();
            CreateMap<City, City>().ReverseMap();
        }
    }
}
