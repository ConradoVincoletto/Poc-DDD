using AutoMapper;
using PocDDD.Application.DTOs;
using PocDDD.Application.Interfaces;
using PocDDD.Domain.Entities;
using PocDDD.Domain.Interfaces;

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
            User user = _mapper.Map<UserToInsertDTO, User>(userToInsertModel);
            //User user = new User(0, userToInsertModel.FirstName, userToInsertModel.LastName, userToInsertModel.Email, userToInsertModel.Password);
            await _userRespository.InsertAsync(user);
            return _mapper.Map<User, ServiceResponseDTO<int>>(user);
        }

        public Task<ServiceResponseDTO<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponseDTO<List<UserDTO>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }       

        public async Task<ServiceResponseDTO<bool>> UpdateAsync(UserDTO userModel)
        {
            User user = _mapper.Map<UserDTO, User>(userModel);
            await _userRespository.UpdateAsync(user);
            return _mapper.Map<User, ServiceResponseDTO<bool>>(user);
        }
    }
}
