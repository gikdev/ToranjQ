namespace ToranjQ.App.Models;

public class Answer
{
    public required Guid Id { get; init; }
    
    public required int UserId { get; init; }
    
    public required int QuestionnaireId { get; init; }
    
    public required string AnswerStr { get; init; }
}