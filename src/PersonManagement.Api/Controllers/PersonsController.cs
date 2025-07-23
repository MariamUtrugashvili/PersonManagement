using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonManagement.Application.Persons.Commands.CreatePerson;
using PersonManagement.Application.Persons.Commands.UpdatePerson;
using PersonManagement.Application.Persons.Commands.DeletePerson;
using PersonManagement.Application.Persons.Commands.AddRelatedPerson;
using PersonManagement.Application.Persons.Commands.DeleteRelatedPerson;
using PersonManagement.Application.Persons.Queries.GetPersonById;
using PersonManagement.Application.Persons.Queries.SearchPersons;
using Swashbuckle.AspNetCore.Filters;
using PersonManagement.Api.Examples.Requests;
using PersonManagement.Api.Examples.Responses;
using PersonManagement.Api.Examples.Responses.ErrorResponses;

namespace PersonManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController(ISender mediator) : ControllerBase
    {
        private readonly ISender _mediator = mediator;

        /// <summary>
        /// Gets a person by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <returns>The person details.</returns>
        /// <response code="200">Returns the person details.</response>
        /// <response code="404">Person not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetPersonByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetPersonByIdResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundErrorResponseExample))]
        public async Task<ActionResult<GetPersonByIdResponse>> GetById([FromRoute] GetPersonByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Searches for persons with optional filters and pagination.
        /// </summary>
        /// <returns>A list of persons matching the search criteria.</returns>
        /// <response code="200">Returns the list of persons.</response>
        [HttpGet]
        [ProducesResponseType(typeof(SearchPersonsResponse), StatusCodes.Status200OK)]
        [SwaggerRequestExample(typeof(SearchPersonsQuery), typeof(SearchPersonsQueryExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SearchPersonsResponseExample))]
        public async Task<ActionResult<SearchPersonsResponse>> Search([FromQuery] SearchPersonsQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new person.
        /// </summary>
        /// <param name="command">The person creation data.</param>
        /// <returns>The result of the creation.</returns>
        /// <response code="201">Returns the result id of the creation.</response>
        /// <response code="409">Person already exists.</response>
        [HttpPost]
        [ProducesResponseType(typeof(CreatePersonResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
        [SwaggerRequestExample(typeof(CreatePersonCommand), typeof(CreatePersonCommandExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(AlreadyExistsErrorResponseExample))]
        public async Task<ActionResult<CreatePersonResponse>> Create([FromBody] CreatePersonCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(
                nameof(GetById),
                new { result.Id },
                result);
        }

        /// <summary>
        /// Updates an existing person.
        /// </summary>
        /// <param name="command">The person update data.</param>
        /// <returns>The result of the update.</returns>
        /// <response code="204">Returns the result of the update.</response>
        /// <response code="404">Person not found.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [SwaggerRequestExample(typeof(UpdatePersonCommand), typeof(UpdatePersonCommandExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundErrorResponseExample))]
        public async Task<IActionResult> Update([FromBody] UpdatePersonCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Deletes a person by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <returns>The result of the deletion.</returns>
        /// <response code="204">Returns the result of the deletion.</response>
        /// <response code="404">Person not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [SwaggerRequestExample(typeof(DeletePersonCommand), typeof(DeletePersonCommandExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundErrorResponseExample))]
        public async Task<IActionResult> Delete(DeletePersonCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Adds a related person to a person.
        /// </summary>
        /// <param name="command">The related person data.</param>
        /// <returns>The result of the addition.</returns>
        /// <response code="204">Returns the result of the addition.</response>
        /// <response code="404">Person not found.</response>
        /// <response code="409">Conflict in adding related person.</response>
        [HttpPost("related")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
        [SwaggerRequestExample(typeof(AddRelatedPersonCommand), typeof(AddRelatedPersonCommandExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundErrorResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(AlreadyExistsErrorResponseExample))]
        public async Task<IActionResult> AddRelated([FromBody] AddRelatedPersonCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Deletes a related person from a person.
        /// </summary>
        /// <param name="command">The related person deletion data.</param>
        /// <returns>The result of the deletion.</returns>
        /// <response code="204">Returns the result of the deletion.</response>
        /// <response code="404">Person not found.</response>
        [HttpDelete("related")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [SwaggerRequestExample(typeof(DeleteRelatedPersonCommand), typeof(DeleteRelatedPersonCommandExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundErrorResponseExample))]
        public async Task<IActionResult> DeleteRelated([FromBody] DeleteRelatedPersonCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}
