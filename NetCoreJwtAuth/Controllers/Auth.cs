
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NetCoreJwtAuth.Dtos;
using NetCoreJwtAuth.Entities;
using NetCoreJwtAuth.Services.IRepository;
using System.IdentityModel.Tokens.Jwt;
using NetCoreJwtAuth.Models;

namespace NetCoreJwtAuth.Controllers
{
    [Route("[controller]")]
    public class Auth : Controller
    {
        private readonly ILogger<Auth> _logger;
        private IAuthenticate _repository;
        private IConfiguration _configuration;

        public Auth(ILogger<Auth> logger, IAuthenticate repository, IConfiguration configuration)
        {
            _logger = logger;
            _repository = repository;
            _configuration = configuration;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();

            }



            var user = new User
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                Password = request.Password
            };

            var isRegistered = _repository.isRegistered(user);
            if (isRegistered)
            {
                return Redirect("~/auth/login");

            }

            try
            {
                var hashedPass = _repository.HashPassword(user.Password);
                user.Password = hashedPass;
                var result = await _repository.RegisterUser(user);
                if (!result)
                {
                    return BadRequest(new { Error = "Error,user not registered!" });

                }

                //if already registered redirect to login
                return Redirect("~/auth/login");

            }
            catch (System.Exception e)
            {

                _logger.LogError(e.Message);
                return StatusCode(500);
            }


        }


        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] UserLoginDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Error = "Fields cannot be empty!" });

            }

            var user = new User
            {

                Email = request.Email,
                Password = request.Password
            };





            var isRegistered = _repository.isRegistered(user);
            if (!isRegistered)
            {
                return Redirect("~/Auth/register");

            }

            var userFromDb = _repository.GetUserByEmail(user.Email);
            if (userFromDb == null)
            {
                return BadRequest(new { Error = "User does not exist with provided email!" });
            }

            var isCorrect = _repository.CheckPassword(userFromDb.Password, request.Password);
            if (!isCorrect)
            {
                return BadRequest(new { Error = "Incorrect password!" });

            }

            var claimsList = new List<Claim>();
            claimsList.Add(new Claim("Email", userFromDb.Email));
            claimsList.Add(new Claim("UserId", userFromDb.Id.ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
             _configuration["Tokens:Issuer"],
                 _configuration["Tokens:Audience"],
                 claimsList,
                 expires: DateTime.UtcNow.AddMinutes(120),
                 signingCredentials: creds

            );

            var tokenToReturn = new JwtToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                isAuthenticated = true

            };


            return Created("~/Auth/login", tokenToReturn);
        }

        [Route("login")]
        [HttpGet]
        public IActionResult LoginGet()
        {
            return Ok(new { Data = "login page" });
        }

        [Route("register")]
        [HttpGet]
        public IActionResult RegisterGet()
        {
            return Ok(new { Data = "register page" });
        }



    }
}