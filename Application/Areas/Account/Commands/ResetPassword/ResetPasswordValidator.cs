using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Areas.Account.Commands.ResetPassword
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        private readonly IStringLocalizer<Resource> localizer;

        public ResetPasswordValidator(IStringLocalizer<Resource> localizer)
        {
            this.localizer = localizer;
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(localizer["Required"])
                .EmailAddress()
               .WithMessage(localizer["InvalidEmail"])
                 .IsSqlInjected()
            .WithMessage(localizer["ContainRefusedWords"]);

            RuleFor(x => x.NewPassword)
              .NotEmpty()
              .WithMessage(localizer["Required"])
                .IsSqlInjected()
            .WithMessage(localizer["ContainRefusedWords"]);

            RuleFor(x => x.Token)
           .NotEmpty()
           .WithMessage(localizer["Required"])
             .IsSqlInjected()
            .WithMessage(localizer["ContainRefusedWords"]);
        }
    }
}
