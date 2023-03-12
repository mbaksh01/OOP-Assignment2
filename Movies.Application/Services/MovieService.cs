using Movies.Application.Models;
using Movies.Application.Repositories.Abstractions;
using Movies.Application.Services.Abstractions;

namespace Movies.Application.Services;

/// <summary>
/// <see cref="IMovieService"/> implementation.
/// </summary>
public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    /// <summary>
    /// Constructor. Injects the required services.
    /// </summary>
    /// <param name="movieRepository">
    /// Repository used to control <see cref="Movie"/> objects.
    /// </param>
    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    /// <inheritdoc/>
    public Task<Movie?> GetMovieByIdAsync(
        Guid id,
        CancellationToken token = default)
        => _movieRepository.GetMovieByIdAsync(id, token);

    /// <inheritdoc/>
    public Task<IEnumerable<Movie>> GetAllMoviesAsync(
        CancellationToken token = default)
        => _movieRepository.GetMoviesAsync(token);

    /// <inheritdoc/>
    public Task<Movie> CreateMovieAsync(
        Movie movie,
        CancellationToken token = default)
        => _movieRepository.CreateMovieAsync(movie, token);

    /// <inheritdoc/>
    public async Task<Movie?> UpdateMovieAsync(
        Movie movie,
        CancellationToken token = default)
    {
        // Find the old movie in the database.
        Movie? oldMovie = await _movieRepository.GetMovieByIdAsync(movie.Id, token);

        // If the old movie is null then the movie did not exist so return null.
        if (oldMovie is null)
        {
            return null;
        }

        // Update the movie in the database.
        return await _movieRepository.UpdateMovieAsync(movie, token);
    }

    /// <inheritdoc/>
    public Task<bool> DeleteMovieByIdAsync(
        Guid id,
        CancellationToken token = default)
        => _movieRepository.DeleteMovieByIdAsync(id, token);
}
