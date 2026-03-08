using IkhsanovAPI.Requests;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace IkhsanovAPI.Intefaces;

public interface IAuthService
{
    Task<IActionResult> RegisterNewUser(QueryUsers register);
    Task<IActionResult> AuthUser(AuthUser authUser);
}