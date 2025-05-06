using Application.Areas.City.Commands.CreateCity;
using Application.Areas.City.Commands.UpdateCity;
using Application.Areas.Product.Commands;
using Application.Areas.Product.Commands.CreateProduct;
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
            CreateMap<CreateProductCommand, Product>().ReverseMap();
            CreateMap<UpdateProductCommand, Product>().ReverseMap();
        
        }
    }
}
