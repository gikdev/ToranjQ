using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToranjQ.Contracts.Requests;

public class UpdateAnswerRequest
{
    [Required]
    [Description("The ID of the user")]
    public required int UserId { get; init; }

    [Required]
    [Description("The ID of the related questionnaire")]
    public required int QuestionnaireId { get; init; }

    [Required]
    [Description("The answers as a single string seperated by comma (,)")]
    public required string AnswerStr { get; init; }
}