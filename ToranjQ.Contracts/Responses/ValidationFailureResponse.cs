using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToranjQ.Contracts.Responses;

public class ValidationFailureResponse
{
    [Required]
    [Description("List of validation errors")]
    public required IEnumerable<ValidationResponse> Errors { get; init; }
}

public class ValidationResponse
{
    [Required]
    [Description("Name of the property")]
    public required string PropertyName { get; init; }
    
    [Required]
    [Description("The [error] message related to this property")]
    public required string Message { get; init; }
}