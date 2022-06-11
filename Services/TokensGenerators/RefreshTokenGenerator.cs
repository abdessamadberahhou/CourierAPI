using CourierApi.Models.Configuration;

namespace CourierApi.Services.TokensGenerators
{
    public class RefreshTokenGenerator
    {
        private readonly AuthConfiguration _authConfiguration;
        private readonly TokenGenerator _tokenGenerator;

        public RefreshTokenGenerator(AuthConfiguration authConfiguration, TokenGenerator tokenGenerator)
        {
            _authConfiguration = authConfiguration;
            _tokenGenerator = tokenGenerator;
        }

        public string GenerateToken()
        {
            return _tokenGenerator.GenerateToken(
                secretKey: _authConfiguration.RefreshTokenSecret,
                issuer: _authConfiguration.Issuer,
                audience: _authConfiguration.Audience,
                expirationMinutes: _authConfiguration.ExpirationRefreshTokenMinutes,
                claims: null
                );
        }
    }
}
