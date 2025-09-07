using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using konsume_v1.Core.Application.Interfaces.Services;
using konsume_v1.Core.Domain.Entities;
using konsume_v1.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace konsume_v1.Core.Application.Services
{
    public class UserInteractionService : IUserInteractionService
    {
        private readonly KonsumeContext _context;

        public UserInteractionService(KonsumeContext context)
        {
            _context = context;
        }

        public async Task<UserInteraction> SaveUserInteractionAsync(int userId, string question, string response)
        {
            var interaction = new UserInteraction
            {
                UserId = userId,
                Question = question,
                Response = response,
                CreatedBy = userId.ToString(),
                DateCreated = DateTime.UtcNow,
                IsDeleted = false,
            };

            _context.UserInteractions.Add(interaction);
            await _context.SaveChangesAsync();
            return interaction;
        }

        
        public async Task<List<UserInteraction>> GetUserInteractionsAsync(int id)
        {
            return await _context.UserInteractions
                .Where(x => x.UserId == id)
                .OrderByDescending(x => x.DateCreated)
                .ToListAsync();
        }

        public async Task<List<UserInteraction>> GetUserInteractionsAsyn(int id)
        {
            return await _context.UserInteractions
                .Where(x => x.UserId == id)
                .Include(ui => ui.User)
                    .ThenInclude(u => u.Profile)
                .OrderByDescending(x => x.DateCreated)
                .Select(ui => new UserInteraction
                {
                    Id = ui.Id,
                    UserId = ui.UserId,
                    DateCreated = ui.DateCreated,
                    User = new User
                    {
                        Id = ui.User.Id,
                        FirstName = ui.User.FirstName,
                        LastName = ui.User.LastName,
                        Email = ui.User.Email,
                        Profile = ui.User.Profile
                    }
                })
                .ToListAsync();
        }
    }
}