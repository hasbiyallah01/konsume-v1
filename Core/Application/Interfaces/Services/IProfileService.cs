using konsume_v1.Models;
using konsume_v1.Models.ProfileModel;

namespace konsume_v1.Core.Application.Interfaces.Services
{
    public interface IProfileService
    {
        Task<BaseResponse<ProfileResponse>> GetProfile(int id);
        Task<BaseResponse<ICollection<ProfileResponse>>> GetAllProfiles();
        Task<BaseResponse> RemoveProfile(int id);
        Task<BaseResponse<bool>> GetProfileByUserId(int id);
        Task<BaseResponse<ProfileResponse>> CreateProfile(int Userid, ProfileRequest request);
        Task<BaseResponse> UpdateProfile(int id, ProfileRequest request);
        Task<BaseResponse<int>> GetProfileDetailsByUserId(int id);
    }
}
