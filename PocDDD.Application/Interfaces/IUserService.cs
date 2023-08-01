using PocDDD.Application.DTOs;

namespace PocDDD.Application.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponseDTO<int>> InsertAsync(UserToInsertDTO userToInsertModel);
        Task<ServiceResponseDTO<bool>> UpdateAsync(UserDTO userModel);
        Task<ServiceResponseDTO<bool>> DeleteAsync(int id);
        Task<ServiceResponseDTO<List<UserDTO>>> GetAllAsync(); 
    }
}
