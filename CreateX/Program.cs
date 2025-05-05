using System.Reflection;
using API.Extentions;
using Core.Extentions;
using CreateX.API.Extentions;
using CreateX.API.MiddleWares;
using HealthChecks.UI.Client;
using Infrastructure.DbContext;
using Infrastructure.Extentions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
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


var supportedCultures = new[] { "en", "ar" };

var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("en")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

localizationOptions.RequestCultureProviders = new List<IRequestCultureProvider>
{
    new CustomRequestCultureProvider(context =>
    {
        var culture = context.Request.Headers["culture"].FirstOrDefault();
        if (!string.IsNullOrWhiteSpace(culture) && supportedCultures.Contains(culture))
        {
            return Task.FromResult(new ProviderCultureResult(culture, culture));
        }

        return Task.FromResult(new ProviderCultureResult("en", "en"));
    })
};



app.UseMiddleware<GlobalErrorMiddleware>();
app.UseRouting();
app.UseRequestLocalization(localizationOptions);


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
 //For Attacks csrf Xss
app.Use(async (context, next) =>
{
    // Prevent MIME-type sniffing
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");

    // Prevent clickjacking
    context.Response.Headers.Add("X-Frame-Options", "DENY");

    // Control resources that can be loaded by the browser
    context.Response.Headers.Add("Content-Security-Policy", "default-src 'self';");

    // Referrer Policy
    context.Response.Headers.Add("Referrer-Policy", "no-referrer");

    // Strict Transport Security (HSTS)
    context.Response.Headers.Add("Strict-Transport-Security", "max-age=63072000; includeSubDomains; preload");

    await next();
});

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        await IdentityDataInitializer.SeedRolesAsync(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding roles.");
    }
}

app.UseSerilogRequestLogging(); // Log HTTP requests

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Files")
    ),
    RequestPath = "/Files"
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
