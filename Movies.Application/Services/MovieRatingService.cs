using FluentValidation;
using FluentValidation.Results;
using Movies.Application.Models;
using Movies.Application.Repositories.Abstractions;
using Movies.Application.Services.Abstractions;
using OneOf;
using OneOf.Types;

namespace Movies.Application.Services;

/// <summary>
/// <see cref="IMovieRatingService"/> implementation.
/// </summary>
public class MovieRatingService : IMovieRatingService
{
    private readonly IMovieRatingRepository _repository;
    private readonly IValidator<MovieRating> _validator;

    /// <summary>
    /// Constructor. Injects required services.
    /// </summary>
    /// <param name="repository">
    /// Movie rating repository used to control <see cref="MovieRating"/>
    /// objects.
    /// </param>
    /// <param name="validator">
    /// Validator used to validate incoming contracts.
    /// </param>
    public MovieRatingService(
        IMovieRatingRepository repository,
        IValidator<MovieRating> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    /// <inheritdoc/>
    public async Task<OneOf<MovieRating, NotFound>> CreateMovieRating(
        MovieRating rating,
        CancellationToken token = default)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(rating, token);

        if (!validationResult.IsValid)
        {
            return new NotFound();
        }

        return await _repository.CreateMovieRating(rating, token);
    }

    /// <inheritdoc/>
    public Task<bool> DeleteMovieRating(Guid id, CancellationToken token = default)
        => _repository.DeleteMovieRating(id, token);

    /// <inheritdoc/>
    public Task<IEnumerable<MovieRating>> GetAllMovieRatings(
        CancellationToken token = default)
        => _repository.GetAllMovieRatings(token);

    /// <inheritdoc/>
    public Task<MovieRating?> GetMovieRatingById(
        Guid id,
        CancellationToken token = default)
        => _repository.GetMovieRatingById(id, token);

    /// <inheritdoc/>
    public async Task<MovieRating?> UpdateMovieRating(
        Guid id,
        int rating,
        CancellationToken token = default)
    {
        MovieRating? oldRating = await _repository.GetMovieRatingById(id, token);

        if (oldRating is null)
        {
            return null;
        }

        MovieRating newRating = new()
        {
            Id = id,
            CreatedDate = oldRating.CreatedDate,
            UpdatedDate = oldRating.UpdatedDate,
            Rating = rating
        };

        return await _repository.UpdateMovieRating(newRating, token);
    }
}
