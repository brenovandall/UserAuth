using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserAuth.Controllers;

[ApiController]
[Route("[controller]")]
public class AccessController : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "minage")] // specify the policy for being authorized -- > class MinAge.cs
    public IActionResult Get()
    {
        return Ok("authorized");
    }
}
