using Movies.Application.Models.Abstractions;

namespace Movies.Application.Models;

/// <summary>
/// Domain object used to control movies.
/// </summary>
public class Movie : MovieBase
{
    /// <summary>
    /// Movie id.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Ratings linked to this movie.
    /// </summary>
    public IEnumerable<MovieRating> Ratings { get; set; } = Enumerable.Empty<MovieRating>();
}
