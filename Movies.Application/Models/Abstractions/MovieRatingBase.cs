namespace Movies.Application.Models.Abstractions;

/// <summary>
/// Base movie rating containg properties shared between the contracts and the
/// domain models.
/// </summary>
public abstract class MovieRatingBase
{
    /// <summary>
    /// Id of the movie that was rated.
    /// </summary>
    public Guid MovieId { get; set; }

    /// <summary>
    /// Rating. A number between 1 and 5.
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Date when the movie was rated.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Date when the movie rating was last updated.
    /// </summary>
    public DateTime UpdatedDate { get; set; }
}