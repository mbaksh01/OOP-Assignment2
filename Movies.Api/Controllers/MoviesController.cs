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
        // Gets all the movies from the database.
        IEnumerable<Movie> movies = await _movieService.GetAllMoviesAsync(token);

        // If no movies exist then return a static instance of a list.
        if (!movies.Any())
        {
            return Ok(Enumerable.Empty<MovieResponse>());
        }

        // If movies do exist, create list of response objects and add movies.
        List<MovieResponse> response = new();

        foreach (var movie in movies)
        {
            response.Add(movie.ToResponse());
        }

        // Return 200 OK with the found movies.
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
        // Gets a movie by its id.
        Movie? movie = await _movieService.GetMovieByIdAsync(id, token);

        // If the movie is null then it was not found.
        if (movie is null)
        {
            return NotFound($"No movie matched the given id of '{id}'");
        }

        // Return 200 OK with the movie as a response.
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
        // Convert the request to a movie.
        Movie movie = request.ToMovie();

        // Add the movie to the database.
        movie = await _movieService.CreateMovieAsync(movie, token);

        // Convert the movie to a response.
        MovieResponse response = movie.ToResponse();

        // Return 201 Created with the response and the location.
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
        // Convert the request to a movie.
        Movie movie = request.ToMovie();

        // Set the id with the provided id.
        movie.Id = id;

        // Try to update the movie.
        Movie? response = await _movieService.UpdateMovieAsync(movie, token);

        // If response is null then the movie was not found.
        if (response is null)
        {
            return NotFound($"No movie matched the given id of '{id}'");
        }

        // Return 204 No Content if the operation was successfull.
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
        // Deletes a movie by its id.
        bool response = await _movieService.DeleteMovieByIdAsync(id, token);

        // If the response was false then the movie was not found.
        if (!response)
        {
            return NotFound($"No movie matched the given id of '{id}'");
        }

        // If the operation was successful then 204 No Content is returned.
        return NoContent();
    }
}
