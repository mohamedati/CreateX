using API.Attributes;
using API.Extentions;
using Application.Areas.Company.Commands.RegisterComapnyCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Areas
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController(ISender sender) : ControllerBase
    {



        /// <summary>
        /// End Point to Register New Seller Account
        /// </summary>
        /// <param name="Email">Email</param>
        /// <param name="Password">Password</param>
        /// <param name="PhoneNumber">PhoneNumber</param>
        [HttpPost("register")]
        
        public async Task<ActionResult> RegisterSeller(RegisterSellerCommand command)
        {
            return await sender.Send(command).ToGenericResponse();
        }

    }
}