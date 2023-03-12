namespace Movies.Application.Models.Abstractions;

/// <summary>
/// Class used to add tracking properties to a object.
/// </summary>
public abstract class Trackable
{
    /// <summary>
    /// Date and time when the movie was added to the database.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Date and time when the movie was last updated.
    /// </summary>
    public DateTime UpdatedDate { get; set; }
}
