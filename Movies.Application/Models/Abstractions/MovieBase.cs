namespace Movies.Application.Models.Abstractions;

/// <summary>
/// Base object for all movie models. Contains properties shared between the
/// domain object and the contracts.
/// </summary>
public abstract class MovieBase : Trackable
{
    /// <summary>
    /// Movie title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Description of the movie.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Date when the movie was released.
    /// </summary>
    public DateTime DateOfRelease { get; set; }
}
