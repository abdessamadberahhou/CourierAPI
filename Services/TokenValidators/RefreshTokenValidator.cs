using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using CourierApi.Models.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CourierApi.Services.TokenValidators
{
    public class RefreshTokenValidator
    {
        private readonly AuthConfiguration authConfiguration;

        public RefreshTokenValidator(AuthConfiguration authConfiguration)
        {
            this.authConfiguration = authConfiguration;
        }

        public bool Validate(string refreshToken)
        {
            TokenValidationParameters parameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfiguration.RefreshTokenSecret)),
                ValidIssuer = authConfiguration.Issuer,
                ValidAudience = authConfiguration.Audience,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            try {
                tokenHandler.ValidateToken(refreshToken, parameters,out SecurityToken validatedToken);
                return true;
            }
            catch (Exception) {
                return false;
            }
            
        }
    }
}
