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
    public class AccountController(ISender sender) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginCommand command)
        {
        return  await sender.Send(command).ToGenericResponse();
         
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterCommand Command)
        {
            return await sender.Send(Command).ToGenericResponse();
        }

        [HttpPost("ForgetPassword")]
        public async Task<ActionResult> ForgetPassword(ForgetPasswordCommand command)
        {
            return await sender.Send(command).ToGenericResponse();
        }

        [HttpPost("VerifyOTP")]
        public async Task<ActionResult> VerifyOTP(VerifyOTPCommand Command)
        {
            return await sender.Send(Command).ToGenericResponse();
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordCommand Command)
        {
            return await sender.Send(Command).ToGenericResponse();
        }
    }
}
