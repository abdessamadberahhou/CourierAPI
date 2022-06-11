using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationAPI.Models;
using CourierApi.Models.Users;

namespace CourierApi.Services.UserRepository
{
    public class UserSaver : IUserRepository
    {
        public Task<User> CreateUser(User user)
        {
            user.Id = Guid.NewGuid();
            user.IsAdmin = 0;
            using (var context = new UsersContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
                return Task.FromResult(user);
            }
        }

        public Task<User> GetByCin(string cin)
        {
            using(var context = new UsersContext())
            {
                var result = context.Users.Where(u => u.Cin == cin).Where(u => u.is_accepted == 1).FirstOrDefault();
                return Task.FromResult(result);
            }
        }

        public Task<User> GetByEmail(string email)
        {
            using (var context = new UsersContext())
            {
                var result = context.Users.Where(u => u.Email == email).Where(u => u.is_accepted == 1).FirstOrDefault();
                return Task.FromResult(result);
            }
        }

        public Task<User> GetById(Guid id)
        {
            using (var context = new UsersContext())
            {
                var result = context.Users.Where(u => u.Id == id).FirstOrDefault();
                return Task.FromResult(result);
            }
        }
    }
}
