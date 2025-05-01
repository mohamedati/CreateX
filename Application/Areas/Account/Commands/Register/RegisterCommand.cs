
using Application.Common.Interfaces;
using Application.Resources;
using Application.Services;
using Createx.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Application.Areas.Account.Commands.Register
{
    public class RegisterCommand:IRequest
    {
     
        public string Email { get; set; }

     
        public string PhoneNumber { get; set; }

        public string Password { get; set; }
    }

    public class RegisterCommandHandler(IAppDbContext appDbContext,
        IStringLocalizer<Resource> localizer,
        SignInManager<ApplicationUser> _signInManager,
        UserManager<ApplicationUser> _userManager,
        ITokenService tokenService) 
        : IRequestHandler<RegisterCommand>
    {
        public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var isExist = await _userManager.FindByEmailAsync(request.Email);
            if (isExist !=null)
            {
                throw new  Exception(localizer["Emailaddressisinuse"]);
            }
           
            var user = new ApplicationUser()
            {
                Email = request.Email,
                UserName = request.Email.Split("@")[0],
                PhoneNumber = request.PhoneNumber,
                RefreshToken="",
                RefreshTokenExpiresAt=DateTime.Now
              
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new Exception(localizer["ErrorInRegister"]);
            }
          
          
        }
    }

}
