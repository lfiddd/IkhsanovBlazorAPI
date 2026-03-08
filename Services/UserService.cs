using IkhsanovAPI.DatabaseContext;
using IkhsanovAPI.Intefaces;
using IkhsanovAPI.UniversalMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IkhsanovAPI.Services;

public class UserService : IUserService
{
    private readonly ContextDatabase _context;
    private readonly JWTtokenGenerator _jwtTokensGenerator;
    
    public UserService(ContextDatabase contextDatabase, JWTtokenGenerator jwtTokensGenerator)
    {
        _context = contextDatabase;
        _jwtTokensGenerator = jwtTokensGenerator;
    }
    public async Task<IActionResult> CheckProfile(string authorization)
    {
        var tempSession = authorization.Split(' ').Last();
    
        var session = await _context.Sessions
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.token == tempSession);
    
        if (session == null)
        {
            return new UnauthorizedObjectResult(new 
            { 
                status = false, 
                message = "Сессия не найдена" 
            });
        }
    
        var userProfile = await _context.Logins
            .Include(l => l.User)
            .FirstOrDefaultAsync(l => l.userId == session.userId); 
    
        if (userProfile == null)
        {
            return new NotFoundObjectResult(new 
            { 
                status = false, 
                message = "Пользователь не найден" 
            });
        }
    
        return new OkObjectResult(new
        {
            status = true,
            message = "Данные профиля",
            data = new
            {
                userId = userProfile.User.userId,
                email = userProfile.email,
                fullname = userProfile.User.fullname,
                description = userProfile.User.description,
                password = userProfile.password
            }
        });
    }
    
    
}