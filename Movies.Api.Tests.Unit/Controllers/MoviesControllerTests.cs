using Microsoft.AspNetCore.Mvc;
using Moq;
using Movies.Api.Contracts;
using Movies.Api.Controllers;
using Movies.Application.Models;
using Movies.Application.Services.Abstractions;

namespace Movies.Api.Tests.Unit.Controllers;

public class MoviesControllerTests
{
    private readonly Mock<IMovieService> _movieServiceMock = new();

    [Fact]
    public async Task CreatingAMovieReturnCreatedAtResult()
    {
        // Arrange
        MovieRequest request = new()
        {
            Title = "Title",
            Description = "Description",
            DateOfRelease = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };

        _ = _movieServiceMock
            .Setup(m =>
                m.CreateMovieAsync(It.IsAny<Movie>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Movie
            {
                Id = Guid.NewGuid(),
                Title = "Title",
                Description = "Description",
                DateOfRelease = request.DateOfRelease,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            });

        // Act
        MoviesController sut = new(_movieServiceMock.Object);

        var response = await sut.CreateMovie(request, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        
        var created = response.Should().BeOfType<CreatedAtActionResult>().Subject;
        created.Value.Should().NotBeNull();
        
        var movie = created.Value.Should().BeOfType<MovieResponse>().Subject;
        movie.Title.Should().Be(request.Title);
        movie.Description.Should().Be(request.Description);
        
        movie.DateOfRelease.Should().Be(request.DateOfRelease);
        
        // Tracking dates should not be set by the user.
        movie.CreatedDate.Should().NotBe(request.CreatedDate);
        movie.UpdatedDate.Should().NotBe(request.UpdatedDate);
    }

    [Fact]
    public async Task UpdatingShouldReturnNoContent()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        MovieRequest request = new()
        {
            Title = "Title",
            Description = "Description",
            DateOfRelease = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };

        _ = _movieServiceMock
            .Setup(m =>
                m.UpdateMovieAsync(It.IsAny<Movie>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Movie
            {
                Id = id,
                Title = "Title",
                Description = "Description",
                DateOfRelease = request.DateOfRelease,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            });

        // Act
        MoviesController sut = new(_movieServiceMock.Object);

        var response = await sut.UpdateMovie(id, request, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task DeletingShouldReturnNoContent()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        _ = _movieServiceMock
            .Setup(m => m.DeleteMovieByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        MoviesController sut = new(_movieServiceMock.Object);

        var response = await sut.DeleteMovie(id, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Should().BeOfType<NoContentResult>();
    }
}
