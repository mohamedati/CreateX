using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Validation;
using FluentValidation;

namespace Application.Common.Extensions
{
    public static  class CustomValidationExtension
    {
        //Extension Method To Check Agains Sql Injection
     public static IRuleBuilderOptions<T, string?> IsSqlInjected<T>( this IRuleBuilderOptions<T, string?> Rule)
        {
            return Rule.Must(a => a == null || !SqlInjectionValidator.IsSqlInjection(a));
        }
    }
}
