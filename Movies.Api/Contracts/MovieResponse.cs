using Movies.Application.Models;
using Movies.Application.Models.Abstractions;

namespace Movies.Api.Contracts;

/// <summary>
/// Response contract used to return a <see cref="Movie"/> on the API layer.
/// </summary>
public class MovieResponse : MovieBase
{
    /// <summary>
    /// Id of the movie.
    /// </summary>
    public Guid Id { get; set; }
}
