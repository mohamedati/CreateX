
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Services;
using Createx.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace Services.Implemnetation
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration config;
        private readonly ICacheService cache;

        public TokenService(IConfiguration config,ICacheService cache)
        {
            this.config = config;
            this.cache = cache;
        }
    

        public string GenereateToken(ApplicationUser user)
        {
      
            var claims = new[]
            {
            new Claim("Sub", user.Email),
            new Claim("uid", user.Id.ToString())
                  };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: config["JwtSettings:Issuer"],
                audience: config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenereateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        public string GenereateOTP()
        {
            var random = new Random();
            string otp = string.Empty;

            for (int i = 0; i < 6; i++)
            {
                otp += random.Next(0, 10); // توليد رقم من 0 إلى 9
            }

            return otp;
        }

        public async Task< bool> ValidateOtpAsync(string Email, string OTP)
        {
            var value=await cache.GetFromCache($"otp:{Email}");

            if(value!=null && value.Trim('"') == OTP)
            {
                return true;
            }

            return false;

        }
    }
    
}
