using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProjectAPI.Data.Entities;
using WebProjectAPI.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ICustomAuthentication _customAuthentication;
        public AuthenticationController(ICustomAuthentication customAuthentication)
        {
            _customAuthentication = customAuthentication;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Authenticate([FromBody] UserCred userCred)
        {
            var token = _customAuthentication.Authenticate(userCred.Username, userCred.Password);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetMe()
        {
            return Ok("Got You!");
        }
    }
}
