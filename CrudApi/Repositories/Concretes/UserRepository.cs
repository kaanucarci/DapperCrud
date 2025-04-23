using System.Data;
using CrudApi.Models;
using CrudApi.Data;
using CrudApi.Repositories.Abstracts;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;

namespace CrudApi.Repositories.Concretes;

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

    public async Task<Users> GetById(int id)
    {
        var query = "SELECT * FROM Users WHERE Id = @Id";
        using var connection = _context.CreateConnection();

        connection.Open(); 
        if (connection.State == ConnectionState.Closed)
            Console.WriteLine("Connection state is closed");

        var user = await connection.QuerySingleOrDefaultAsync<Users>(query, new { Id = id });
        return user;
    }

    public async Task<Users> Create(Users user)
    {
        var query = "INSERT INTO Users (Name, Email) VALUES (@Name, @Email)";
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, user);
        return user;
    }


    public async Task<Users> Update(Users user)
    {
        var query = "UPDATE Users SET Name = @Name, Email = @Email WHERE Id = @Id";
    
        using var connection = (SqlConnection)_context.CreateConnection();
        await connection.OpenAsync(); 

        var affectedRows = await connection.ExecuteAsync(query, user);

        return user;
    }
    public async Task<int> Delete(int id)
    {
        var query = "DELETE FROM Users WHERE Id = @Id";
        using var connection = (SqlConnection)_context.CreateConnection();
        await connection.OpenAsync();

       var affectedRows = await connection.ExecuteAsync(query, new { Id = id });

       return affectedRows;
    }
}