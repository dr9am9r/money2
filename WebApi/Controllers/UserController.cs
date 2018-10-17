using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Money2.Application.Users;

namespace Money2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController( IUserService userService )
        {
            _userService = userService;
        }

        public ActionResult GetUser()
        {
            UserDto user = _userService.GetUser( UserId );

            return Ok( user );
        }
    }
}
