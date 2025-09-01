using konsume_v1.Models.UserModel;

namespace konsume_v1.Core.Application.Interfaces.Services
{
    public interface IIdentityService
    {
        string GenerateToken(string key, string issuer, LoginResponse user);
        bool IsTokenValid(string key, string issuer, string token);
    }
}
