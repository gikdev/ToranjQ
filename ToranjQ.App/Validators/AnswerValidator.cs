using FluentValidation;
using ToranjQ.App.Models;

namespace ToranjQ.App.Validators;

public class AnswerValidator:AbstractValidator<Answer>
{
    public AnswerValidator()
    {
        RuleFor(a => a.Id).NotEmpty().NotNull();
        RuleFor(a => a.AnswerStr).NotEmpty().NotNull();
        RuleFor(a => a.QuestionnaireId).NotNull();
        RuleFor(a => a.UserId).NotNull();
    }
}