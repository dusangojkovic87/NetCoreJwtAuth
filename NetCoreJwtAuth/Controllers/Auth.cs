
using Microsoft.AspNetCore.Mvc;
using NetCoreJwtAuth.Dtos;
using NetCoreJwtAuth.Entities;
using NetCoreJwtAuth.Services.IRepository;

namespace NetCoreJwtAuth.Controllers
{
    [Route("[controller]")]
    public class Auth : Controller
    {
        private readonly ILogger<Auth> _logger;
        private IAuthenticate _repository;

        public Auth(ILogger<Auth> logger, IAuthenticate repository)
        {
            _logger = logger;
            _repository = repository;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDto request)
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
        public IActionResult Login()
        {
            return Ok();
        }

        [Route("login")]
        [HttpGet]
        public IActionResult LoginGet()
        {
            return Ok(new { Data = "login page" });
        }




    }
}