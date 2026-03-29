using IkhsanovAPI.DatabaseContext;
using IkhsanovAPI.Intefaces;
using IkhsanovAPI.Models;
using IkhsanovAPI.Requests;
using IkhsanovAPI.UniversalMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IkhsanovAPI.Services;

public class AdminService : IAdminService
{
    private readonly ContextDatabase _context;
    private readonly JWTtokenGenerator _jwtTokensGenerator;
    
    public AdminService(ContextDatabase contextDatabase, JWTtokenGenerator jwtTokensGenerator)
    {
        _context = contextDatabase;
        _jwtTokensGenerator = jwtTokensGenerator;
    }

    public async Task<IActionResult> GetAllUsers(string authorization)
    {
        var tempSession = authorization.Split(' ').Last();
        var usersList = await _context.Sessions.Include(s => s.User).FirstOrDefaultAsync(s => s.token == tempSession);

        if (usersList == null)
        {
            return new NotFoundObjectResult(new { status = false, message = "Users not found." });
        }
        
        var users = await _context.Users.ToListAsync();
        return new OkObjectResult(new
        {
            data = users,
            status = true,
            message = "Users successfully retrieved."
        });
    }
    

    public async Task<IActionResult> CreateNewUserAndLoginAsync(string authorization, QueryUsers newUser)
    {
        var tempSession = authorization.Split(' ').Last();
        var usersList = await _context.Sessions.Include(s => s.User).FirstOrDefaultAsync(s => s.token == tempSession);
        
        var newLogin = new Login()
        {
            email = newUser.Email,
            password = newUser.Password,
            User = new User()
            {
                fullname = newUser.fullname,
                description = newUser.description,
                email = newUser.Email,
                roleId = 2
            }

        };
        
        await _context.AddAsync(newLogin);
        await _context.SaveChangesAsync();
        
        return new OkObjectResult(new {status = true, message = "User created successfully." });
    }

    public async Task<IActionResult> UpdateUserAndLoginAsync(string authorization, QueryUsers updateUser)
    {
        var tempSession = authorization.Split(' ').Last();
        var thisUser = await _context.Sessions.Include(s => s.User).FirstOrDefaultAsync(s => s.token == tempSession);
        if (thisUser == null)
        {
            return new UnauthorizedObjectResult(new
            {
                status = false,
                message = "Ссесия не найдена"
            });
        }
        var getUser = await _context.Logins.Include(l => l.User).FirstOrDefaultAsync(u => u.userId == updateUser.userId);
        if (getUser == null)
        {
            return new NotFoundObjectResult(new
            {
                status = false,
                message = "Пользователь не найден"
            });
        }

        if (await _context.Logins.AnyAsync(l => l.email == updateUser.Email && l.User.userId != updateUser.userId))
        {
            return new NotFoundObjectResult(new
            {
                status = false,
                message = "Данный Email уже занят"
            });
        }
        getUser.email = updateUser.Email;
        getUser.User.email = updateUser.Email;
        getUser.password = updateUser.Password;
        getUser.User.description = updateUser.description;
        getUser.User.fullname = updateUser.fullname;
        await _context.SaveChangesAsync();

        return new OkObjectResult(new
        {
            status = true,
            
            message = "Вы успешно обновили данные"
        });
    }

    public async Task<IActionResult> DeleteUserAndLoginAsync(string authorization, int userId)
    {
        var tempSession = authorization.Split(' ').Last();
        var thisUser = await _context.Sessions.Include(s => s.User).FirstOrDefaultAsync(s => s.token == tempSession);
        if (thisUser == null)
        {
            return new UnauthorizedObjectResult(new
            {
                status = false,
                message = "Ссесия не найдена"
            });
        }
        var getUser = await _context.Logins.Include(l => l.User).FirstOrDefaultAsync(u => u.userId == userId);
        if (getUser == null)
        {
            return new NotFoundObjectResult(new
            {
                status = false,
                message = "Пользователь с таким Id не найден"
            });
        }
        _context.Logins.Remove(getUser);
        _context.Users.Remove(getUser.User);
        await _context.SaveChangesAsync();
        
        return new OkObjectResult(new
        {
            status = true,
            message = "Вы успешно удалили пользователя"
        });
    }

    public async Task<IActionResult> GetAllMovies(string authorization)
    {
        var tempSession = authorization.Split(' ').Last();
    
        var session = await _context.Sessions
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.token == tempSession);
    
        var moviesList = await _context.Movies.ToListAsync();
        
        return new OkObjectResult(new {data = moviesList, status = true, message = "Movies list successfully retrieved" });
    }


    public async Task<IActionResult> AddNewMovie(string authorization, MovieRequest newMovie)
    {
        var tempSession = authorization.Split(' ').Last();
    
        var session = await _context.Sessions
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.token == tempSession);

        var createdMovie = new Movie()
        {
            title = newMovie.title,
            description = newMovie.description,
            releaseDate = newMovie.releaseDate,
            rate =  newMovie.rate,
            genreId = newMovie.genreId,
            imageUrl = newMovie.imageUrl
        };
        
        await _context.Movies.AddAsync(createdMovie);
        await _context.SaveChangesAsync();

        return new OkObjectResult(new
            {
                status = true,
                message = "Movie created successfully."
            }
        );
    }

    public async Task<IActionResult> UpdateMovie(string authorization, MovieRequest updateMovie)
    {
        var tempSession = authorization.Split(' ').Last();
        var thisUser = await _context.Sessions.Include(s => s.User).FirstOrDefaultAsync(s => s.token == tempSession);

        var thisMovie = await _context.Movies.FirstOrDefaultAsync(s => s.movieId == updateMovie.movieId);
        if (thisMovie == null)
        {
            return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Movie not found"
                }
            );
        }
        
        
        thisMovie.title = updateMovie.title;
        thisMovie.description = updateMovie.description;
        thisMovie.releaseDate = updateMovie.releaseDate;
        thisMovie.rate = updateMovie.rate;
        thisMovie.genreId = updateMovie.genreId;
        thisMovie.imageUrl = updateMovie.imageUrl;
        
        await _context.SaveChangesAsync();
        return new OkObjectResult(new
            {
                status = true,
                message = "Movie updated successfully."
            }
        );
    }

    public async Task<IActionResult> DeleteMovie(string authorization, int movieIdd)
    {
        var tempSession = authorization.Split(' ').Last();
        var usersList = await _context.Sessions.Include(s => s.User).FirstOrDefaultAsync(s => s.token == tempSession);


        var thisMovie = await _context.Movies.FirstOrDefaultAsync(s => s.movieId == movieIdd);

        if (thisMovie == null)
        {
            return new NotFoundObjectResult(new {status = false, message = "Movie not found"});
        }
        
        _context.Movies.Remove(thisMovie);
        await _context.SaveChangesAsync();

        return new OkObjectResult(new
            {
                status = true,
                message = "Movie deleted successfully."
            }
        );
    }
}