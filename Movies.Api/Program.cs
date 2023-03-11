using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Movies.Application.DAL;
using Movies.Application.Models;
using Movies.Application.Repositories;
using Movies.Application.Repositories.Abstractions;
using Movies.Application.Services;
using Movies.Application.Services.Abstractions;
using Movies.Application.Validators;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Set the comments path for the Swagger JSON and UI.
    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.SupportNonNullableReferenceTypes();
});

builder.Services.AddSqlite<MoviesApiContext>(
    connectionString: "DataSource=movies.db",
    optionsAction: options => 
        options.UseQueryTrackingBehavior(
            QueryTrackingBehavior.NoTracking
            ));

// Add services to the container.
builder.Services
    .AddScoped<IMovieRepository, MovieRepository>()
    .AddScoped<IMovieRatingRepository, MovieRatingRepository>()
    .AddScoped<IMovieService, MovieService>()
    .AddScoped<IMovieRatingService, MovieRatingService>()
    .AddScoped<IValidator<MovieRating>, MovieRatingValidator>();

// Build all services.
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
