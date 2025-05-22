namespace ToranjQ.Contracts.Requests;

public class UpdateAnswerRequest
{
    public required int UserId { get; init; }
    
    public required int QuestionnaireId { get; init; }
    
    public required string AnswerStr { get; init; }
}