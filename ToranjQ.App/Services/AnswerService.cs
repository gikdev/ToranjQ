using ToranjQ.App.Models;
using ToranjQ.App.Repositories;

namespace ToranjQ.App.Services;

public class AnswerService(IAnswerRepo answerRepo) : IAnswerService
{
    public Task<bool> CreateAsync(Answer answer)
    {
        return answerRepo.CreateAsync(answer);
    }

    public Task<Answer?> GetByIdAsync(Guid id)
    {
        return answerRepo.GetByIdAsync(id);
    }

    public Task<IEnumerable<Answer>> GetAllAsync()
    {
        return answerRepo.GetAllAsync();
    }

    public async Task<Answer?> UpdateAsync(Answer answer)
    {
        var doesAnswerExist = await answerRepo.ExistsByIdAsync(answer.Id);
        if (!doesAnswerExist) return null;
        await answerRepo.UpdateAsync(answer);
        return answer;
    }

    public Task<bool> DeleteByIdAsync(Guid id)
    {
        return answerRepo.DeleteByIdAsync(id);
    }
}