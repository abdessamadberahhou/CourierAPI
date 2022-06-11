using System;
using System.Collections.Generic;

#nullable disable

namespace CourierApi.Models.RefreshToken
{
    public partial class RefreshToken
    {
        public Guid Id { get; set; }
        public string RefreshToken1 { get; set; }
        public Guid UserId { get; set; }
    }
}
