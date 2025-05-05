using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using Microsoft.OpenApi.Models;

namespace API.Extentions
{
    public static  class RegisterSwagger
    {
        public static IServiceCollection SwaggerRegister(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CreateX API",
                    Version = "v1"
                });

                // إعداد الـ Authorization Header
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT token like: Bearer {your token}"
                });

                options.OperationFilter<SwaggerHeader>();

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
            });

            return services;

        }
      
    }
}
