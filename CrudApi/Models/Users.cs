namespace CrudApi.Models;

public class Users
{
    public int Id { get; set; } 
    public required string Name { get; set; }
    public required string Email { get; set; }
}