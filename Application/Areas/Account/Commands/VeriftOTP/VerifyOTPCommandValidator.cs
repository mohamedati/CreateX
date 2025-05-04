using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Areas.Account.Commands.VeriftOTP
{
    public  class VerifyOTPCommandValidator:AbstractValidator<VerifyOTPCommand>
    {
        private readonly IStringLocalizer<Resource> localizer;

        public VerifyOTPCommandValidator(IStringLocalizer<Resource> localizer)
        {
            this.localizer = localizer;
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(localizer["Required"])
                .EmailAddress()
               .WithMessage(localizer["InvalidEmail"])
              .IsSqlInjected()
            .WithMessage(localizer["ContainRefusedWords"]);

            RuleFor(x => x.OTP)
              .NotEmpty()
              .WithMessage(localizer["Required"])
             .IsSqlInjected()
            .WithMessage(localizer["ContainRefusedWords"]);

        }
    }

}
