using System;
using Microsoft.AspNetCore.Mvc;
using Money2.Application.Users;
using Money2.WebApi.Security;

namespace Money2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly ISecurityProvider _securityProvider;
        private readonly IUserService _userService;

        public AuthController( IJwtService jwtService, ISecurityProvider securityProvider, IUserService userService )
        {
            _jwtService = jwtService;
            _securityProvider = securityProvider;
            _userService = userService;
        }

        [HttpPost, Route( "login" )]
        public IActionResult Login( [FromBody]LoginDto login )
        {
            if ( login == null )
            {
                return BadRequest( ModelState );
            }

            login.Password = _securityProvider.CalculateSha256( login.Password );

            UserDto user = _userService.ValidateUser( login );
            if ( user != null )
            {              
                String token = _jwtService.GenerateToken( user.Id, user.Email, user.Name );

                return Ok( new { Token = token } );
            }
            else
            {
                return BadRequest( "Неверный email или пароль" );
            }
        }

        [HttpPost, Route( "register" )]
        public IActionResult Register( [FromBody]RegisterDto register )
        {
            if ( register == null )
            {
                return BadRequest( ModelState );
            }

            register.Password = _securityProvider.CalculateSha256( register.Password ); 

            if( _userService.RegisterUser( register ) )
            {
                return Ok();
            }

            return BadRequest( "Такой email уже есть в системе" );
        }
    }
}
