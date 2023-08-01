using PocDDD.Domain.Entities;
using PocDDD.Domain.Interfaces;

namespace PocDDD.Infra.Data.Repositories
{
    public class UserRepository : IUserRespository
    {

        public Task<int> InsertAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
