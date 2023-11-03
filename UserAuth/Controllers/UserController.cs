using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using UserAuth.Data.Dtos;
using UserAuth.Models;
using UserAuth.Services;

namespace UserAuth.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private signService _signService;

    public UserController(signService signService)
    {
        _signService = signService;
    }

    [HttpPost]
    public async Task<IActionResult> UserCreating(UserDto user)
    {
        _signService.Sign(user);
        return Ok("User successfully signed");
    }
}
