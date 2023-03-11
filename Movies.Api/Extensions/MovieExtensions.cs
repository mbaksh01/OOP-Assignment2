using Movies.Api.Contracts;
using Movies.Application.Models;

namespace Movies.Api.Extensions;

/// <summary>
/// Extension methods for the <see cref="Movie"/> object.
/// </summary>
public static class MovieExtensions
{
    /// <summary>
    /// Converts a movie contract used on the API layer to the
    /// <see cref="Movie"/> domain object.
    /// </summary>
    /// <param name="request">
    /// Contract to convert to a <see cref="Movie"/>.
    /// </param>
    /// <returns>
    /// The <see cref="MovieRequest"/> contract as a <see cref="Movie"/>.
    /// </returns>
    public static Movie ToMovie(this MovieRequest request)
    {
        return new Movie
        {
            Title = request.Title,
            Description = request.Description,
            DateOfRelease = request.DateOfRelease,
            CreatedDate = request.CreatedDate,
            UpdatedDate = request.UpdatedDate,
        };
    }

    /// <summary>
    /// Coverts a <see cref="Movie"/> object to a <see cref="MovieResponse"/>
    /// for use on the API layer.
    /// </summary>
    /// <param name="movie">
    /// <see cref="Movie"/> to be converted to a <see cref="MovieResponse"/>.
    /// </param>
    /// <returns>
    /// The <see cref="Movie"/> as a <see cref="MovieResponse"/>.
    /// </returns>
    public static MovieResponse ToResponse(this Movie movie)
    {
        return new MovieResponse
        {
            Id = movie.Id,
            Title = movie.Title,
            Description = movie.Description,
            DateOfRelease = movie.DateOfRelease,
            CreatedDate = movie.CreatedDate,
            UpdatedDate = movie.UpdatedDate,
        };
    }
}
