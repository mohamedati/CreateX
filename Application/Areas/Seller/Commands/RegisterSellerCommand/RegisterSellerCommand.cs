
using Application.Common.Interfaces;
using Application.Resources;
using Application.Services;
using Createx.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Application.Areas.Company.Commands.RegisterComapnyCommand
{
    public class RegisterSellerCommand : IRequest
    {

        public string SellerName { get; set; } = null!;


        public string Email { get; set; } = null!;


        public string PhoneNumber { get; set; } = null!;

        public string Password { get; set; } = null!;
    }


    public class RegisterSellerCommandHandler(
        IAppDbContext appDbContext,
        IStringLocalizer<Resource> localizer,
        SignInManager<ApplicationUser> _signInManager,
        UserManager<ApplicationUser> _userManager
      )
        : IRequestHandler<RegisterSellerCommand>
    {
        public async Task Handle(RegisterSellerCommand request, CancellationToken cancellationToken)
        {
            // Check if email exists
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                throw new Exception(localizer["AlreadyExist"]);
           
            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.SellerName,
                PhoneNumber = request.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                // You can now use 'errors', for example:
                throw new Exception($"Operation failed: {errors}");
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "Seller");

        }
    }

}
 

