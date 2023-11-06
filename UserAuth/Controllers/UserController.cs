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

    // tasks represent asynchronous operations, enabling concurrent execution without blocking the main thread
    [HttpPost]
    public async Task<IActionResult> UserCreating(UserDto user)
    {
        await _signService.Sign(user); // wait for service request 
        return Ok("User successfully signed"); // can return ok because its on the controller, so if system gets off the service, it can return ok to the client
    }
}
