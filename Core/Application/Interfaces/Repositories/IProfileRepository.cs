using konsume_v1.Core.Domain.Entities;

namespace konsume_v1.Core.Application.Interfaces.Repositories
{
    public partial interface IProfileRepository
    {
        Task<Profile> AddAsync(Profile Profile);
        Task<Profile> GetAsync(int id);
        Task<bool> GetProfileByUserIdAsync(int id);
        Task<int> GetProfileDetailsByUserIdAsync(int id);
        Task<Profile> GetAsync(string email);
    }

}
