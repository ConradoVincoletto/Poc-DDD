using Microsoft.EntityFrameworkCore;
using PocDDD.Domain.Entities;
using PocDDD.Domain.Interfaces;
using PocDDD.Infra.Data.Context;

namespace PocDDD.Infra.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {

        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteAsync(Order order)
        {
            order.IsActive = false;
            return await UpdateAsync(order);
        }

        public async Task<List<Order>> GetAllAsync(int? orderId, int? userId, DateTime? creatAtStart, DateTime? creatAtEnd)
        {
            IQueryable<Order> _order = _context.Set<Order>()
                .Where(_order => orderId != 0 ? _order.OrderId == orderId : true)
                .Where(_order => userId != 0 ? _order.UserId == userId : true)
                .Where(_order => creatAtStart != null ? _order.CreatAt.Date >= creatAtStart.Value.Date : true)
                .Where(_order => creatAtEnd != null ? _order.CreatAt.Date <= creatAtEnd.Value.Date : true);               


            List<Order> orders = await _order
                .AsNoTracking()
                .ToListAsync();

            return orders;
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            Order order = await _context.Orders.FirstOrDefaultAsync(_order => _order.OrderId == id);
            return order;
        }

        public async Task<Order> InsertAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<bool> UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            int result = await _context.SaveChangesAsync();
            return result == 1;
        }
    }
}
