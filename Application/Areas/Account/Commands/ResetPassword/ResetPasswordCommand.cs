
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Resources;
using Application.Services;
using Createx.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Application.Areas.Account.Commands.ResetPassword
{
    public  class ResetPasswordCommand:IRequest
    {
        public string NewPassword { get; set; } = null!;

        public string Email { get;  set; } = null!;

        public string Token {  get; set; } = null!;


    }

    public class ResetPasswordCommandHandler
        (IAppDbContext appDbContext,
        IStringLocalizer<Resource> localizer,
        SignInManager<ApplicationUser> _signInManager,
        UserManager<ApplicationUser> _userManager,
        ITokenService tokenService): IRequestHandler<ResetPasswordCommand>
    {
        public async  Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
                throw new ItemNotFoundException(localizer["NotFound"]);

           var result=await _userManager.ResetPasswordAsync(
                user,
                    request.Token,
                request.NewPassword
            
                );

            if (!result.Succeeded)
                throw new Exception(localizer["ResetPasswordNotDone"]);
        }
    }
}
