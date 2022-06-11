
namespace CourierApi.Services.PasswordHash
{
    public class PasswordHashImp : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
