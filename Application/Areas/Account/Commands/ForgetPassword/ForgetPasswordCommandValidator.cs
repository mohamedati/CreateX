using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Areas.Account.Commands.ForgetPassword
{
    public  class ForgetPasswordCommandValidator:AbstractValidator<ForgetPasswordCommand>
    {
        private readonly IStringLocalizer<Resource> localizer;

        public ForgetPasswordCommandValidator(IStringLocalizer<Resource> localizer)
        {
            this.localizer = localizer;
            RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(localizer["Required"])
            .IsSqlInjected()
            .WithMessage(localizer["ContainRefusedWords"])
            ;
        }
    }
}
