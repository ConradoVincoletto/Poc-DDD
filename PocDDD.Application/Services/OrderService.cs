using AutoMapper;
using PocDDD.Application.DTOs;
using PocDDD.Application.Filters;
using PocDDD.Domain.Entities;
using PocDDD.Domain.Interfaces;
using System.Net;

namespace PocDDD.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRespository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _orderRespository = orderRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Task<ServiceResponseDTO<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponseDTO<List<OrderDTO>>> GetAllAsync(UserFilter userFilter)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponseDTO<OrderDTO>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponseDTO<int>> InsertAsync(OrderDTO orderDTO)
        {
            ServiceResponseDTO<int> serviceResponseDTO = new ServiceResponseDTO<int>();
            try
            {
                Order order = new Order(orderDTO.IsActive,
                                    orderDTO.UserId,
                                    orderDTO.CreatAt,
                                    orderDTO.TotalPrice);

                order = await _orderRespository.InsertAsync(order);
                throw new Exception("Contexto de transação");

                order = await _orderRespository.InsertAsync(order);
                serviceResponseDTO.StatusCode = HttpStatusCode.Created;
                serviceResponseDTO.Data = order.OrderId;
            }
            catch (Exception ex)
            {
                serviceResponseDTO = new ServiceResponseDTO<int>(ex);
                await _unitOfWork.RollbackAscync();
            }
            finally
            {
                await _unitOfWork.CommitAsync();
            }
            return serviceResponseDTO;
        }

        public Task<ServiceResponseDTO<bool>> UpdateAsync(OrderDTO orderDTO)
        {
            throw new NotImplementedException();
        }
    }
}
