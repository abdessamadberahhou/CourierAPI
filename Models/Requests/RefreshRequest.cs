using System.ComponentModel.DataAnnotations;

namespace CourierApi.Models.Requests
{
    public class RefreshRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
