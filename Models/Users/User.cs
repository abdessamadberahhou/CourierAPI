using System;
using System.Collections.Generic;

#nullable disable

namespace CourierApi.Models.Users
{
    public partial class User
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDay { get; set; }
        public string Cin { get; set; }
        public string Password { get; set; }
        public byte[] Avatar { get; set; }
        public string NumTele { get; set; }
        public Guid Id { get; set; }
        public int IsAdmin { get; set ; }
        public int is_accepted { get; set; }
    }
}
