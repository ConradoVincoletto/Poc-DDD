using PocDDD.Application.DTOs;
using PocDDD.Application.Filters;

public interface IOrderService
{
    Task<ServiceResponseDTO<int>> InsertAsync(OrderDTO orderDTO);
    Task<ServiceResponseDTO<bool>> UpdateAsync(OrderDTO orderDTO);
    Task<ServiceResponseDTO<bool>> DeleteAsync(int id);
    Task<ServiceResponseDTO<List<OrderDTO>>> GetAllAsync(UserFilter userFilter);
    Task<ServiceResponseDTO<OrderDTO>> GetByIdAsync(int id);
}
