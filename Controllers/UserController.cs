using IkhsanovAPI.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace IkhsanovAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController
{
    public readonly IUserService _userService;
    public UserController(IUserService userService) => _userService = userService;
    
   [HttpGet]
   public async Task<IActionResult> CheckProfile([FromHeader] string authorization) => await _userService.CheckProfile(authorization);
}