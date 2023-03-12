using FluentAssertions;
using Movies.Api.Contracts;
using Movies.Api.Extensions;
using Movies.Application.Models;

namespace Movies.Api.Tests.Unit.Extensions;

public class MovieRatingExtensionsTests
{
    [Fact]
    public void SuccessfullyMapToMovieRating()
    {
        MovieRatingRequest request = new()
        {
            Rating = 5,
            MovieId = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };

        MovieRating rating = request.ToRating();

        rating.Rating.Should().Be(request.Rating);
        rating.MovieId.Should().Be(request.MovieId);
        rating.CreatedDate.Should().Be(request.CreatedDate);
        rating.UpdatedDate.Should().Be(request.UpdatedDate);
    }

    [Fact]
    public void SuccessfullyMapToResponse()
    {
        MovieRating rating = new()
        {
            Id = Guid.NewGuid(),
            Rating = 5,
            MovieId = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };

        MovieRatingResponse response = rating.ToResponse();

        response.Id.Should().Be(response.Id);
        response.Rating.Should().Be(response.Rating);
        response.MovieId.Should().Be(response.MovieId);
        response.CreatedDate.Should().Be(response.CreatedDate);
        response.UpdatedDate.Should().Be(response.UpdatedDate);
    }
}
