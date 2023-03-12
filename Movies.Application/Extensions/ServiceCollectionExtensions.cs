using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Models;
using Movies.Application.Repositories.Abstractions;
using Movies.Application.Repositories;
using Movies.Application.Services.Abstractions;
using Movies.Application.Services;
using Movies.Application.Validators;

namespace Movies.Application.Extensions;

/// <summary>
/// Extension methods for the consuming <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds required services from the application layer to the consuming
    /// project.
    /// </summary>
    /// <param name="serviceDescriptors">
    /// <see cref="IServiceCollection"/> of the consuming project.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="IServiceCollection"/>.
    /// </returns>
    public static IServiceCollection AddApplication(this IServiceCollection serviceDescriptors)
    {
        return serviceDescriptors
            .AddScoped<IMovieRepository, MovieRepository>()
            .AddScoped<IMovieRatingRepository, MovieRatingRepository>()
            .AddScoped<IMovieService, MovieService>()
            .AddScoped<IMovieRatingService, MovieRatingService>()
            .AddScoped<IValidator<MovieRating>, MovieRatingValidator>();
    }
}
