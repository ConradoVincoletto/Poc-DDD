using Microsoft.AspNetCore.Mvc;
using PocDDD.Application.DTOs;
using PocDDD.Application.Interfaces;

namespace PocDDD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponseDTO<int>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ServiceResponseDTO<int>), StatusCodes.Status500InternalServerError)]
        [Route("register")]

        public async Task<IActionResult> Register([FromBody] UserToInsertDTO userToInsertDTO)
        {
            ServiceResponseDTO<int> serviceResponseModel = await _userService.InsertAsync(userToInsertDTO);
            return StatusCode((int)serviceResponseModel.StatusCode, serviceResponseModel);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ServiceResponseDTO<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponseDTO<string>), StatusCodes.Status500InternalServerError)]
        [Route("login")]
        public async Task<IActionResult> Login([FromQuery] UserLoginDTO userLoginDTO)
        {
            ServiceResponseDTO<string> serviceResponseModel = await _userService.LoginAsync(userLoginDTO);
            return StatusCode((int)serviceResponseModel.StatusCode, serviceResponseModel);
        }
    }
}
