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
        // Overwrite the properties set by the calling method.
        rating.Id = Guid.NewGuid();
        rating.CreatedDate = DateTime.UtcNow;
        rating.UpdatedDate = DateTime.UtcNow;

        // Add to the database.
        await _context.Ratings.AddAsync(rating, token);

        // Commit the changes to the database.
        await _context.SaveChangesAsync(token);

        // Return the newly created rating.
        return rating;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteMovieRating(Guid id,
        CancellationToken token = default)
    {
        // Gets the rating using the provided id.
        MovieRating? rating = await _context.Ratings.FindAsync(
            new object?[] { id },
            cancellationToken: token);

        // If the rating is null return false telling the user the movie was
        // not deleted.
        if (rating is null)
        {
            return false;
        }

        // Remove the rating from the database.
        _context.Ratings.Remove(rating);

        // Commit the changes to the database.
        await _context.SaveChangesAsync(token);

        // Return true to indicate the operation was successful.
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
        // Set the updated data to the current time as the object has been
        // updated.
        rating.UpdatedDate = DateTime.UtcNow;

        // Update the rating.
        _context.Ratings.Update(rating);

        // Commit the changes to the database.
        await _context.SaveChangesAsync(token);

        // Return the updated rating.
        return rating;
    }
}
