using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using UserAuth.Data.Dtos;

namespace UserAuth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public IActionResult UserCreating(UserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
