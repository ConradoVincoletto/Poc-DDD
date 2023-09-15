using Microsoft.EntityFrameworkCore;
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

            return user;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            int result = await _context.SaveChangesAsync();
            return result == 1;
        }

        public async Task<bool> DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            int result = await _context.SaveChangesAsync();
            return result == 1;
        }

        public async Task<List<User>> GetAllAsync(int? id, string? firstName, string? lastName)
        {
            IQueryable<User> _users = _context.Set<User>()
                .Where(_user => (id != null && id != 0) ? _user.UserId == id : true)
                .Where(_user => !string.IsNullOrWhiteSpace(firstName) ? _user.FirstName.Contains(firstName) : true)
                .Where(_user => !string.IsNullOrWhiteSpace(lastName) ? _user.LastName.Contains(lastName) : true)
                .Include(x => x.Orders).AsNoTracking();
                

            List<User> users = await _users
                .AsNoTracking()
                .ToListAsync();

            return users;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            User user = await _context.Users
                .Include(x => x.Orders)
                .FirstOrDefaultAsync(_user => _user.UserId == id);
                
            return user;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            User user = await _context.Users.FirstOrDefaultAsync(_user => _user.Email == email);
            return user;
        }
    }
}
