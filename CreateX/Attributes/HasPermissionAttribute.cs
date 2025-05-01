using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Createx.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;

using Application.Resources;
using Application.Services;
namespace API.Attributes
{
    public class HasPermissionAttribute: Attribute, IAsyncAuthorizationFilter
    {


        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var LangService = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<Resource>>();
            var authorizationHeader = "Bearer "+context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                throw new UnauthorizedAccessException(LangService["InvalidAuthorizationHeader"]);
            }

            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken validatedToken;
            var key = Encoding.UTF8.GetBytes("VerySecretKeyForJwtGeneration123456789");

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out validatedToken);

                // Token is valid
                return;
            }
            catch (SecurityTokenExpiredException)
            {
                // Token expired — try to validate refresh token
                var jwtToken = tokenHandler.ReadJwtToken(token);
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    throw new UnauthorizedAccessException(LangService["InvalidToken"]);
                }

                var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
                var user = await userManager.FindByIdAsync(userId);

                if (user == null || user.RefreshTokenExpiresAt <= DateTime.UtcNow)
                {
                    throw new UnauthorizedAccessException(LangService["AllTokensExpired"]);
                }

                // Issue new token
                var tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();
                var newAccessToken = tokenService.GenereateToken(user);

                // إعداد الاستجابة مباشرة (بدلاً من الاستمرار)
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.HttpContext.Response.ContentType = "application/json";

                var response = new
                {
                    message = LangService["AccessTokenRefreshed"].Value,
                    newAccessToken = newAccessToken
                };

                var json = System.Text.Json.JsonSerializer.Serialize(response);
                await context.HttpContext.Response.WriteAsync(json);

                // منع الاستمرار (مافيش return لأنك بتكتب في Response مباشرة)
                return;
            }
            catch
            {
                // Invalid for other reasons
                throw new UnauthorizedAccessException(LangService["InvalidToken"]);
            }
        
        }

   

        private bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("VerySecretKeyForJwtGeneration123456789");

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false, // You can enable/disable as needed
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    }