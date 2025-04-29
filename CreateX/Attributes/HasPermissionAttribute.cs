using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Createx.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Services.Services;

namespace API.Attributes
{
    public class HasPermissionAttribute: Attribute, IAsyncAuthorizationFilter
    {


        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                throw new UnauthorizedAccessException("Missing or invalid Authorization header.");
            }

            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var tokenHandler = new JwtSecurityTokenHandler();

            if (!ValidateToken(token))
            {
                var jwtToken = tokenHandler.ReadJwtToken(token);

                // الوصول للـ Claims
                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "uid").Value;

                var service = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();

               var user=await service.FindByIdAsync(userIdClaim);

                if (user.RefreshTokenExpiresAt<=DateTime.Now)
                {
                    throw new UnauthorizedAccessException("Refresh token and Auth expired.");
                }
                else
                {
                    var tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();

                    var newAccessToken = tokenService.GenereateToken(user);

                   
                    context.HttpContext.Response.Headers.Add("X-New-Access-Token", newAccessToken);
                }
           
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