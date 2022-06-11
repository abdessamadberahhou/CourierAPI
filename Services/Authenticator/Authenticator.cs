using System.Threading.Tasks;
using CourierApi.Models.RefreshToken;
using CourierApi.Models.Requests;
using CourierApi.Models.Responses;
using CourierApi.Models.Users;
using CourierApi.Services.RefreshTokenRepository;
using CourierApi.Services.TokensGenerators;

namespace CourierApi.Services.Authenticator
{
    public class Authenticator
    {
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public Authenticator(AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator, IRefreshTokenRepository refreshTokenRepository)
        {
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthenticatedUserResponse> Authenticate(User user)
        {
             string accessToken = _accessTokenGenerator.GenerateToken(user);
            string refreshToken = _refreshTokenGenerator.GenerateToken();


            RefreshToken refreshTokenDTO = new RefreshToken()
            {
                UserId = user.Id,
                RefreshToken1 = refreshToken
            };
            await _refreshTokenRepository.Create(refreshTokenDTO);
            return new AuthenticatedUserResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                id = user.Id,
                cin = user.Cin,
                nom = user.LastName,
                prenom = user.FirstName,
                date = user.BirthDay,
                email = user.Email,
                phone = user.NumTele,
                isAdmin = user.IsAdmin,
                avatar = user.Avatar
            };
        }
    }
}
