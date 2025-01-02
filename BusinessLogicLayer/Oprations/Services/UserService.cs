using DataAccessLayer.Contracts;
using DataAccessLayer.Entities;
using Microsoft.IdentityModel.Tokens;
using UserProjectBusinessLogicLayer.Contracts.Services;
using UserProjectBusinessLogicLayer.Contracts.Validations;

namespace UserProjectBusinessLogicLayer.Oprations.Services;
public class UserService : IUserService
{
    private readonly IRepositoryFactory _repositoryFactory;
    private readonly IUserValidator _userValidator;

    public UserService(IRepositoryFactory repositoryFactory, IUserValidator userValidator)
    {
        _repositoryFactory = repositoryFactory;
        _userValidator = userValidator;
    }

    public async Task AddUserAsync(UserModel user)
    {
        //validate user
        var validationResult = await _userValidator.ValidateAsync(user);
        if (!validationResult.IsValid)
        {
            throw new ArgumentException("Invalid user data.");
        }

        // Select Repository
        var userRepository = _repositoryFactory.CreateRepository<UserModel>();
        await userRepository.AddAsync(user);
    }

    public async Task<IEnumerable<UserModel>> GetUsersAsync()
    {
        var userRepository = _repositoryFactory.CreateRepository<UserModel>();
        return await userRepository.GetAllAsync();
    }

    public async Task<UserModel?> GetUserByIdAsync(int id)
    {
        var userRepository = _repositoryFactory.CreateRepository<UserModel>();
        return await userRepository.GetByIdAsync(id);
    }

    public async Task UpdateUserAsync(UserModel user)
    {
        var validationResult = await _userValidator.ValidateAsync(user);
        if (!validationResult.IsValid)
        {
            throw new ArgumentException("Invalid user data.");
        }

        var userRepository = _repositoryFactory.CreateRepository<UserModel>();
        await userRepository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        var userRepository = _repositoryFactory.CreateRepository<UserModel>();
        await userRepository.DeleteAsync(id);
    }
}