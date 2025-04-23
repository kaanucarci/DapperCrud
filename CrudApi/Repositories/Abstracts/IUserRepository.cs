using CrudApi.Models;

namespace CrudApi.Repositories.Abstracts;

public interface IUserRepository
{
    Task <IEnumerable<Users>> GetAll();
    Task<Users> GetById(int id);
    Task<Users> Create(Users user);
    Task<Users> Update(Users user);
    Task<int> Delete(int id);
}