using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using konsume_v1.Core.Application.Interfaces.Repositories;
using konsume_v1.Core.Domain.Entities;
using konsume_v1.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace konsume_v1.Infrastructure.Repositories
{
    public class StreakRepository : IStreakRepository
    {
        private readonly KonsumeContext _context;

        public StreakRepository(KonsumeContext context)
        {
            _context = context;
        }

        public async Task<Streak> GetStreakByProfileIdAsync(int profileId)
        {
            return await _context.Streaks
                .Where(s => s.ProfileId == profileId)
                .SingleOrDefaultAsync();
        }


        public async Task UpdateStreakAsync(Streak streak)
        {
            _context.Streaks.Update(streak);
            await _context.SaveChangesAsync();
        }

        public async Task CreateStreakAsync(Streak streak)
        {
            await _context.Streaks.AddAsync(streak);
            await _context.SaveChangesAsync();
        }
    }
}