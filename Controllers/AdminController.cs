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

    [HttpGet("GetAllMovies")]
    public async Task<IActionResult> GetAllMovies([FromHeader] string authorization) => await _adminService.GetAllMovies(authorization);
    
    [HttpPost("AddNewMovie")]
    public async Task<IActionResult> AddNewMovie([FromHeader] string authorization, MovieRequest newMovie) => await _adminService.AddNewMovie(authorization, newMovie);
    
    [HttpPost("UpdateMovie")]
    public async Task<IActionResult> UpdateMovie([FromHeader] string authorization,MovieRequest updateMovie) => await _adminService.UpdateMovie(authorization, updateMovie);

    [HttpPost("DeleteMovie")]
    public async Task<IActionResult> DeleteMovie([FromHeader] string authorization,[FromQuery] int movieIdd) => await _adminService.DeleteMovie(authorization, movieIdd);
}