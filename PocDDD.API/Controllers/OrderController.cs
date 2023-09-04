using Microsoft.AspNetCore.Mvc;
using PocDDD.Application.DTOs;

namespace PocDDD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponseDTO<int>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ServiceResponseDTO<int>), StatusCodes.Status500InternalServerError)]
        [Route("insert")]

        public async Task<IActionResult> Insert(OrderDTO orderDTO)
        {
            ServiceResponseDTO<int> serviceResponseModel = await _orderService.InsertAsync(orderDTO);
            return StatusCode((int)serviceResponseModel.StatusCode, serviceResponseModel);
        }
    }
}
