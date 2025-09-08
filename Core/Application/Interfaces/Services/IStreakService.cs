using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using konsume_v1.Core.Domain.Entities;
using konsume_v1.Models;

namespace konsume_v1.Core.Application.Interfaces.Services
{
    public interface IStreakService
    {
        Task<Streak> UpdateReadingStreakAsync(int profileId);
        Task<int> GetStreakCountByProfileIdAsync(int profileId);
    }
}