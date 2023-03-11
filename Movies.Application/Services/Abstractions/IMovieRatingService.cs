using Movies.Application.Models;
using OneOf;
using OneOf.Types;

namespace Movies.Application.Services.Abstractions;

/// <summary>
/// Movies ratings service used to intract with movie ratings.
/// </summary>
public interface IMovieRatingService
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
    /// <see cref="MovieRating"/> which will get added to the database.
    /// </param>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns>The newly created <see cref="MovieRating"/>.</returns>
    Task<OneOf<MovieRating, NotFound>> CreateMovieRating(
        MovieRating rating,
        CancellationToken token = default);

    /// <summary>
    /// Updates a <see cref="MovieRating"/>.
    /// </summary>
    /// <param name="id">Id of the movie to update.</param>
    /// <param name="rating">
    /// Rating which will be used to replace the old rating.
    /// </param>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns>
    /// The updated movie. If the <see cref="MovieRating"/> was not found then
    /// <see langword="null"/> is returned.
    /// </returns>
    Task<MovieRating?> UpdateMovieRating(
        Guid id,
        int rating,
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
    Task<bool> DeleteMovieRating(Guid id, CancellationToken token = default);
}
