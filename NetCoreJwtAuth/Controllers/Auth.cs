
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
        public IActionResult Register([FromBody] UserDto request)
        {
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
                return Redirect("https:localhost:7062/auth/login");

            }

            try
            {


            }
            catch (System.Exception e)
            {

                _logger.LogError(e.Message);
            }






            return Ok();
        }


        [Route("login")]
        [HttpPost]
        public IActionResult Login()
        {
            return Ok();
        }


    }
}