using Movies.Api.Contracts;
using Movies.Application.Models;

namespace Movies.Api.Extensions;

/// <summary>
/// Extension methods for the <see cref="MovieRating"/> model.
/// </summary>
public static class MovieRatingExtensions
{
    /// <summary>
    /// Converts a <see cref="MovieRatingRequest"/> to a movie.
    /// </summary>
    /// <param name="request">Contract to convert.</param>
    /// <returns>
    /// A <see cref="MovieRating"/> created from the <paramref name="request"/>.
    /// </returns>
    public static MovieRating ToRating(this MovieRatingRequest request)
    {
        return new MovieRating
        {
            MovieId = request.MovieId,
            Rating = request.Rating,
            CreatedDate = request.CreatedDate,
            UpdatedDate = request.UpdatedDate,
        };
    }

    /// <summary>
    /// Converts a <see cref="MovieRating"/> to a
    /// <see cref="MovieRatingResponse"/>.
    /// </summary>
    /// <param name="rating">Model to convert.</param>
    /// <returns>
    /// A <see cref="MovieResponse"/> created from the <paramref name="rating"/>.
    /// </returns>
    public static MovieRatingResponse ToResponse(this MovieRating rating)
    {
        return new MovieRatingResponse
        {
            Id = rating.Id,
            Rating = rating.Rating,
            MovieId = rating.MovieId,
            CreatedDate = rating.CreatedDate,
            UpdatedDate = rating.UpdatedDate,
        };
    }
}
