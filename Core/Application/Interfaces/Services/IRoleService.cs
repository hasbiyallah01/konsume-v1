using konsume_v1.Models;
using konsume_v1.Models.RoleModel;

namespace konsume_v1.Core.Application.Interfaces.Services
{
    public interface IRoleService
    {
        Task<BaseResponse> CreateRole(RoleRequest request);
        
    }

}
