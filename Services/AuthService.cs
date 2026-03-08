using IkhsanovAPI.DatabaseContext;
using IkhsanovAPI.Intefaces;
using IkhsanovAPI.Models;
using IkhsanovAPI.Requests;
using IkhsanovAPI.UniversalMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IkhsanovAPI.Services;

public class AuthService : IAuthService
{
    private readonly ContextDatabase _context;
    private readonly JWTtokenGenerator _jwtTokenGenerator;

    public AuthService(ContextDatabase context, JWTtokenGenerator jwtTokenGenerator)
    {
        _context = context;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    public async Task<IActionResult> RegisterNewUser(QueryUsers register)
    {
        if (await _context.Logins.AnyAsync(x => x.email == register.Email))
        {
            return new BadRequestObjectResult(new
            {
                status = false,
                message = "Пользователь с таким Email уже существует"
            });
        }
        var newLogin = new Login()
        {
            User = new User()
            {
                fullname = register.fullname,
                description = register.description,
                roleId = 2,
                email = register.Email,
            },
            email = register.Email,
            password = register.Password,

        };
        await _context.AddAsync(newLogin);
        await _context.SaveChangesAsync();

        return new OkObjectResult(new
        {
            status = true,
            message = "Вы успешно зарегистрировались"   
        });
    }

    public async Task<IActionResult> AuthUser(AuthUser authUser)
    {
        var selectedUser = _context.Logins
            .Include(login => login.User)
            .FirstOrDefault(login => login.email == authUser.Email && login.password == authUser.Password);

        if (selectedUser != null)
        {
            string token = _jwtTokenGenerator.GenerateJwtToken(selectedUser.userId, selectedUser.User.roleId);

            _context.Sessions.Add(new Session()
            {
                token = token,
                userId = selectedUser.userId,
            });
            await _context.SaveChangesAsync();
            

            return new OkObjectResult(new { status = true, token = token, userId = selectedUser.User.userId, roleId = selectedUser.User.roleId });
        }
        else
        {
            return new NotFoundObjectResult(new
                { status = false, message = "User not found. Check you login and password!" });
        }
        
        
    }
}