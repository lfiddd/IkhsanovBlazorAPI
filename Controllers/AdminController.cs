using IkhsanovAPI.Intefaces;
using IkhsanovAPI.Requests;
using Microsoft.AspNetCore.Mvc;

namespace IkhsanovAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController
{
    public readonly IAdminService _adminService;
    public AdminController(IAdminService adminService) => _adminService = adminService;

    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAll([FromHeader] string authorization) => await _adminService.GetAllUsers(authorization);
    
    [HttpPost("CreateNewUser")]
    public async Task<IActionResult> CreateNewUserAndLogin([FromHeader] string authorization, QueryUsers newUser) => await _adminService.CreateNewUserAndLoginAsync(authorization, newUser);
    
    [HttpPost("UpdateUser")]
    public async Task<IActionResult> UpdateUser([FromHeader] string authorization,QueryUsers updateUser) => await _adminService.UpdateUserAndLoginAsync(authorization, updateUser);

    [HttpPost("DeleteUser")]
    public async Task<IActionResult> DeleteUser([FromHeader] string authorization,[FromQuery] int userId) => await _adminService.DeleteUserAndLoginAsync(authorization, userId);


}