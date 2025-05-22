using Dapper;

namespace ToranjQ.App.Database;

public class DbInitializer(IDbConnectionFactory dbConnectionFactory)
{
    public async Task InitializeAsync()
    {
        using var connection = await dbConnectionFactory.CreateConnectionAsync();

        await connection.ExecuteAsync("""
                                      create table if not exists users (
                                          id serial primary key,
                                          phone text not null
                                      );
                                      """);
        
        await connection.ExecuteAsync("""
                                      create table if not exists questionnaires (
                                          id serial primary key,
                                          name text not null
                                      );
                                      """);
        
        await connection.ExecuteAsync("""
                                      create table if not exists answers (
                                          id uuid primary key,
                                          questionnaire_id integer not null references questionnaires(id),
                                          user_id integer not null references users(id),
                                          answer_str text not null
                                      );
                                      """);
    }
}