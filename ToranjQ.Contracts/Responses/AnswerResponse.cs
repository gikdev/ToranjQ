namespace ToranjQ.Contracts.Responses;

public class AnswerResponse
{
    public required Guid Id { get; init; }
    
    public required int UserId { get; init; }
    
    public required int QuestionnaireId { get; init; }
    
    public required string AnswerStr { get; init; }
}