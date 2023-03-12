using Microsoft.EntityFrameworkCore;
using Movies.Application.Models;

namespace Movies.Application.DAL;

/// <summary>
/// Database context for this project.
/// </summary>
public class MoviesApiContext : DbContext
{
    /// <summary>
    /// Constructor. Accepts options and passess them to the base object.
    /// </summary>
    /// <param name="options">Options used to build up the database.</param>
    public MoviesApiContext(DbContextOptions<MoviesApiContext> options)
        : base(options) { }

    /// <summary>
    /// Movies <see cref="DbSet{TEntity}"/> used to track and manage all movies.
    /// Represents the Movies table in the database.
    /// </summary>
    public DbSet<Movie> Movies { get; set; }

    /// <summary>
    /// Movie ratings <see cref="DbSet{TEntity}"/> used to track and manage all
    /// movie ratings. Respresents the movie ratings table in the database.
    /// </summary>
    public DbSet<MovieRating> Ratings { get; set; }
}
