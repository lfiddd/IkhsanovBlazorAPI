using IkhsanovAPI.Requests;
using Microsoft.AspNetCore.Mvc;

namespace IkhsanovAPI.Intefaces;

public interface IAdminService
{
    Task<IActionResult> GetAllUsers([FromHeader] string authorization);
    Task<IActionResult> CreateNewUserAndLoginAsync([FromHeader] string authorization, QueryUsers newUser);
    Task<IActionResult> UpdateUserAndLoginAsync([FromHeader] string authorization, QueryUsers updateUser);
    Task<IActionResult> DeleteUserAndLoginAsync([FromHeader] string authorization, int userId);
    
    Task<IActionResult> GetAllMovies([FromHeader] string authorization);
    Task<IActionResult> AddNewMovie([FromHeader] string authorization, MovieRequest newMovie);
    Task<IActionResult> UpdateMovie([FromHeader] string authorization, MovieRequest updateMovie);
    Task<IActionResult> DeleteMovie([FromHeader] string authorization, int movieIdd);
}