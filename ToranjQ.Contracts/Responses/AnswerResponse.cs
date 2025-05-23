using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToranjQ.Contracts.Responses;

public class AnswerResponse
{
    [Required]
    [Description("ID of the answer")]
    public required Guid Id { get; init; }
    
    [Required]
    [Description("ID of the related user")]
    public required int UserId { get; init; }
    
    [Required]
    [Description("ID of the related questionnaire")]
    public required int QuestionnaireId { get; init; }
    
    [Required]
    [Description("The answers in a single string seperated/seperable by comma (,)")]
    public required string AnswerStr { get; init; }
}