namespace CourierApi.Services
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}
