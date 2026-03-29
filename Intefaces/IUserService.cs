using Microsoft.AspNetCore.Mvc;

namespace IkhsanovAPI.Intefaces;

public interface IUserService
{
    Task<IActionResult> CheckProfile(string authorization);
    Task<IActionResult> GetAllMovies([FromHeader] string authorization);
    Task<IActionResult> GetMovieFromID([FromHeader] string authorization, int movieIdd);
}