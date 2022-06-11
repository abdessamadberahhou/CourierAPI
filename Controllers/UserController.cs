using System;
using System.Linq;
using System.Threading.Tasks;
using CourierApi.Models.Requests;
using CourierApi.Models.Responses;
using CourierApi.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourierApi.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        [HttpPost("allusers/{isAdmin}")]
        public IActionResult AllUsers(int isAdmin)
        {
            using (var context = new UsersContext())
            {
                var users = context.Users.Where(u => u.IsAdmin == 0).Where(u => u.is_accepted == 1).ToList();
                var invits = context.Users.Where(u => u.IsAdmin == 0).Where(u => u.is_accepted == 0).ToList();
                int total = context.Users.Where(u => u.IsAdmin == 0).Where(u => u.is_accepted == 1).Count();
                int totalInvit = context.Users.Where(u => u.IsAdmin == 0).Where(u => u.is_accepted == 0).Count();
                return Ok(new ListUserResponse()
                {
                    users = users,
                    invitations = invits,
                    total = total,
                    totalInvitation = totalInvit
                });
            }
        }



        [HttpPost("modify/user/{id}")]
        public async Task<IActionResult> ModifyUser([FromBody] ModifyUserRequest user, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            using(var context = new UsersContext())
            {
                var u = context.Users.Where(u => u.Id == id).FirstOrDefault();
                if(u!= null) {
                    if (u.Password != user.Password)
                    {
                        return Unauthorized(new ErrorResponse("Mot de passe incorrect"));
                    }
                    u.Email = user.Email;
                    u.Cin = user.Cin;
                    u.LastName = user.LastName;
                    u.BirthDay = user.BirthDay;
                    u.FirstName = user.FirstName;
                    if(!string.IsNullOrEmpty(user.NewPassword))
                    {
                        u.Password = user.NewPassword;
                    }
                    u.NumTele = user.NumTele;
                    if(user.avatar != null && user.avatar.Length > 0)
                    {
                        u.Avatar = user.avatar;
                    }
                    context.SaveChanges();
                    return Ok("Profile modifié avec succée");
                }
                else
                {
                    return BadRequest(new ErrorResponse("utilisateur introuvable"));
                }
                
            }
        }


        [HttpPost("modify/{isAdmin}")]
        public async Task<IActionResult> ModifyUser(int isAdmin, [FromBody] AdminModifyUserRequest user) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponse("verifier vos cordonnées"));
            }
            if(isAdmin == 1)
            {
                using(var context = new UsersContext())
                {
                    User result = context.Users.Where(u => u.Id == user.id).FirstOrDefault();
                    if(result == null)
                    {
                        return BadRequest(new ErrorResponse("utilisateur introuvable"));
                    }
                    else
                    {
                        result.Email = user.Email;
                        result.FirstName = user.FirstName;
                        result.LastName = user.LastName;
                        result.BirthDay = user.BirthDay;
                        result.Cin = user.Cin;
                        result.Avatar = user.avatar;
                        result.NumTele = user.NumTele;
                        result.Password = user.Password;
                        context.SaveChanges();
                        return Ok("Utilisateur modifié avec sucée");
                    }
                }
            }
            return Unauthorized(new ErrorResponse("Action Immpossible"));
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserRequest request)
        { 
            if (request.isAdmin == 1)
            {
                using (var context = new UsersContext())
                {
                    User result = context.Users.Where(u => u.Id == request.id).FirstOrDefault();
                    if (result == null)
                    {
                        return BadRequest(new ErrorResponse("utilisateur introuvable"));
                    }
                    else
                    {
                        context.Remove(result);
                        context.SaveChanges();
                        return Ok("Utilisateur supprimé avec sucée");
                    }
                }
            }
            return Unauthorized(new ErrorResponse("Action Immpossible"));
        }
        [HttpPost("accept/user/{id}")]
        public async Task<IActionResult> AcceptUser(Guid id)
        {
            using (var context = new UsersContext())
            {
                var user = context.Users.Where(u => u.Id == id).Where(u => u.is_accepted == 0).FirstOrDefault();
                if (user != null)
                {
                    user.is_accepted = 1;
                    context.SaveChanges();
                    return Ok("Utilisateur accepté");
                }
                else
                {
                    return BadRequest("Utilisateur introuvable");
                }
            }
        }



        [HttpPost("search/users/accepted")]
        public async Task<IActionResult> SearchAcceptedUsers(string query)
        {
            using (var context = new UsersContext())
            {
                var usersAccepted = context.Users.
                    Where(u => (u.FirstName.Contains(query)) || (u.LastName.Contains(query)) || (u.Email.Contains(query)) || (u.Cin.Contains(query)) || (u.NumTele.Contains(query))).
                    Where(u => u.is_accepted == 1).
                    Where(u => u.IsAdmin == 0).
                    ToList();;

                if (usersAccepted.Count > 0)
                {
                    return Ok(new ListUserResponse()
                    {
                        users = usersAccepted,
                    });
                }
                else
                {
                    return BadRequest("Aucun rsultat");
                }
            }
        }



        [HttpPost("search/users/notaccepted")]
        public async Task<IActionResult> SearchNotAcceptedUsers(string query)
        {
            using (var context = new UsersContext())
            {;
                var invitations = context.Users.
                    Where(u => (u.FirstName.Contains(query)) || (u.LastName.Contains(query)) || (u.Email.Contains(query)) || (u.Cin.Contains(query)) || (u.NumTele.Contains(query))).
                    Where(u => u.is_accepted == 0).
                    Where(u => u.IsAdmin == 0).
                    ToList();

                if (invitations.Count > 0)
                {
                    return Ok(new ListUserResponse()
                    {
                        invitations = invitations
                    });
                }
                else
                {
                    return BadRequest("Aucun rsultat");
                }
            }
        }
    }
}
