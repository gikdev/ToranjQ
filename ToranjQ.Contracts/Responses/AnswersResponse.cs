namespace ToranjQ.Contracts.Responses;

public class AnswersResponse
{
    public required IEnumerable<AnswerResponse> Items { get; init; } = [];
}