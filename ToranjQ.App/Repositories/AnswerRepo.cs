using System.Transactions;
using Dapper;
using ToranjQ.App.Database;
using ToranjQ.App.Models;

namespace ToranjQ.App.Repositories;

public class AnswerRepo : IAnswerRepo
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public AnswerRepo(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<bool> CreateAsync(Answer answer)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();
        var insertAnswerCommand = new CommandDefinition("""
                                                        insert into answers (id, user_id, questionnaire_id, answer_str)
                                                        values (@Id, @UserId, @QuestionnaireId, @AnswerStr)
                                                        """, answer);
        var result = await connection.ExecuteAsync(insertAnswerCommand);
        transaction.Commit();
        return result > 0;
    }

    public async Task<Answer?> GetByIdAsync(Guid id)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var getByIdCommand = new CommandDefinition("""
                                                   select * from answers where id = @Id 
                                                   """, new { id });
        var answer = await connection.QuerySingleOrDefaultAsync<Answer>(getByIdCommand);
        return answer ?? null;
    }

    public async Task<IEnumerable<Answer>> GetAllAsync()
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var getAllCommand = new CommandDefinition("""
                                                  select * from answers
                                                  """);
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

    public async Task<bool> UpdateAsync(Answer answer)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var command = new CommandDefinition("""
                                            update answers 
                                            set 
                                                answer_str = @AnswerStr, 
                                                user_id = @UserId, 
                                                questionnaire_id = @QuestionnaireId, 
                                            where id = @Id
                                            """, answer);

        var changesCount = await connection.ExecuteAsync(command);
        var result = changesCount > 0;

        transaction.Commit();
        return result;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();
        
        var command = new CommandDefinition("delete from answers where id = @Id", new { id });
        var changesCount = await connection.ExecuteAsync(command);
        var result = changesCount > 0;

        transaction.Commit();
        return result;
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var command = new CommandDefinition("select count(1) from answers where id = @Id", new { id });
        var result = await connection.ExecuteScalarAsync<bool>(command);
        return result;
    }
}