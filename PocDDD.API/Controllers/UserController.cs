using Microsoft.AspNetCore.Mvc;
using PocDDD.Application.DTOs;
using PocDDD.Application.Interfaces;

namespace PocDDD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponseDTO<int>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Route("insert")]
        public async Task<IActionResult> Insert(UserToInsertDTO userToInsertModel)
        {
            ServiceResponseDTO<int> serviceResponseModel = await _userService.InsertAsync(userToInsertModel);
            if(serviceResponseModel.IsSuccess)
            {
                return Ok(serviceResponseModel);
            }
            else
            {
                return BadRequest(serviceResponseModel);
            }
        } 


    }
}
