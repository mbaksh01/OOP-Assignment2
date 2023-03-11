using Movies.Application.Models;

namespace Movies.Application.Repositories.Abstractions;

/// <summary>
/// Movies repository which handles data access to all movies.
/// </summary>
public interface IMovieRepository
{
    /// <summary>
    /// Gets a <see cref="Movie"/> by its specified id.
    /// </summary>
    /// <param name="id">Id of <see cref="Movie"/> to add.</param>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns>
    /// A <see cref="Movie"/>. If the <see cref="Movie"/> was not found then
    /// <see langword="null"/> is returned.
    /// </returns>
    Task<Movie?> GetMovieByIdAsync(Guid id, CancellationToken token = default);

    /// <summary>
    /// Gets all <see cref="Movie"/>s currently in the database.
    /// </summary>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns></returns>
    Task<IEnumerable<Movie>> GetMoviesAsync(CancellationToken token = default);

    /// <summary>
    /// Adds a <see cref="Movie"/> to the database.
    /// </summary>
    /// <param name="movie"><see cref="Movie"/> to add.</param>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns>The newly created <see cref="Movie"/>.</returns>
    Task<Movie> CreateMovieAsync(
        Movie movie,
        CancellationToken token = default);

    /// <summary>
    /// Updates a <see cref="Movie"/>.
    /// </summary>
    /// <param name="movie"><see cref="Movie"/> to update.</param>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns>
    /// The updated <see cref="Movie"/>.
    /// </returns>
    Task<Movie> UpdateMovieAsync(
        Movie movie,
        CancellationToken token = default);

    /// <summary>
    /// Deletes a <see cref="Movie"/> from the database.
    /// </summary>
    /// <param name="id">Id of <see cref="Movie"/> to delete.</param>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns>
    /// A <see cref="bool"/> indicating the result of the action.
    /// <see langword="true"/> if the <see cref="Movie"/> was successfully
    /// deleted otherwise <see langword="false"/>.
    /// </returns>
    Task<bool> DeleteMovieByIdAsync(Guid id, CancellationToken token = default);
}
