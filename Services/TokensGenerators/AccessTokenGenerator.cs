using System.Collections.Generic;
using System.Security.Claims;
using CourierApi.Models.Configuration;
using CourierApi.Models.Users;

namespace CourierApi.Services.TokensGenerators
{
    public class AccessTokenGenerator
    {
        private readonly AuthConfiguration _authConfiguration;
        private readonly TokenGenerator _tokenGenerator;

        public AccessTokenGenerator(AuthConfiguration authConfiguration, TokenGenerator tokenGenerator)
        {
            _authConfiguration = authConfiguration;
            _tokenGenerator = tokenGenerator;
        }

        public string GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id",user.Id.ToString()),
                new Claim("Name",user.FirstName+" "+user.LastName),
                new Claim("isSuccess", "Success")
            };
            return _tokenGenerator.GenerateToken(
                secretKey: _authConfiguration.AccessTokenSecret,
                issuer: _authConfiguration.Issuer,
                audience: _authConfiguration.Audience,
                expirationMinutes: _authConfiguration.ExpirationAccessTokenMinutes,
                claims: claims
            );
        }
    }
}
