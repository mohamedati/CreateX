using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Areas.Company.Commands.RegisterComapnyCommand
{
    public  class RegisterCompanyCommandValidator:AbstractValidator<RegisterComapnyCommand>
    {
        private readonly IStringLocalizer<Resource> localizer;

        public RegisterCompanyCommandValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .WithMessage(localizer["Required"]);

            RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(localizer["Required"]);
            RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(localizer["Required"]);
            RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage(localizer["Required"]);

            this.localizer = localizer;
        }
    }
}
