using Application.Areas.Account.Commands.ForgetPassword;
using Application.Areas.Account.Commands.Login;
using Application.Areas.Account.Commands.Register;
using Application.Areas.Account.Commands.VeriftOTP;
using Application.Areas.City.Commands;
using Application.Areas.City.Queries;
using Core.Classes;
using Createx.Core.Entities;
using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace API.Areas
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(ISender sender) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<UserLoginDTO> Login(LoginCommand command)
        {
            return await sender.Send(command);
        }

        [HttpPost("Register")]
        public async Task Register(RegisterCommand Command)
        {
            await sender.Send(Command);
        }

        [HttpPost("ForgetPassword")]
        public async Task ForgetPassword(ForgetPasswordCommand command)
        {
             await sender.Send(command);
        }

        [HttpPost("VerifyOTP")]
        public async Task<ResetTokenDTO> VerifyOTP(VerifyOTPCommand Command)
        {
            return await sender.Send(Command);
        }
    }
}
