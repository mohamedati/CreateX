using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Areas.City.Commands.UpdateCity
{
    public class UpdateCityCommandValidator:AbstractValidator<UpdateCityCommand>
    {
        private IStringLocalizer<Resource> localizer;

        public UpdateCityCommandValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(localizer["Required"])
            .MaximumLength(50)
            .WithMessage(localizer["MaxLength", 50]);
            this.localizer = localizer;
        }
    }
}
