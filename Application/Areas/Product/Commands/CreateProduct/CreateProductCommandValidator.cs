using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.Resources;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace Application.Areas.Product.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {

        public CreateProductCommandValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(x => x.NameAr)
            .NotEmpty()
            .WithMessage(localizer["Required"])
            .MaximumLength(50)
            .WithMessage(localizer["MaxLength", 50])
            .IsSqlInjected()
            .WithMessage(localizer["ContainRefusedWords"]);

            RuleFor(x => x.NameEn)
          .NotEmpty()
          .WithMessage(localizer["Required"])
          .MaximumLength(50)
          .WithMessage(localizer["MaxLength", 50])
          .IsSqlInjected()
            .WithMessage(localizer["ContainRefusedWords"]);

            RuleFor(x => x.StoreId)
    .NotEmpty()
    .WithMessage(localizer["Required"]);



            RuleFor(x => x.SalePrice)
    .NotEmpty()
    .WithMessage(localizer["Required"]);


            RuleFor(x => x.ProductId)
    .NotEmpty()
    .WithMessage(localizer["Required"]);



            RuleFor(x => x.PurchasePrice)
    .NotEmpty()
    .WithMessage(localizer["Required"]);


            RuleFor(x => x.Barcode)
 .NotEmpty()
 .WithMessage(localizer["Required"]);



            RuleFor(x => x.TypeId)
 .NotEmpty()
 .WithMessage(localizer["Required"]);


            RuleFor(x => x.Photo)
                    .NotEmpty()
                    .WithMessage(localizer["Required"]);


        }
    }
}
