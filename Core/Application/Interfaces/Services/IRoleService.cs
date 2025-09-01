using konsume_v1.Models;
using konsume_v1.Models.RoleModel;

namespace konsume_v1.Core.Application.Interfaces.Services
{
    public interface IRoleService
    {
        Task<BaseResponse> CreateRole(RoleRequest request);
        Task<BaseResponse> UpdateRole(int id, RoleRequest request);
        Task<BaseResponse> RemoveRole(int id);
        Task<BaseResponse<RoleResponse>> GetRole(int id);
        Task<BaseResponse<ICollection<RoleResponse>>> GetAllRole();

    }

}
