using Movies.Application.DAL;
using Movies.Application.Models;
using Movies.Application.Repositories.Abstractions;

namespace Movies.Application.Repositories;

/// <summary>
/// <see cref="IMovieRepository"/> implementation.
/// </summary>
public class MovieRepository : IMovieRepository
{
    private readonly MoviesApiContext _context;

    /// <summary>
    /// Constructor. Injects required services.
    /// </summary>
    /// <param name="context">Database context for this project.</param>
    public MovieRepository(MoviesApiContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<Movie> CreateMovieAsync(
        Movie movie,
        CancellationToken token = default)
    {
        // Overwrite properties set by the calling method.
        movie.Id = Guid.NewGuid();
        movie.CreatedDate = DateTime.UtcNow;
        movie.UpdatedDate = DateTime.UtcNow;

        // Add the movie to the database.
        Movie response =
            (await _context.Movies.AddAsync(movie, token))
            .Entity;

        // Commit the changes to the database.
        await _context.SaveChangesAsync(token);

        // Returns the created movie.
        return response;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteMovieByIdAsync(
        Guid id,
        CancellationToken token = default)
    {
        // Gets a movie using the provided id.
        Movie? movie = await _context.Movies.FindAsync(
            new object?[] { id },
            cancellationToken: token);

        // If the movie was null then false is returned to indicate the movie
        // was not found.
        if (movie is null)
        {
            return false;
        }

        // Remove the movie from the database.
        _context.Movies.Remove(movie);

        // Commit the changes.
        await _context.SaveChangesAsync(token);

        // Return true to let the user know the operation was successful.
        return true;
    }

    /// <inheritdoc/>
    public Task<Movie?> GetMovieByIdAsync(
        Guid id,
        CancellationToken token = default)
    {
        return _context.Movies.FindAsync(
            new object?[] { id },
            cancellationToken: token)
        .AsTask();
    }

    /// <inheritdoc/>
    public Task<IEnumerable<Movie>> GetMoviesAsync(
        CancellationToken token = default)
        => Task.FromResult(_context.Movies.AsEnumerable());

    /// <inheritdoc/>
    public async Task<Movie> UpdateMovieAsync(
        Movie movie,
        CancellationToken token = default)
    {
        // Set the updated date to the current time.
        movie.UpdatedDate = DateTime.UtcNow;

        // Update the movie in the database.
        _context.Movies.Update(movie);

        // Commit the changes to the database.
        await _context.SaveChangesAsync(token);

        // Return the updated movie.
        return movie;
    }
}
