using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using konsume_v1.Core.Domain.Entities;

namespace konsume_v1.Core.Application.Interfaces.Repositories
{
    public interface IStreakRepository
    {
        Task CreateStreakAsync(Streak streak);
        Task<Streak> GetStreakByProfileIdAsync(int profileId);
        Task UpdateStreakAsync(Streak streak);
    }
}