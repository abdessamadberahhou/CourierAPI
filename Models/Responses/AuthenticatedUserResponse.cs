using System;

namespace CourierApi.Models.Requests
{
    public class AuthenticatedUserResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public Guid id { get; set; }
        public string cin { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string date { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public int isAdmin { get; set; }
        public byte[] avatar { get; set; }
    }
}
