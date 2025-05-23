using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using ToranjQ.Api.Mapping;
using ToranjQ.App.Services;
using ToranjQ.Contracts.Requests;
using ToranjQ.Contracts.Responses;

namespace ToranjQ.Api.Controllers;

[ApiController]
public class AnswersController(IAnswerService answerService) : ControllerBase
{
    [HttpPost(ApiEndpoints.Answers.Create)]
    [EndpointSummary("Create a new answer")]
    [EndpointDescription("Creates a new answer using the provided request body.")]
    [ProducesResponseType(typeof(AnswerResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationFailureResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateAnswerRequest req, CancellationToken token)
    {
        var answer = req.MapToAnswer();
        var result = await answerService.CreateAsync(answer, token);
        return CreatedAtAction(nameof(Get), new { id = answer.Id }, answer);
    }

    [HttpGet(ApiEndpoints.Answers.Get)]
    [EndpointSummary("Get an answer by ID")]
    [EndpointDescription("Retrieves a specific answer using its unique identifier.")]
    [ProducesResponseType(typeof(AnswerResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(
        [FromRoute, Description("the unique ID of the answer")]
        Guid id,
        CancellationToken token
    )
    {
        var answer = await answerService.GetByIdAsync(id, token);
        if (answer is null) return NotFound();
        var res = answer.MapToResponse();
        return Ok(res);
    }

    [HttpGet(ApiEndpoints.Answers.GetAll)]
    [EndpointSummary("List all answers")]
    [EndpointDescription("Retrieves all answers from the DB.")]
    [ProducesResponseType(typeof(List<AnswerResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        var answers = await answerService.GetAllAsync(token);
        var answersRes = answers.MapToResponse();
        return Ok(answersRes);
    }

    [HttpPut(ApiEndpoints.Answers.Update)]
    [EndpointSummary("Update an existing answer")]
    [EndpointDescription("Updates the answer identified by the given ID with new values.")]
    [ProducesResponseType(typeof(AnswerResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationFailureResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromRoute, Description("The unique ID of the answer")]
        Guid id,
        [FromBody] UpdateAnswerRequest req,
        CancellationToken token
    )
    {
        var answer = req.MapToAnswer(id);
        var updatedAnswer = await answerService.UpdateAsync(answer, token);
        if (updatedAnswer is null) return NotFound();
        var res = updatedAnswer.MapToResponse();
        return Ok(res);
    }

    [HttpDelete(ApiEndpoints.Answers.Delete)]
    [EndpointSummary("Delete an answer")]
    [EndpointDescription("Deletes the answer specified by the given ID.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromRoute, Description("The unique ID of the answer")]
        Guid id,
        CancellationToken token
    )
    {
        var deleted = await answerService.DeleteByIdAsync(id, token);
        if (!deleted) return NotFound();
        return Ok();
    }
}