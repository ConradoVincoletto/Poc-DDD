using PocDDD.Domain.Entities;

namespace PocDDD.Domain.Interfaces
{
    public interface IUserRespository
    {
        Task<User> InsertAsync(User user);
    }
}
