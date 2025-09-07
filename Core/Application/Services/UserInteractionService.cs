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

        
        public Task<List<UserInteraction>> GetUserInteractionsAsyn(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserInteraction>> GetUserInteractionsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}