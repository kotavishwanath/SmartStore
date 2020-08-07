using smartStoreApi.Common;
using smartStoreApi.Models.Configuration;
using smartStoreApi.Models.Response;
using smartStoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace smartStoreApi.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly JwtDetails _jwtDetails;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticateService(IOptions<JwtDetails> jwtDetails, IHttpContextAccessor httpContextAccessor)
        {
            _jwtDetails = jwtDetails.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GenerateSecurityToken(string email, int userId,string firstName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtDetails.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimConstants.Email, email),
                    new Claim(ClaimConstants.UserId, userId.ToString()),
                    new Claim(ClaimConstants.FirstName, firstName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtDetails.ExpirationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = "localhost",
                Issuer = "localhost"
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public int GetUserId()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers[AppConstants.AuthorizationHeader].ToString().Replace(JwtBearerDefaults.AuthenticationScheme, string.Empty).Trim();

            var handler = new JwtSecurityTokenHandler();
            return int.Parse(handler.ReadJwtToken(token).Claims?.FirstOrDefault(c => c.Type == "UserId").Value);
        }

        public AuthenticationResponse GetAuthenticatedUser()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers[AppConstants.AuthorizationHeader].ToString().Replace(JwtBearerDefaults.AuthenticationScheme, string.Empty).Trim();

            var handler = new JwtSecurityTokenHandler();
            var claimsResponse = handler.ReadJwtToken(token).Claims;
            return new AuthenticationResponse
            {
                UserId = int.Parse(claimsResponse?.FirstOrDefault(c => c.Type == ClaimConstants.UserId).Value),
                Name = claimsResponse?.FirstOrDefault(c => c.Type == ClaimConstants.FirstName).Value,
                Email = claimsResponse?.FirstOrDefault(c => c.Type == ClaimConstants.Email).Value,
            };
        }
    }
}