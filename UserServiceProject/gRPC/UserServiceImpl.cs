using DataAccessLayer.Entities;
using Grpc.Core;
using UserProjectBusinessLogicLayer.Contracts.Services;

namespace ApplicationLayer.gRPC;

public class UserServiceImpl : UserService.UserServiceBase
{
    private readonly IUserService _userService; 

    public UserServiceImpl(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
    {
        try
        {
            var user = await _userService.GetUserByIdAsync(request.Id);

            if (user == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
            }

            return new GetUserResponse
            {
                User = new User
                {
                    NationalCode = user.NationalCode,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BirthDate = user.BirthDate.ToString("yyyy-MM-dd")
                }
            };
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public override async Task<AddUserResponse> AddUser(AddUserRequest request, ServerCallContext context)
    {
        try
        {

            var user = new UserModel
            {
                NationalCode = request.User.NationalCode,
                FirstName = request.User.FirstName,
                LastName = request.User.LastName,
                BirthDate = DateTime.Parse(request.User.BirthDate)
            };

            await _userService.AddUserAsync(user);

            return new AddUserResponse { Success = true };

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public override async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
    {
        try
        {
            var user = new UserModel
            {
                NationalCode = request.User.NationalCode,
                FirstName = request.User.FirstName,
                LastName = request.User.LastName,
                BirthDate = DateTime.Parse(request.User.BirthDate)
            };

            await _userService.UpdateUserAsync(user);

            return new UpdateUserResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public override async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
    {
        try
        {
            await _userService.DeleteUserAsync(request.Id);

            return new DeleteUserResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
