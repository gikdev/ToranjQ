using ToranjQ.App.Models;

namespace ToranjQ.App.Repositories;

public interface IAnswerRepo
{
    Task<bool> CreateAsync(Answer answer);
    
    Task<Answer?> GetByIdAsync(Guid id);
    
    Task<IEnumerable<Answer>> GetAllAsync();
    
    Task<bool> UpdateAsync(Answer answer);
    
    Task<bool> DeleteByIdAsync(Guid id);
    
    Task<bool> ExistsByIdAsync(Guid id);
}