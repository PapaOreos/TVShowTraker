using Microsoft.EntityFrameworkCore;
using TVShowTraker.Helpers;
using TVShowTraker.Middlewares;
using TVShowTraker.Models;
using TVShowTraker.Models.Contexts;
using TVShowTraker.Models.ViewModels;
using TVShowTraker.Services;
using TVShowTraker.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
var conStr = configuration.GetConnectionString("ConStr");

// Add services to the container.
{
    var services = builder.Services;
    services.AddCors();

    // For Entity Framework
    services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(conStr));


    services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    services.AddScoped<IAuthService, AuthenticationService>();
    services.AddScoped<IBaseService<User, UserVM>, UserService>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
{
    app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();
    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapControllers();
}

app.Run();
