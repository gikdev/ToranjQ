using System.Transactions;
using Dapper;
using ToranjQ.App.Database;
using ToranjQ.App.Models;

namespace ToranjQ.App.Repositories;

public class AnswerRepo(IDbConnectionFactory dbConnectionFactory) : IAnswerRepo
{
    public async Task<bool> CreateAsync(Answer answer, CancellationToken token = default)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();
        var insertAnswerCommand = new CommandDefinition("""
                                                        insert into answers (id, user_id, questionnaire_id, answer_str)
                                                        values (@Id, @UserId, @QuestionnaireId, @AnswerStr)
                                                        """, answer, cancellationToken: token);
        var result = await connection.ExecuteAsync(insertAnswerCommand);
        transaction.Commit();
        return result > 0;
    }

    public async Task<Answer?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync(token);
        var getByIdCommand = new CommandDefinition("select * from answers where id = @Id", new { id }, cancellationToken: token);
        var answer = await connection.QuerySingleOrDefaultAsync<Answer>(getByIdCommand);
        return answer ?? null;
    }

    public async Task<IEnumerable<Answer>> GetAllAsync(CancellationToken token = default)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync(token);
        var getAllCommand = new CommandDefinition("select * from answers", cancellationToken: token);
        var result = await connection.QueryAsync(getAllCommand);
        var finalResult = result.Select(a => new Answer
        {
            Id = a.Id,
            UserId = a.UserId,
            QuestionnaireId = a.QuestionnaireId,
            AnswerStr = a.AnswerStr
        });
        return finalResult;
    }

    public async Task<bool> UpdateAsync(Answer answer, CancellationToken token = default)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();

        var command = new CommandDefinition("""
                                            update answers 
                                            set 
                                                answer_str = @AnswerStr, 
                                                user_id = @UserId, 
                                                questionnaire_id = @QuestionnaireId, 
                                            where id = @Id
                                            """, answer, cancellationToken: token);

        var changesCount = await connection.ExecuteAsync(command);
        var result = changesCount > 0;

        transaction.Commit();
        return result;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();
        
        var command = new CommandDefinition("delete from answers where id = @Id", new { id }, cancellationToken: token);
        var changesCount = await connection.ExecuteAsync(command);
        var result = changesCount > 0;

        transaction.Commit();
        return result;
    }

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync(token);
        var command = new CommandDefinition("select count(1) from answers where id = @Id", new { id }, cancellationToken: token);
        var result = await connection.ExecuteScalarAsync<bool>(command);
        return result;
    }
}