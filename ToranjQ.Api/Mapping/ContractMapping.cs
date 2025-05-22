using ToranjQ.App.Models;
using ToranjQ.Contracts.Requests;
using ToranjQ.Contracts.Responses;

namespace ToranjQ.Api.Mapping;

public static class ContractMapping
{
    public static Answer MapToAnswer(this CreateAnswerRequest req)
    {
        var answer = new Answer
        {
            Id = Guid.NewGuid(),
            AnswerStr = req.AnswerStr,
            QuestionnaireId = req.QuestionnaireId,
            UserId = req.UserId,
        };
        return answer;
    }

    
    public static Answer MapToAnswer(this UpdateAnswerRequest req, Guid id)
    {
        var answer = new Answer
        {
            Id = id,
            AnswerStr = req.AnswerStr,
            QuestionnaireId = req.QuestionnaireId,
            UserId = req.UserId,
        };
        return answer;
    }

    public static AnswerResponse MapToResponse(this Answer answer)
    {
        var res = new AnswerResponse
        {
            Id = answer.Id,
            AnswerStr = answer.AnswerStr,
            QuestionnaireId = answer.QuestionnaireId,
            UserId = answer.UserId,
        };
        return res;
    }
    
    public static AnswersResponse MapToResponse(this IEnumerable<Answer> answers)
    {
        var res = new AnswersResponse
        {
            Items = answers.Select(MapToResponse)
        };
        return res;
    }
}