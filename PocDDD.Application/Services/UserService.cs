using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PocDDD.Application.DTOs;
using PocDDD.Application.Filters;
using PocDDD.Application.Interfaces;
using PocDDD.Domain.Entities;
using PocDDD.Domain.Interfaces;
using PocDDD.Domain.Validation;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace PocDDD.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRespository _userRespository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IUserRespository userRespository, IMapper mapper, IConfiguration configuration)
        {
            _userRespository = userRespository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<ServiceResponseDTO<int>> InsertAsync(UserToInsertDTO userToInsertModel)
        {
            ServiceResponseDTO<int> serviceResponseDTO = new ServiceResponseDTO<int>();
            try
            {
                User user = new User(true,
                                    userToInsertModel.FirstName,
                                    userToInsertModel.LastName,
                                    userToInsertModel.Email, 
                                    userToInsertModel.Password);

                user = await _userRespository.InsertAsync(user);
                serviceResponseDTO.StatusCode = HttpStatusCode.Created;
                serviceResponseDTO.Data = user.UserId;
            }
            catch (Exception ex)
            {
                serviceResponseDTO = new ServiceResponseDTO<int>(ex);
            }
            return serviceResponseDTO;

        }

        public async Task<ServiceResponseDTO<bool>> UpdateAsync(UserDTO userModel)
        {
            ServiceResponseDTO<bool> serviceResponseDTO = new ServiceResponseDTO<bool>();
            try
            {
                User user = _mapper.Map<User>(userModel);
                bool result = await _userRespository.UpdateAsync(user);
                serviceResponseDTO.StatusCode = HttpStatusCode.OK;
                serviceResponseDTO.Data = result;

            }
            catch (DomainExceptionValidation ex)
            {
                serviceResponseDTO = new ServiceResponseDTO<bool>(HttpStatusCode.UnprocessableEntity);
                serviceResponseDTO.Message = ex.GetBaseException().Message;
            }
            catch (Exception ex)
            {
                serviceResponseDTO = new ServiceResponseDTO<bool>(ex);
            }
            return serviceResponseDTO;
        }

        public async Task<ServiceResponseDTO<List<UserDTO>>> GetAllAsync(UserFilter userFilter)
        {
            ServiceResponseDTO<List<UserDTO>> serviceResponseDTO = new ServiceResponseDTO<List<UserDTO>>();
            try
            {
                List<User> users = await _userRespository.GetAllAsync(userFilter.Id, userFilter.FirstName, userFilter.LastName);
                List<UserDTO> userDTOs = _mapper.Map<List<UserDTO>>(users);
                if (userDTOs == null || userDTOs.Count == 0)
                {
                    serviceResponseDTO = new ServiceResponseDTO<List<UserDTO>>(HttpStatusCode.NotFound);
                }
                else
                {
                    serviceResponseDTO.Data = userDTOs;
                }
            }
            catch (Exception ex)
            {
                serviceResponseDTO = new ServiceResponseDTO<List<UserDTO>>(ex);
            }
            return serviceResponseDTO;
        }

        public async Task<ServiceResponseDTO<UserDTO>> GetByIdAsync(int id)
        {
            ServiceResponseDTO<UserDTO> serviceResponseDTO = new ServiceResponseDTO<UserDTO>();
            try
            {
                User user = await _userRespository.GetByIdAsync(id);
                if (user == null || user.UserId == 0)
                {
                    serviceResponseDTO = new ServiceResponseDTO<UserDTO>(HttpStatusCode.NotFound);
                }
                else
                {
                    UserDTO userDTO = _mapper.Map<UserDTO>(user);
                    serviceResponseDTO.Data = userDTO;
                }

            }
            catch (Exception ex)
            {
                serviceResponseDTO = new ServiceResponseDTO<UserDTO>(ex);
            }
            return serviceResponseDTO;
        }

        public async Task<ServiceResponseDTO<bool>> DeleteAsync(int id)
        {
            ServiceResponseDTO<bool> serviceResponseDTO = new ServiceResponseDTO<bool>();
            try
            {

            }
            catch (Exception ex)
            {
                serviceResponseDTO = new ServiceResponseDTO<bool>(ex);
            }
            return serviceResponseDTO;
        }

        public async Task<ServiceResponseDTO<string>> LoginAsync(UserLoginDTO userLoginDTO)
        {
            ServiceResponseDTO<string> serviceResponseDTO = new ServiceResponseDTO<string>();
            try
            {
                User user = await _userRespository.GetByEmailAsync(userLoginDTO.Email);
                if (user == null || user.UserId == 0)
                {
                    serviceResponseDTO.IsSuccess = false;
                    serviceResponseDTO.Message = "Usuário não encontrado";
                    serviceResponseDTO.StatusCode = HttpStatusCode.Unauthorized;
                    return serviceResponseDTO;
                }
                else if (!user.CheckHash(userLoginDTO.Password, user.PasswordHash, user.PasswordSalt))
                {
                    serviceResponseDTO.IsSuccess = false;
                    serviceResponseDTO.Message = "Senha inválida";
                    serviceResponseDTO.StatusCode = HttpStatusCode.Unauthorized;
                    return serviceResponseDTO;
                }
                string token = CreateToken(user);
                serviceResponseDTO.Data = token;
            }
            catch (Exception ex)
            {
                serviceResponseDTO = new ServiceResponseDTO<string>(ex);
            }
            return serviceResponseDTO;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Role, "Admin1"),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (_configuration.GetSection("AppSettings:Token").Value));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = signingCredentials
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }
    }
}
