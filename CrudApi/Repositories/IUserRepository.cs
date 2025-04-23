using CrudApi.Models;

namespace CrudApi.Repositories;

public interface IUserRepository
{
    Task <IEnumerable<Users>> GetAll();
    Task<Users> GetById(int id);
    Task<bool> Create(Users user);
    Task<Users> Update(Users user);
    Task<Users> Delete(int id);
}