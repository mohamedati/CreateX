
using Application.Common.Interfaces;
using Application.Resources;
using Application.Services;
using Createx.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Application.Areas.Company.Commands.RegisterComapnyCommand
{
    public class RegisterComapnyCommand : IRequest
    {

        public string CompanyName { get; set; } = null!;


        public string Email { get; set; } = null!;


        public string PhoneNumber { get; set; } = null!;

        public string Password { get; set; } = null!;
    }


    public class RegisterComapnyCommandHandler(
        IAppDbContext appDbContext,
        IStringLocalizer<Resource> localizer,
        SignInManager<ApplicationUser> _signInManager,
        UserManager<ApplicationUser> _userManager
      )
        : IRequestHandler<RegisterComapnyCommand>
    {
        public async Task Handle(RegisterComapnyCommand request, CancellationToken cancellationToken)
        {
            // Check if email exists
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                throw new Exception(localizer["EmailAlreadyExist"]);
           
            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.CompanyName,
                PhoneNumber = request.PhoneNumber,
            
            };
           
                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {

                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    // You can now use 'errors', for example:
                    throw new Exception($"Operation failed: {errors}");
                }

              await _userManager.AddToRoleAsync(user, "Company");
          




        }
    }

}
 

