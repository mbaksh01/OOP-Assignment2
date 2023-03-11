using Movies.Application.Models;
using Movies.Application.Models.Abstractions;

namespace Movies.Api.Contracts;

/// <summary>
/// Response contract used to return a <see cref="MovieRating"/> on the API layer.
/// </summary>
public class MovieRatingResponse : MovieRatingBase
{
    /// <summary>
    /// Rating Id.
    /// </summary>
    public Guid Id { get; set; }
}
