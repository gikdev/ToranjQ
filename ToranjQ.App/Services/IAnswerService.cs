using ToranjQ.App.Models;

namespace ToranjQ.App.Services;

public interface IAnswerService
{
    Task<bool> CreateAsync(Answer answer);
    
    Task<Answer?> GetByIdAsync(Guid id);
    
    Task<IEnumerable<Answer>> GetAllAsync();
    
    Task<Answer?> UpdateAsync(Answer answer);
    
    Task<bool> DeleteByIdAsync(Guid id);
}