using System;
using System.Threading.Tasks;
using CourierApi.Models.RefreshToken;


namespace CourierApi.Services.RefreshTokenRepository
{
    public interface IRefreshTokenRepository
    {
        Task Create(RefreshToken refreshToken);
        Task<RefreshToken> GetByToken(string token);
        Task Delete(Guid id);
        Task DeleteAll(Guid id);
    }
}
