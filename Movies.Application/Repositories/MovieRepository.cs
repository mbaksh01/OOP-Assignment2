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
        movie.Id = Guid.NewGuid();
        movie.CreatedDate = DateTime.UtcNow;
        movie.UpdatedDate = DateTime.UtcNow;

        Movie response =
            (await _context.Movies.AddAsync(movie, token))
            .Entity;

        await _context.SaveChangesAsync(token);

        return response;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteMovieByIdAsync(
        Guid id,
        CancellationToken token = default)
    {
        Movie? movie = await _context.Movies.FindAsync(
            new object?[] { id },
            cancellationToken: token);

        if (movie is null)
        {
            return false;
        }

        _context.Movies.Remove(movie);

        await _context.SaveChangesAsync(token);

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
        movie.UpdatedDate = DateTime.UtcNow;

        _context.Movies.Update(movie);

        await _context.SaveChangesAsync(token);

        return movie;
    }
}
