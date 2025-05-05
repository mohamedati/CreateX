using API.Attributes;
using API.Extentions;
using Application.Areas.Account.Commands.Login;
using Application.Areas.City.Commands.CreateCity;
using Application.Areas.City.Commands.DeleteCity;
using Application.Areas.City.Commands.UpdateCity;
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
    [HasPermission]
    [Tags("City")]
    public class CityController(ISender sender) : ControllerBase
    {


        /// <summary>
        /// Get Cities Form Database as Paginated List
        /// </summary>
        /// <returns> Pagination List Of Cities</returns>


        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<City>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Paginate([FromQuery] GetPaginatedCities query)
        {
            return await sender.Send(query).ToGenericResponse();
        }





        /// <summary>
        /// Add City To Database
        /// </summary>
        /// <param name="Name">Email</param>



        [ProducesResponseType(typeof(PaginatedList<City>), 200)]
        [ProducesResponseType(401)]
        [HttpPost]


        public async Task<IActionResult> Add(CreateCity Command)
        {
            return await sender.Send(Command).ToGenericResponse();
        }



        /// <summary>
        /// Update  City in  Database
        /// </summary>
        /// <param name="ID">Email</param>
        /// <param name="Name">Email</param>
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(UpdateCityCommand Command)
        {
            return await sender.Send(Command).ToGenericResponse();
        }


        /// <summary>
        /// Delete   City From  Database
        /// </summary>
        /// <param name="ID">Email</param>
        /// <param name="Name">Email</param>
        [HttpDelete("{ID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete([FromRoute]DeleteCityCommand Command)
        {
            return await sender.Send(Command).ToGenericResponse();
        }
    }
}
