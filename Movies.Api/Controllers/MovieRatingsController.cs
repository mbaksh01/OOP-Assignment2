using Microsoft.AspNetCore.Mvc;
using Movies.Api.Contracts;
using Movies.Api.Extensions;
using Movies.Application.Models;
using Movies.Application.Services.Abstractions;
using OneOf;
using OneOf.Types;

namespace Movies.Api.Controllers;

/// <summary>
/// Controller user to expose CRUD endpoints for movie ratings.
/// </summary>
[ApiController]
[Route("ratings")]
public class MovieRatingsController : ControllerBase
{
    private readonly IMovieRatingService _movieRatingService;

    /// <summary>
    /// Constructor. Injects the required services.
    /// </summary>
    /// <param name="movieRatingService">
    /// Rating service required to controll movie rating objects.
    /// </param>
    public MovieRatingsController(IMovieRatingService movieRatingService)
    {
        _movieRatingService = movieRatingService;
    }

    /// <summary>
    /// Asynchronously gets all the movie ratings that currently exist.
    /// </summary>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns>
    /// A HTTP response code indicating the status of the operation. If the
    /// operation was successful then the repones body will include a list of
    /// movie ratings.
    /// </returns>
    /// <response code="200">Returns the list of movie ratings.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MovieRatingResponse>), 200)]
    public async Task<IActionResult> GetAllRatings(CancellationToken token)
    {
        // Get all movies.
        IEnumerable<MovieRating> movieRatings =
            await _movieRatingService.GetAllMovieRatings(token);

        // If no movies exist return an static empty list.
        if (!movieRatings.Any())
        {
            // Return 200 OK with a list of responses.
            return Ok(Enumerable.Empty<MovieRatingResponse>());
        }

        // If movie ratings do exist create a list of responses and add the
        // converted movies.
        List<MovieRatingResponse> response = new();

        foreach (var movieRating in movieRatings)
        {
            response.Add(movieRating.ToResponse());
        }

        // Return 200 OK with a list of responses.
        return Ok(response);
    }

    /// <summary>
    /// Asynchronously gets a movie rating by its Id.
    /// </summary>
    /// <param name="id">Id of movie rating.</param>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns>
    /// A HTTP response code indicating the status of the operation. If the
    /// operation was successful then the response body will include the movie
    /// rating.
    /// </returns>
    /// <response code="200">Returns the requested movie rating.</response>
    /// <response code="404">The movie rating did not exist.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(MovieRatingResponse), 200)]
    [ProducesResponseType(typeof(string), 404)]
    public async Task<IActionResult> GetRatingById(
        Guid id,
        CancellationToken token)
    {
        // Gets the movie rating linked to the prodided details.
        MovieRating? rating =
            await _movieRatingService.GetMovieRatingById(id, token);

        // If no movie rating was found then return 404 Not Found.
        if (rating is null)
        {
            return NotFound($"No movie rating matched the given id of '{id}'.");
        }

        // If the movie was found return the movie rating as a response.
        return Ok(rating.ToResponse());
    }

    /// <summary>
    /// Asynchronously creates a movie rating using the details provided in the
    /// <paramref name="request"/> body.
    /// </summary>
    /// <param name="request">
    /// Request body containing information required to create a movie rating.
    /// </param>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns>
    /// A HTTP response code indicating the status of the operation. If the
    /// operation was successful then the response body will include the movie
    /// rating.
    /// </returns>
    /// <response code="201">Returns the newly created movie rating.</response>
    /// <response code="400">A validation error occurred.</response>
    [HttpPost]
    [ProducesResponseType(typeof(MovieRatingResponse), 201)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> CreateRating(
        MovieRatingRequest request,
        CancellationToken token)
    {
        // Converts request to rating.
        MovieRating rating = request.ToRating();

        // Tries to create the rating.
        OneOf<MovieRating, NotFound> createRatingResult =
            await _movieRatingService.CreateMovieRating(rating, token);

        // Returns the a HTTP response based on the result. 201 Created if
        // successful otherwise 400 Bad Request.
        return createRatingResult.Match<IActionResult>(
            movieRating
                => CreatedAtAction(
                    nameof(GetRatingById),
                    new { id = movieRating.Id },
                    movieRating.ToResponse()
                ),
            notFound
                => BadRequest($"No movie exists with id '{rating.MovieId}'.")
        ); 
    }

    /// <summary>
    /// Asynchronously updates an existing movie rating with the rating provided
    /// in the request body.
    /// </summary>
    /// <param name="rating">New movie rating.</param>
    /// <param name="id">Id of movie rating to update.</param>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns>
    /// A HTTP response code indicating the status of the operation.
    /// </returns>
    /// <response code="204">
    /// The movie rating was successfully updated.
    /// </response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(string), 404)]
    public async Task<IActionResult> UpdateRating(
        Guid id,
        [FromBody]int rating,
        CancellationToken token)
    {
        // Tries to update a rating.
        MovieRating? response =
            await _movieRatingService.UpdateMovieRating(id, rating, token);

        // If the response is null the rating was not found.
        if (response is null)
        {
            return NotFound($"No movie rating matched the given id of '{id}'.");
        }

        // Return no content is the operation successfully completed.
        return NoContent();
    }

    /// <summary>
    /// Asynchronously deletes a movie rating.
    /// </summary>
    /// <param name="id">Id of movie rating to delete.</param>
    /// <param name="token">
    /// <see cref="CancellationToken"/> which can be used to cancel this request.
    /// </param>
    /// <returns>
    /// A HTTP response code indicating the status of the operation.
    /// </returns>
    /// <response code="204">
    /// The movie rating was successfully deleted.
    /// </response>
    /// <response code="404">The movie rating did not exist.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(string), 204)]
    public async Task<IActionResult> DeleteRating(
        Guid id,
        CancellationToken token)
    {
        // Tried to delete a movie rating.
        bool response = await _movieRatingService.DeleteMovieRating(id, token);

        // If false is return then the movie rating was not found.
        if (!response)
        {
            return NotFound($"No movie rating matched the given id of '{id}'.");
        }

        // If the operation was succesful then no content is returned.
        return NoContent();
    }
}
