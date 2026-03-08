using IkhsanovAPI.Intefaces;
using IkhsanovAPI.Requests;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace IkhsanovAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController
{
    public readonly IAuthService _service;
    public AuthController(IAuthService service) => _service = service; 
    
    [HttpPost("Register")]
    public async Task<IActionResult> Register(QueryUsers register) => await _service.RegisterNewUser(register);
    
    [HttpPost("Authorize")]
    public async Task<IActionResult> AuthUser(AuthUser authUser) => await _service.AuthUser(authUser);
}