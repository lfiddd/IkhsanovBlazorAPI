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

    public async Task<IActionResult> GetAllMovies(string authorization)
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
        
        var moviesList = await _context.Movies.ToListAsync();
        
        return new OkObjectResult(new {data = moviesList, status = true, message = "Movies list successfully retrieved" });
    }


    public async Task<IActionResult> GetMovieFromID(string authorization, int movieIdd)
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

        var movie = await _context.Movies.FirstOrDefaultAsync(s => s.movieId == movieIdd);

        if (movie == null)
        {
            return new NotFoundObjectResult(new
            {
                status = false,
                message = "Movie not founded"
            });
        }

        return new OkObjectResult(new
            {
                status = true,
                data = new
                {
                    title = movie.title,
                    description = movie.description,
                    releaseDate = movie.releaseDate,
                    rate = movie.rate,
                    genre = movie.Genre.genreName,
                    image = movie.imageUrl,
                },
                message = "Movie succsesfully retrieved"
            }
        );
    }
    
    
}