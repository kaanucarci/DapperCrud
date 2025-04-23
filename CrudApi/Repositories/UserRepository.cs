using System.Data;
using CrudApi.Models;
using CrudApi.Data;
using Dapper;

namespace CrudApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DapperContext _context;

    public UserRepository(DapperContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Users>> GetAll()
    {
        var sql = "SELECT * FROM Users";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Users>(sql);
    }

    public Task<Users> GetById(int id)
    {
        var query = "SELECT * FROM Users WHERE Id = @Id";
        using var connection = _context.CreateConnection();
        if (connection.State != ConnectionState.Open)
            Console.WriteLine("Bağlantı kapalı!");

        return connection.QuerySingleOrDefaultAsync<Users>(query, new { Id = id });
    }

    public async Task<bool> Create(Users user)
    {
        var query = "INSERT INTO Users (Name, Email) VALUES (@Name, @Email)";
        using var connection = _context.CreateConnection();
        var result = await connection.ExecuteAsync(query, user);
        return result > 0;
    }


    public Task<Users> Update(Users user)
    {
        var query = "UPDATE Users SET Name = @Name, Email = @Email WHERE Id = @Id";
        using var connection = _context.CreateConnection();
        return connection.QuerySingleOrDefaultAsync<Users>(query, user);
    }

    public Task<Users> Delete(int id)
    {
        var query = "DELETE FROM Users WHERE Id = @Id";
        using var connection = _context.CreateConnection();
        return connection.QuerySingleOrDefaultAsync<Users>(query, new { Id = id });
    }
}