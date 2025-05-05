using API.Extentions;
using Application.Areas.Account.Commands.ForgetPassword;
using Application.Areas.Account.Commands.Login;
using Application.Areas.Account.Commands.Register;
using Application.Areas.Account.Commands.ResetPassword;
using Application.Areas.Account.Commands.VeriftOTP;
using Application.Areas.City.Commands;
using Application.Areas.City.Queries;
using Core.Classes;
using Createx.Core.Entities;
using MediatR;

using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Areas
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Account")]
    public class AccountController(ISender sender) : ControllerBase
    {


        /// <summary>
        /// Login Request that accept Email And Password
        /// </summary>
       
        [HttpPost("Login")]
        [ProducesResponseType(typeof(UserLoginDTO), 200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> Login(LoginCommand command)
        {
        return  await sender.Send(command).ToGenericResponse();
         
        }
        /// <summary>
        /// Register Request to register new User
        /// </summary>
        /// <param name="Email">Email</param>
        /// <param name="Password">Password</param>
        /// <param name="PhoneNumber">PhoneNumber</param>
       
        [HttpPost("Register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Register(RegisterCommand Command)
        {
            return await sender.Send(Command).ToGenericResponse();
        }

        /// <summary>
        /// For Forgetting Password
        /// </summary>
        /// <param name="Email">Email</param>
        /// <returns> otp</returns>



        [HttpPost("ForgetPassword")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> ForgetPassword(ForgetPasswordCommand command)
        {
            return await sender.Send(command).ToGenericResponse();
        }

        /// <summary>
        /// OTP Verification
        /// </summary>
        /// <param name="Email">Email</param>
        /// <param name="OTP">OTP</param>
        /// <returns> Token </returns>

        [HttpPost("VerifyOTP")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> VerifyOTP(VerifyOTPCommand Command)
        {
            return await sender.Send(Command).ToGenericResponse();
        }

        /// <summary>
        /// ResetPAssword Command with new PAssword
        /// </summary>
        /// <param name="Email">Email</param>
        /// <param name="Token">Token</param>
        /// <param name="NewPassword">NewPassword</param>
        
        [HttpPost("ResetPassword")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> ResetPassword(ResetPasswordCommand Command)
        {
            return await sender.Send(Command).ToGenericResponse();
        }
    }
}
