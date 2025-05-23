using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToranjQ.Contracts.Responses;

public class AnswersResponse
{
    [Required]
    [Description("A list of answers")]
    public required IEnumerable<AnswerResponse> Items { get; init; } = [];
}