using PocDDD.Domain.Entities;

namespace PocDDD.Domain.Interfaces
{
    public interface IUserRespository
    {
        Task<User> InsertAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<List<User>> GetAllAsync(int id, string firstName, string lastName);
        Task<User> GetById(int id);
        Task<bool> DeleteAsync(User user);
    }
}
