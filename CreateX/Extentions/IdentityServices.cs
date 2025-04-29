using Createx.Core.Entities;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity;
using System;

namespace CreateX.API.Extentions
{
    public static class IdentityServices
    {
        public static IServiceCollection RegisterIdentity(this IServiceCollection services)
        {

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;

                // User settings
                options.User.RequireUniqueEmail = true;

                // Token settings
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            })
           .AddEntityFrameworkStores<Context>()
           .AddDefaultTokenProviders();

            return services;
        }
    }
}
