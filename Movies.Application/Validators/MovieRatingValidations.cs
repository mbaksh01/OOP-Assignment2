using FluentValidation;
using Movies.Application.Models;
using Movies.Application.Repositories.Abstractions;

namespace Movies.Application.Validators;

/// <summary>
/// Validates the provided movie rating is valid.
/// </summary>
public class MovieRatingValidator : AbstractValidator<MovieRating>
{
    private readonly IMovieRepository _movieRepository;

    /// <summary>
    /// Constrcutor. Injects and applys required rules.
    /// </summary>
    /// <param name="movieRepository">Movies repository.</param>
    public MovieRatingValidator(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;

        RuleFor(r => r.MovieId)
            .MustAsync(ValidateMovieIdAsync)
            .WithMessage("No movie matched the provided id.");
    }

    /// <summary>
    /// Validates whether the provided movie id exists in the database.
    /// </summary>
    /// <param name="id">Movie id.</param>
    /// <param name="token">
    /// <see cref="CancellationToken"/> used to cancel this operations.
    /// </param>
    /// <returns>
    /// A <see cref="bool"/> indicating the state of the operation.
    /// <see langword="true"/> if the movie was found otherwise
    /// <see langword="false"/>.
    /// </returns>
    private async Task<bool> ValidateMovieIdAsync(
        Guid id,
        CancellationToken token = default)
    {
        Movie? movie = await _movieRepository.GetMovieByIdAsync(id, token);

        return movie is not null;
    }
}
