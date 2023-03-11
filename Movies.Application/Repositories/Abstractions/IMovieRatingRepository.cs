using Movies.Application.Models;

namespace Movies.Application.Repositories.Abstractions;

/// <summary>
/// Movie ratings respository which is used to handel all data access for movie
/// ratings.
/// </summary>
public interface IMovieRatingRepository
{
    /// <summary>
    /// Gets a <see cref="MovieRating"/> matching the provieded
    /// <paramref name="id"/>.
    /// </summary>
    /// <param name="id">Id of movie rating.</param>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns>
    /// A <see cref="MovieRating"/>. If they <see cref="MovieRating"/> was not
    /// found then <see langword="null"/> is returned.
    /// </returns>
    Task<MovieRating?> GetMovieRatingById(
        Guid id,
        CancellationToken token = default);

    /// <summary>
    /// Gets all movie ratings.
    /// </summary>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns>A list of <see cref="MovieRating"/>s.</returns>
    Task<IEnumerable<MovieRating>> GetAllMovieRatings(
        CancellationToken token = default);

    /// <summary>
    /// Adds the provided <paramref name="rating"/> to the database.
    /// </summary>
    /// <param name="rating">
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <see cref="MovieRating"/> which will get added to the database.
    /// </param>
    /// <returns>The newly created <see cref="MovieRating"/>.</returns>
    Task<MovieRating> CreateMovieRating(MovieRating rating,
        CancellationToken token = default);

    /// <summary>
    /// Updates a <see cref="MovieRating"/>.
    /// </summary>
    /// <param name="rating">
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <see cref="MovieRating"/> which will be used to replace the old rating.
    /// </param>
    /// <returns>
    /// The updated movie rating.
    /// </returns>
    Task<MovieRating> UpdateMovieRating(MovieRating rating,
        CancellationToken token = default);

    /// <summary>
    /// Deletes a <see cref="MovieRating"/> from the database.
    /// </summary>
    /// <param name="id">Id of rating to delete.</param>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns>
    /// A <see cref="bool"/> indicating whether the operation was successful.
    /// <see langword="true"/> if the <see cref="MovieRating"/> was successfully
    /// deleted otherwise <see langword="false"/>.
    /// </returns>
    Task<bool> DeleteMovieRating(Guid id,
        CancellationToken token = default);
}
