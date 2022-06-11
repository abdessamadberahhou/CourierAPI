using System.Security.Claims;
using System.Threading.Tasks;
using AuthenticationAPI.Models.Requests;
using CourierApi.Models.Responses;
using CourierApi.Models.Requests;
using CourierApi.Models.Users;
using CourierApi.Services;
using CourierApi.Services.Authenticator;
using CourierApi.Services.TokensGenerators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CourierApi.Services.UserRepository;
using CourierApi.Services.TokenValidators;
using CourierApi.Services.RefreshTokenRepository;
using CourierApi.Models.RefreshToken;
using System;

namespace CourierApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authentication : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly Authenticator _authenticator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public Authentication(IUserRepository userRepository,
            AccessTokenGenerator accessTokenGenerator,
            Authenticator authenticator,
            RefreshTokenValidator refreshTokenValidator,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _accessTokenGenerator = accessTokenGenerator;
            _authenticator = authenticator;
            _refreshTokenValidator = refreshTokenValidator;
            _refreshTokenRepository = refreshTokenRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (register.Password != register.ConfirmPassword)
            {
                return BadRequest(new ErrorResponse("Verifier vos mots de passe"));
            }

            User existingUserByEmail = await _userRepository.GetByEmail(register.Email);
            if (existingUserByEmail != null)
            {
                return Conflict(new ErrorResponse("Cette email est déja utilisé"));
            }


            User existingUserByCin = await _userRepository.GetByCin(register.Cin);
            if (existingUserByCin != null)
            {
                return Conflict(new ErrorResponse("Cette CIN est déja utilisé"));
            }

            //string passwordHash = _passwordHasher.HashPassword(register.Password);
            User registrationUser = new User()
            {
                Email = register.Email,
                Cin = register.Cin,
                FirstName = register.FirstName,
                LastName = register.LastName,
                BirthDay = register.BirthDay,
                NumTele = register.NumTele,
                Password = register.Password,
                Avatar = register.Avatar,
            };
            await _userRepository.CreateUser(registrationUser);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            User user = await _userRepository.GetByCin(loginRequest.Username);
            if (user == null)
            {
                return BadRequest(new ErrorResponse("Utilisateur introuvable ou inaccepté"));
            }
            if (user.Password != loginRequest.Password)
            {
                return Unauthorized(new ErrorResponse("Mot de passe incorrect"));
            }
            AuthenticatedUserResponse response = await _authenticator.Authenticate(user);
            return Ok(response);
        }





        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest refreshRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("verifier votre token");
            }
            bool isValidRefreshToken = _refreshTokenValidator.Validate(refreshRequest.RefreshToken);
            if (!isValidRefreshToken)
            {
                return BadRequest(new ErrorResponse("Refresh Token invalide"));
            }
            RefreshToken refreshTokenDTO = await _refreshTokenRepository.GetByToken(refreshRequest.RefreshToken);
            if(refreshTokenDTO == null)
            {
                return NotFound(new ErrorResponse("Token introuvable"));
            }
            await _refreshTokenRepository.Delete(refreshTokenDTO.Id);
            User user = await _userRepository.GetById(refreshTokenDTO.UserId);
            if(user == null)
            {
                return NotFound(new ErrorResponse("Utilisateur introuvable"));
            }
            AuthenticatedUserResponse response = await _authenticator.Authenticate(user);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("data")]
        public IActionResult Index()
        {
            string id = HttpContext.User.FindFirstValue("id");
            string name = HttpContext.User.FindFirstValue("name");

            return Ok(new { name = name, id = id });
        }


        [Authorize]
        [HttpDelete("logout")]
        public async Task<IActionResult> logout([FromBody] LogoutRequest user)
        {
            var u = await _userRepository.GetById(user.userId);
            if(u != null)
            {
                await _refreshTokenRepository.DeleteAll(user.userId);
                return Ok("Logged out !");
            }
            return NoContent();
        }
    } 
}
