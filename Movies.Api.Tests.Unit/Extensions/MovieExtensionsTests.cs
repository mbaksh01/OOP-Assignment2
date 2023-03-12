using Movies.Api.Contracts;
using Movies.Api.Extensions;
using Movies.Application.Models;

namespace Movies.Api.Tests.Unit.Extensions;

public class MovieExtensionsTests
{
    [Fact]
    public void SuccessfullyMapToMovie()
    {
        MovieRequest request = new()
        {
            Title = "Title",
            Description = "Description",
            DateOfRelease = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };

        Movie movie = request.ToMovie();

        movie.Title.Should().Be(request.Title);
        movie.Description.Should().Be(request.Description);
        movie.DateOfRelease.Should().Be(request.DateOfRelease);
        movie.CreatedDate.Should().Be(request.CreatedDate);
        movie.UpdatedDate.Should().Be(request.UpdatedDate);
    }

    [Fact]
    public void SuccessfullyMapToResponse()
    {
        Movie movie = new()
        {
            Id = Guid.NewGuid(),
            Title = "Title",
            Description = "Description",
            DateOfRelease = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };

        MovieResponse response = movie.ToResponse();

        response.Id.Should().Be(movie.Id);
        response.Title.Should().Be(movie.Title);
        response.Description.Should().Be(movie.Description);
        response.DateOfRelease.Should().Be(movie.DateOfRelease);
        response.CreatedDate.Should().Be(movie.CreatedDate);
        response.UpdatedDate.Should().Be(movie.UpdatedDate);
    }
}
