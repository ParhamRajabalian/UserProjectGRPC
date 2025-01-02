using DataAccessLayer.Entities;

namespace UserProjectBusinessLogicLayer.Contracts.Services;
public interface IUserService
{
    Task AddUserAsync(UserModel user);
    Task<IEnumerable<UserModel>> GetUsersAsync();
    Task<UserModel?> GetUserByIdAsync(int id);
    Task UpdateUserAsync(UserModel user);
    Task DeleteUserAsync(int id);
}
