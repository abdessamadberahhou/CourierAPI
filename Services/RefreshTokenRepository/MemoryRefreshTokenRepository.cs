using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourierApi.Models.RefreshToken;
using Microsoft.EntityFrameworkCore;

namespace CourierApi.Services.RefreshTokenRepository
{
    public class MemoryRefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly List<RefreshToken> list = new List<RefreshToken>();
        public Task Create(RefreshToken refreshToken)
        {
            refreshToken.Id = Guid.NewGuid();
            using (var context = new RefreshTokenContext())
            {
                context.RefreshTokens.Add(refreshToken);
                context.SaveChanges();
                return Task.FromResult(refreshToken);
            }
        }
        public Task Delete(Guid id)
        {
            using (var context = new RefreshTokenContext())
            {
                var  token = context.RefreshTokens.Where(r => r.Id == id).FirstOrDefault();
                context.RefreshTokens.Remove(token);
                context.SaveChanges();
                return Task.CompletedTask;
            }
        }

        public async Task DeleteAll(Guid id)
        {
            using(var context = new RefreshTokenContext())
            {
                IEnumerable<RefreshToken> refreshTokens = await context.RefreshTokens.Where(r => r.UserId == id).ToListAsync();
                context.RefreshTokens.RemoveRange(refreshTokens);
                context.SaveChanges();
            }
        }

        public Task<RefreshToken> GetByToken(string token)
        {
            using (var context = new RefreshTokenContext())
            {
                var refreshToken = context.RefreshTokens.Where(r => r.RefreshToken1 == token).FirstOrDefault();
                return Task.FromResult(refreshToken);
            }
        }
    }
}
