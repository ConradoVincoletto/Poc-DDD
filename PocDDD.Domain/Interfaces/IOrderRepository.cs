using PocDDD.Domain.Entities;

public interface IOrderRepository
{
    Task<Order> InsertAsync(Order order);
    Task<bool> UpdateAsync(Order order);
    Task<List<Order>> GetAllAsync(int? orderId, int? userId, DateTime? creatAtStart, DateTime? creatAtEnd);
    Task<Order> GetByIdAsync(int id);
    Task<bool> DeleteAsync(Order order);    
}
