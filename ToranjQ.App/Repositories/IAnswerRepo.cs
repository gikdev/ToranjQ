using ToranjQ.App.Models;

namespace ToranjQ.App.Repositories;

public interface IAnswerRepo
{
    Task<bool> CreateAsync(Answer answer, CancellationToken token = default);

    Task<Answer?> GetByIdAsync(Guid id, CancellationToken token = default);

    Task<IEnumerable<Answer>> GetAllAsync(CancellationToken token = default);

    Task<bool> UpdateAsync(Answer answer, CancellationToken token = default);

    Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);

    Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default);
}