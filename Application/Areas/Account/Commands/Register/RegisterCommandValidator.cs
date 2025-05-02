using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Areas.Account.Commands.Register;
using Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Areas.Account.Commands.Login
{
    public  class RegisterCommandValidator:AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Email)
               .NotEmpty()
               .WithMessage(localizer["Required"])
               .EmailAddress()
               .WithMessage(localizer["InvalidEmail"]);


            RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(localizer["Required"]);

            RuleFor(x => x.PhoneNumber)
                
                .NotEmpty()
                .WithMessage(localizer["Required"]);






        }
    }
}
