using ToranjQ.App.Models;

namespace ToranjQ.App.Services;

public interface IAnswerService
{
    Task<bool> CreateAsync(Answer answer, CancellationToken token = default);
    
    Task<Answer?> GetByIdAsync(Guid id, CancellationToken token = default);
    
    Task<IEnumerable<Answer>> GetAllAsync(CancellationToken token = default);
    
    Task<Answer?> UpdateAsync(Answer answer, CancellationToken token = default);
    
    Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);
}