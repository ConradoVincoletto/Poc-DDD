using AutoMapper;
using PocDDD.Application.DTOs;
using PocDDD.Application.Filters;
using PocDDD.Application.Interfaces;
using PocDDD.Domain.Entities;
using PocDDD.Domain.Interfaces;
using PocDDD.Domain.Validation;
using System.Net;

namespace PocDDD.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRespository _userRespository;
        private readonly IMapper _mapper;

        public UserService(IUserRespository userRespository, IMapper mapper)
        {
            _userRespository = userRespository;
            _mapper = mapper;
        }

        public async Task<ServiceResponseDTO<int>> InsertAsync(UserToInsertDTO userToInsertModel)
        {
            ServiceResponseDTO<int> serviceResponseDTO = new ServiceResponseDTO<int>();
            try
            {
                User user = _mapper.Map<UserToInsertDTO, User>(userToInsertModel);
                user = await _userRespository.InsertAsync(user);
                serviceResponseDTO.StatusCode = HttpStatusCode.Created;
                serviceResponseDTO.Data = user.Id;
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
                if (user == null || user.Id == 0)
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

        public Task<ServiceResponseDTO<string>> LoginAsync(UserLoginDTO userLoginDTO)
        {
            throw new NotImplementedException();
        }
    }
}
