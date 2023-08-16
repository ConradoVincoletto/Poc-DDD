using PocDDD.Domain.Entities;
using PocDDD.Domain.Interfaces;
using PocDDD.Infra.Data.Context;

namespace PocDDD.Infra.Data.Repositories
{
    public class UserRepository : IUserRespository
    {

        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> InsertAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var response = new
            {
                Id = user.Id,
                Message = $"Usuário com o {user.Id} criado com sucesso."
            };

            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
