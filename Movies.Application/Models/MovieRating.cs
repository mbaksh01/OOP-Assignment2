using Movies.Application.Models.Abstractions;

namespace Movies.Application.Models;

/// <summary>
/// Ratings domain model used to rate <see cref="Movie"/>s.
/// </summary>
public class MovieRating : MovieRatingBase
{
    /// <summary>
    /// Rating Id.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
}
