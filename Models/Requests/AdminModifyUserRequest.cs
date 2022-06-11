﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CourierApi.Models.Requests
{
    public class AdminModifyUserRequest
    {
        [Required]
        public Guid id { get; set; }
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


        public string Password { get; set; }


        [Required]
        public string NumTele { get; set; }
        public byte[] avatar { get; set; }
    }
}
