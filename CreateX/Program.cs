using API.Extentions;
using Core.Extentions;
using CreateX.API.Extentions;
using CreateX.API.MiddleWares;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
app.UseMiddleware<GlobalErrorMiddleware>();
app.UseSerilogRequestLogging(); // Log HTTP requests

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
