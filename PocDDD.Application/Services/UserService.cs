using PocDDD.Application.DTOs;
using PocDDD.Application.Interfaces;

namespace PocDDD.Application.Services
{
    public class UserService : IUserService
    {

        public Task<ServiceResponseDTO<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponseDTO<List<UserDTO>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponseDTO<int>> InsertAsync(UserToInsertDTO userToInsertModel)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponseDTO<bool>> UpdateAsync(UserDTO userModel)
        {
            throw new NotImplementedException();
        }
    }
}
