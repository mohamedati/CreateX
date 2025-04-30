using API.Attributes;
using Application.Areas.City.Commands;
using Application.Areas.City.Queries;
using Core.Classes;
using Createx.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Areas
{
    [Route("api/[controller]")]
    [ApiController]

 
    public class CityController(ISender sender) : ControllerBase
    {
        [HttpGet]
        public async Task<PaginatedList<City>> Paginate([FromQuery]GetPaginatedCities query)
        {
            return  await sender.Send(query);
        }

        [HttpPost]
        public async Task Add( CreateCity Command)
        {
             await sender.Send(Command);
        }
    }
}
