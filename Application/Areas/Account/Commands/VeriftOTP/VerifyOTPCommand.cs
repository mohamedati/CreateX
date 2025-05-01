using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Resources;
using Application.Services;
using Createx.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Application.Areas.Account.Commands.VeriftOTP
{
    public class VerifyOTPCommand:IRequest<ResetTokenDTO>
    {
        public string Email {  get; set; }

        public string OTP {  get; set; }


    }

    public class VerifyOTPCommandHandler
         (IStringLocalizer<Resource> localizer,
        SignInManager<ApplicationUser> _signInManager,
        UserManager<ApplicationUser> _userManager,
        ITokenService tokenService) 
        : IRequestHandler<VerifyOTPCommand, ResetTokenDTO>
    {
        public async  Task<ResetTokenDTO> Handle(VerifyOTPCommand request, CancellationToken cancellationToken)
        {

            var isValid = await tokenService.ValidateOtpAsync(request.Email, request.OTP);
            if (!isValid)
                throw new Exception(localizer["InvalidOrExpiredOTP"]);

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(
                await _userManager.FindByEmailAsync(request.Email));

            return new ResetTokenDTO{ ResetToken=resetToken };
        }
    }
}
