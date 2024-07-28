using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Livros.Domain.Service;
using Livros.Infra.CrossCutting.Configuration;

namespace Livros.Application.Service
{ 
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private TokenConfiguration _tokenConfiguration;

        public TokenService(IConfiguration configuration, TokenConfiguration tokenConfiguration)
        {
            _configuration = configuration;
            _tokenConfiguration = tokenConfiguration;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            
            var options = new JwtSecurityToken(
                issuer: _tokenConfiguration.Issuer,
                audience: _tokenConfiguration.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(7),
                signingCredentials: signinCredentials
            );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(options);
            return tokenString;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            };
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("TokenConfigurations:Secret"))),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCulture))
                throw new SecurityTokenException("Invalid Token");

            return principal;
        }
    }
}
