using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Money2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected Int32 UserId
        {
            get
            {
                return 0;// Int32.Parse( User.Claims.First( c => c.Type == ClaimTypes.NameIdentifier ).Value );
            }
        }
    }
}
