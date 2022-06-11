using System.ComponentModel.DataAnnotations;

namespace AuthenticationAPI.Models.Requests
{
    public class RegisterRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string BirthDay { get; set; }
        [Required]
        public string Cin { get; set; }
        public string Token { get; set; }
        [MinLength(8)]
        [Required]
        public string Password { get; set; }
        [MinLength(8)]
        [Required]
        public string ConfirmPassword { get; set; }
        public string NumTele { get; set; }
        public byte[] Avatar { get; set; }
    }
}
