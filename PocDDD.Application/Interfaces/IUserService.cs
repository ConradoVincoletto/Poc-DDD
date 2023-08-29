using PocDDD.Application.DTOs;
using PocDDD.Application.Filters;

namespace PocDDD.Application.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponseDTO<int>> InsertAsync(UserToInsertDTO userToInsertModel);
        Task<ServiceResponseDTO<bool>> UpdateAsync(UserDTO userModel);
        Task<ServiceResponseDTO<bool>> DeleteAsync(int id);
        Task<ServiceResponseDTO<List<UserDTO>>> GetAllAsync(UserFilter userFilter);
        Task<ServiceResponseDTO<UserDTO>> GetByIdAsync(int id);
        Task<ServiceResponseDTO<string>> LoginAsync(UserLoginDTO userLoginDTO);
    }
}
