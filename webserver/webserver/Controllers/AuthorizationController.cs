using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace webserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        [Authorize]
        [Route("getlogin")]
        public IActionResult GetLogin()
        {
            return Ok(User.Identity.Name);
        }

        [Authorize(Roles = "admin")]
        [Route("isAdmin")]
        public IActionResult GetRole()
        {
            return Ok();
        }
    }
}
