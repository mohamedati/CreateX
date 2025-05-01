using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Resources;
using Application.Services;
using Createx.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Application.Areas.Account.Commands.Login
{
    public  class LoginCommand:IRequest<UserLoginDTO>
    {

        public string Email {  get; set; }

        public string Password { get; set; }

    }

    public class LoginCommandHandler 
        (IAppDbContext appDbContext,
        IStringLocalizer<Resource> localizer,
        SignInManager<ApplicationUser> _signInManager,
        UserManager<ApplicationUser> _userManager,
        ITokenService tokenService)
        : IRequestHandler<LoginCommand, UserLoginDTO>
    {
        public async Task<UserLoginDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new  UnauthorizedAccessException(localizer["UnAuthorized"]);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException(localizer["InvalidUSerNameOrPassword"]);
            }
            var refreshToken = tokenService.GenereateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiresAt = DateTime.Now.AddDays(3);

           await  _userManager.UpdateAsync(user);


            return new UserLoginDTO
            {
                Email = user.Email,
                Token =  tokenService.GenereateToken(user),
                UserName = user.UserName,
                RefreshToken=refreshToken
            };
        }
    }
}
