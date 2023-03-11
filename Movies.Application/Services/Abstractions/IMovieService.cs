using Movies.Application.Models;

namespace Movies.Application.Services.Abstractions;

/// <summary>
/// Movie service used to interact with movies.
/// </summary>
public interface IMovieService
{
    /// <summary>
    /// Gets a <see cref="Movie"/> by its <see cref="Movie.Id"/>.
    /// </summary>
    /// <param name="id">Id of <see cref="Movie"/>.</param>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns>
    /// A <see cref="Movie"/> if found otherwise <see langword="null"/>.
    /// </returns>
    Task<Movie?> GetMovieByIdAsync(Guid id, CancellationToken token = default);

    /// <summary>
    /// Gets all <see cref="Movie"/>s from the database.
    /// </summary>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns>A list of <see cref="Movie"/>s.</returns>
    Task<IEnumerable<Movie>> GetAllMoviesAsync(
        CancellationToken token = default);

    /// <summary>
    /// Creates a <see cref="Movie"/>.
    /// </summary>
    /// <param name="movie"><see cref="Movie"/> to be created.</param>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns>The newly created <see cref="Movie"/>.</returns>
    Task<Movie> CreateMovieAsync(Movie movie, CancellationToken token = default);

    /// <summary>
    /// Updates a <see cref="Movie"/> by replacing the old <see cref="Movie"/>
    /// with the new <paramref name="movie"/>.
    /// </summary>
    /// <param name="movie">New <see cref="Movie"/> object.</param>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns>
    /// The updated version of the <see cref="Movie"/> or
    /// <see langword="null"/> if the <see cref="Movie"/> was not found.
    /// </returns>
    Task<Movie?> UpdateMovieAsync(
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
    /// <see langword="true"/> if the <see cref="Movie"/> was successfully
    /// deleted otherwise <see langword="false"/>.
    /// </returns>
    Task<bool> DeleteMovieByIdAsync(Guid id, CancellationToken token = default);
}
