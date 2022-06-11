using System.Collections.Generic;
using CourierApi.Models.Users;

namespace CourierApi.Models.Responses
{
    public class ListUserResponse
    {
        public List<User> users { get; set; }
        public List<User> invitations { get; set; }
        public int total { get; set; }
        public int totalInvitation { get; set; }
    }
}
