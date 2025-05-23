using FluentValidation;
using ToranjQ.App.Models;
using ToranjQ.App.Repositories;

namespace ToranjQ.App.Services;

public class AnswerService(IAnswerRepo answerRepo, IValidator<Answer> answerValidator) : IAnswerService
{
    public async Task<bool> CreateAsync(Answer answer, CancellationToken token = default)
    {
        await answerValidator.ValidateAndThrowAsync(answer, cancellationToken: token);
        return await answerRepo.CreateAsync(answer, token);
    }

    public Task<Answer?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return answerRepo.GetByIdAsync(id, token);
    }

    public Task<IEnumerable<Answer>> GetAllAsync(CancellationToken token = default)
    {
        return answerRepo.GetAllAsync(token);
    }

    public async Task<Answer?> UpdateAsync(Answer answer, CancellationToken token = default)
    {
        await answerValidator.ValidateAndThrowAsync(answer, cancellationToken: token);
        var doesAnswerExist = await answerRepo.ExistsByIdAsync(answer.Id, token);
        if (!doesAnswerExist) return null;
        await answerRepo.UpdateAsync(answer, token);
        return answer;
    }

    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        return answerRepo.DeleteByIdAsync(id, token);
    }
}