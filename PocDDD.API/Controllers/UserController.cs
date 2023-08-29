using Microsoft.AspNetCore.Mvc;
using PocDDD.Application.DTOs;
using PocDDD.Application.Filters;
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
        [ProducesResponseType(typeof(ServiceResponseDTO<int>), StatusCodes.Status500InternalServerError)]
        [Route("insert")]
        public async Task<IActionResult> Insert(UserToInsertDTO userToInsertModel)
        {
            ServiceResponseDTO<int> serviceResponseModel = await _userService.InsertAsync(userToInsertModel);
            return StatusCode((int)serviceResponseModel.StatusCode, serviceResponseModel);    
        }

        [HttpGet]
        [ProducesResponseType(typeof(ServiceResponseDTO<UserDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponseDTO<UserDTO>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceResponseDTO<UserDTO>), StatusCodes.Status500InternalServerError)]
        [Route("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceResponseDTO<UserDTO> serviceResponseDTO = await _userService.GetByIdAsync(id);
            return StatusCode((int)serviceResponseDTO.StatusCode, serviceResponseDTO);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ServiceResponseDTO<List<UserDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponseDTO<List<UserDTO>>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceResponseDTO<List<UserDTO>>), StatusCodes.Status500InternalServerError)]
        [Route("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] UserFilter userFilter)
        {
            ServiceResponseDTO<List<UserDTO>> serviceResponseDTO = await _userService.GetAllAsync(userFilter);
            return StatusCode((int)serviceResponseDTO.StatusCode, serviceResponseDTO);
        }


        [HttpPut]
        [ProducesResponseType(typeof(ServiceResponseDTO<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponseDTO<bool>), StatusCodes.Status500InternalServerError)]
        [Route("update")]
        public async Task<IActionResult> Update(UserDTO userDTO)
        {
            ServiceResponseDTO<bool> serviceResponseModel = await _userService.UpdateAsync(userDTO);
            return StatusCode((int)serviceResponseModel.StatusCode, serviceResponseModel);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ServiceResponseDTO<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponseDTO<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceResponseDTO<bool>), StatusCodes.Status500InternalServerError)]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponseDTO<bool> serviceResponseDTO = await _userService.DeleteAsync(id);
            return StatusCode((int)serviceResponseDTO.StatusCode, serviceResponseDTO);
        }



    }
}
