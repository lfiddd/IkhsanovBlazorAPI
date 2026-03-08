using Microsoft.AspNetCore.Mvc;

namespace IkhsanovAPI.Intefaces;

public interface IUserService
{
    Task<IActionResult> CheckProfile(string authorization);
}