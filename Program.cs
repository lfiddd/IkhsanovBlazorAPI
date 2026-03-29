using IkhsanovAPI.Controllers;
using IkhsanovAPI.DatabaseContext;
using IkhsanovAPI.Intefaces;
using IkhsanovAPI.Services;
using IkhsanovAPI.UniversalMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();


// import DatabaseContext file
builder.Services.AddDbContext<ContextDatabase>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("TestDBString")), ServiceLifetime.Scoped);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm", policy =>
    {
        policy.WithOrigins("https://localhost:7073",
            "http://localhost:7073").AllowAnyHeader().AllowAnyMethod();
    });
});


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<JWTtokenGenerator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowBlazorWasm");

app.UseHttpsRedirection();

app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseAuthorization();

app.MapControllers();

app.Run();

