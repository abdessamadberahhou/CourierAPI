using System;
using System.Threading.Tasks;
using AuthenticationAPI.Models;
using CourierApi.Models.Users;

namespace CourierApi.Services.UserRepository
{
    public interface IUserRepository
    {
        Task<User> GetByEmail(string email);
        Task<User> GetByCin(string cin);
        Task<User> CreateUser(User user);
        Task<User> GetById(Guid id);
    }
}
