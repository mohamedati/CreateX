using API.Attributes;
using API.Extentions;
using Application.Areas.Product.Commands;
using Application.Areas.Product.Commands.CreateProduct;
using Application.Areas.Product.Commands.DeleteProduct;

using Application.Areas.Product.Queries;
using Application.Areas.Product.Queries.GetPaginatedProducts;
using Core.Attributes;
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
    [Tags("Product")]
    public class ProductController(ISender sender) : ControllerBase
    {


        /// <summary>
        /// Get Products Form Database as Paginated List
        /// </summary>
        /// <returns> Pagination List Of Products</returns>


        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<Product>), 200)]
        [ProducesResponseType(500)]
        [Cached(600)]
        public async Task<IActionResult> Paginate([FromQuery] GetPaginatedProducts query)
        {
            return await sender.Send(query).ToGenericResponse();
        }





        /// <summary>
        /// Add Product To Database
        /// </summary>
       



        [ProducesResponseType(typeof(PaginatedList<Product>), 200)]
        [ProducesResponseType(401)]
        [HttpPost]


        public async Task<IActionResult> Add([FromForm]CreateProductCommand Command)
        {
            return await sender.Send(Command).ToGenericResponse();
        }



        /// <summary>
        /// Update  Product in  Database
        /// </summary>
  
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update([FromForm]UpdateProductCommand Command)
        {
            return await sender.Send(Command).ToGenericResponse();
        }


        /// <summary>
        /// Delete   Product From  Database
        /// </summary>
        [HttpDelete("{ID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommand Command)
        {
            return await sender.Send(Command).ToGenericResponse();
        }
    }

}