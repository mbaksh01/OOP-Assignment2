using Microsoft.AspNetCore.Mvc;
using Movies.Api.Contracts;
using Movies.Api.Extensions;
using Movies.Application.Models;
using Movies.Application.Services.Abstractions;

namespace Movies.Api.Controllers;

/// <summary>
/// Handles CRUD operations for movies.
/// </summary>
[ApiController]
[Route("movies")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    /// <summary>
    /// Constructor. Inject the required services.
    /// </summary>
    /// <param name="movieService">
    /// Repository used to control <see cref="Movie"/> objects.
    /// </param>
    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    /// <summary>
    /// Asynchronously gets all the movies that currently exist.
    /// </summary>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns>
    /// A HTTP response code indicating the status of the operation. If the
    /// operation was successful then the repones body will include a list of
    /// movies.
    /// </returns>
    /// <response code="200">Returns the list of movies.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MovieResponse>), 200)]
    public async Task<IActionResult> GetMovies(CancellationToken token)
    {
        IEnumerable<Movie> movies = await _movieService.GetAllMoviesAsync(token);

        if (!movies.Any())
        {
            return Ok(Enumerable.Empty<MovieResponse>());
        }

        List<MovieResponse> response = new();

        foreach (var movie in movies)
        {
            response.Add(movie.ToResponse());
        }

        return Ok(response);
    }

    /// <summary>
    /// Asynchronously gets a movie by its Id.
    /// </summary>
    /// <param name="id">Id of movie.</param>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns>
    /// A HTTP response code indicating the status of the operation. If the
    /// operation was successful then the response body will include the movie.
    /// </returns>
    /// <response code="200">Returns the requested movie.</response>
    /// <response code="404">The movie did not exist.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(MovieResponse), 200)]
    [ProducesResponseType(typeof(string), 404)]
    public async Task<IActionResult> GetMovieById(
        Guid id,
        CancellationToken token)
    {
        Movie? movie = await _movieService.GetMovieByIdAsync(id, token);

        if (movie is null)
        {
            return NotFound($"No movie matched the given id of '{id}'");
        }

        return Ok(movie.ToResponse());
    }

    /// <summary>
    /// Asynchronously creates a movie using the details provided in the
    /// <paramref name="request"/> body.
    /// </summary>
    /// <param name="request">
    /// Request body containing information required to create a movie.
    /// </param>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns>
    /// A HTTP response code indicating the status of the operation.
    /// </returns>
    /// <response code="201">Returns the newly created movie.</response>
    [HttpPost]
    [ProducesResponseType(typeof(MovieResponse), 201)]
    public async Task<IActionResult> CreateMovie(
        MovieRequest request,
        CancellationToken token)
    {
        Movie movie = request.ToMovie();

        movie = await _movieService.CreateMovieAsync(movie, token);

        MovieResponse response = movie.ToResponse();

        return CreatedAtAction(
            nameof(GetMovieById),
            new { id = response.Id },
            response
        );
    }

    /// <summary>
    /// Asynchronously replaces an existing movie with the movie provided in the
    /// request body.
    /// </summary>
    /// <param name="request">Movie which will replace old movie.</param>
    /// <param name="id">Id of movie to replace.</param>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns>
    /// A HTTP response code indicating the status of the operation.
    /// </returns>
    /// <response code="204">The movie was successfully updated.</response>
    /// <response code="404">The movie was not found.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(string), 404)]
    public async Task<IActionResult> UpdateMovie(
        Guid id,
        MovieRequest request,
        CancellationToken token)
    {
        Movie movie = request.ToMovie();

        movie.Id = id;

        Movie? response = await _movieService.UpdateMovieAsync(movie, token);

        if (response is null)
        {
            return NotFound($"No movie matched the given id of '{id}'");
        }

        return NoContent();
    }

    /// <summary>
    /// Asynchronously deletes a movie.
    /// </summary>
    /// <param name="id">Id of movie to delete.</param>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns>
    /// A HTTP response code indicating the status of the operation.
    /// </returns>
    /// <response code="204">The movie was successfully deleted.</response>
    /// <response code="404">The movie did not exist.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(string), 404)]
    public async Task<IActionResult> DeleteMovie(
        Guid id,
        CancellationToken token)
    {
        bool response = await _movieService.DeleteMovieByIdAsync(id, token);

        if (!response)
        {
            return NotFound($"No movie matched the given id of '{id}'");
        }

        return NoContent();
    }
}
