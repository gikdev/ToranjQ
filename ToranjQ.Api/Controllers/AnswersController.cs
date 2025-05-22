using Microsoft.AspNetCore.Mvc;
using ToranjQ.Api.Mapping;
using ToranjQ.App.Models;
using ToranjQ.App.Repositories;
using ToranjQ.App.Services;
using ToranjQ.Contracts.Requests;

namespace ToranjQ.Api.Controllers;

[ApiController]
public class AnswersController(IAnswerService answerService) : ControllerBase
{
    [HttpPost(ApiEndpoints.Answers.Create)]
    public async Task<IActionResult> Create([FromBody] CreateAnswerRequest req)
    {
        var answer = req.MapToAnswer();
        var result = await answerService.CreateAsync(answer);
        return CreatedAtAction(nameof(Get), new { id = answer.Id }, answer);
    }

    [HttpGet(ApiEndpoints.Answers.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var answer = await answerService.GetByIdAsync(id);
        if (answer is null) return NotFound();
        var res = answer.MapToResponse();
        return Ok(res);
    }

    [HttpGet(ApiEndpoints.Answers.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var answers = await answerService.GetAllAsync();
        var answersRes = answers.MapToResponse();
        return Ok(answersRes);
    }

    [HttpPut(ApiEndpoints.Answers.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateAnswerRequest req)
    {
        var answer = req.MapToAnswer(id);
        var updatedAnswer = await answerService.UpdateAsync(answer);
        if (updatedAnswer is null) return NotFound();
        var res = updatedAnswer.MapToResponse();
        return Ok(res);
    }

    [HttpDelete(ApiEndpoints.Answers.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleted = await answerService.DeleteByIdAsync(id);
        if (!deleted) return NotFound();
        return Ok();
    }
}