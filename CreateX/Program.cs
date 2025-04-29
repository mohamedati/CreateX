using API.Extentions;
using Core.Extentions;
using CreateX.API.Extentions;
using CreateX.API.MiddleWares;
using HealthChecks.UI.Client;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();






builder.Services.RegisterApplicatonLayerService(builder.Configuration); //Register Services and DbContext Pool

builder.Services.RegisterRedis(builder.Configuration);

builder.Services.RegisterIdentity();//Add Identity Configuration

builder.Services.RegisterJWTService(builder.Configuration);

builder.Services.SwaggerRegister();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog(); // Use Serilog for ASP.NET Core logging


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
 
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = string.Empty; // Swagger UI تظهر مباشرة على /
           


        });

  


}


var options = new RequestLocalizationOptions()
    .SetDefaultCulture("en")
    .AddSupportedCultures("en", "ar")
    .AddSupportedUICultures("en", "ar");

app.UseRequestLocalization(options);

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health", new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

    endpoints.MapHealthChecksUI(options =>
    {
        options.UIPath = "/health-ui";
        options.ApiPath = "/health-json";
    });

    endpoints.MapControllers(); // مهم جداً لو عندك API Controllers

});

app.UseMiddleware<GlobalErrorMiddleware>();

app.UseSerilogRequestLogging(); // Log HTTP requests

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
