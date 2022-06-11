namespace CourierApi.Models.Configuration
{
    public class AuthConfiguration
    {
        public string AccessTokenSecret { get; set; }
        public string RefreshTokenSecret { get; set; }
        public double ExpirationAccessTokenMinutes { get; set; }
        public double ExpirationRefreshTokenMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
