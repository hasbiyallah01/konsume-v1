

using konsume_v1.Models;
using Seek.Models;
using Seek.Models.UserModel;

namespace Seek.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<BaseResponse<UserResponse>> GetUser(int id);
        Task<BaseResponse<ICollection<UserResponse>>> GetAllUsers();
        Task<BaseResponse> RemoveUser(int id);
        Task<BaseResponse> UpdateUser(int id, UserRequest request);
        Task<BaseResponse<LoginResponse>> Login(LoginRequestModel model);
        Task<BaseResponse<UserResponse>> CreateUser(UserRequest request);
        Task<BaseResponse<LoginResponse>> GoogleLogin(string tokenId);
    }
}
