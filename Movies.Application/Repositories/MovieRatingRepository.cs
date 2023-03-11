using Movies.Application.DAL;
using Movies.Application.Models;
using Movies.Application.Repositories.Abstractions;

namespace Movies.Application.Repositories;

/// <summary>
/// <see cref="IMovieRatingRepository"/> implementation.
/// </summary>
public class MovieRatingRepository : IMovieRatingRepository
{
    private readonly MoviesApiContext _context;

    /// <summary>
    /// Constructor. Injects required services.
    /// </summary>
    /// <param name="context">Database context for this project.</param>
    public MovieRatingRepository(MoviesApiContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<MovieRating> CreateMovieRating(MovieRating rating,
        CancellationToken token = default)
    {
        rating.Id = Guid.NewGuid();
        rating.CreatedDate = DateTime.UtcNow;
        rating.UpdatedDate = DateTime.UtcNow;

        await _context.Ratings.AddAsync(rating, token);

        await _context.SaveChangesAsync(token);

        return rating;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteMovieRating(Guid id,
        CancellationToken token = default)
    {
        MovieRating? rating = await _context.Ratings.FindAsync(
            new object?[] { id },
            cancellationToken: token);

        if (rating is null)
        {
            return false;
        }

        _context.Ratings.Remove(rating);

        await _context.SaveChangesAsync(token);

        return true;
    }

    /// <inheritdoc/>
    public Task<IEnumerable<MovieRating>> GetAllMovieRatings(
        CancellationToken token = default)
        => Task.FromResult(_context.Ratings.AsEnumerable());

    /// <inheritdoc/>
    public Task<MovieRating?> GetMovieRatingById(Guid id,
        CancellationToken token = default)
    {
        return _context.Ratings.FindAsync(
            new object?[] { id },
            cancellationToken: token)
        .AsTask();
    }

    /// <inheritdoc/>
    public async Task<MovieRating> UpdateMovieRating(MovieRating rating,
        CancellationToken token = default)
    {
        rating.UpdatedDate = DateTime.UtcNow;

        _context.Ratings.Update(rating);

        await _context.SaveChangesAsync(token);

        return rating;
    }
}
