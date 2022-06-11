using System;

namespace CourierApi.Models.Requests
{
    public class DeleteUserRequest
    {
        public Guid id { get; set; }
        public int isAdmin { get; set; }
       
    }
}
