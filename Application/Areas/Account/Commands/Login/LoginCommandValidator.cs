﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Areas.Account.Commands.Login
{
    public  class LoginCommandValidator:AbstractValidator<LoginCommand>
    {
        private readonly IStringLocalizer<Resource> localizer;

        public LoginCommandValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.Email)
               .NotEmpty()
               .WithMessage(localizer["Required"])
               .EmailAddress()
               .WithMessage(localizer["InvalidEmail"])
               .IsSqlInjected()
            .WithMessage(localizer["ContainRefusedWords"]);


            RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(localizer["Required"])
              .IsSqlInjected()
            .WithMessage(localizer["ContainRefusedWords"]);
            this.localizer = localizer;
        }
    }
}
