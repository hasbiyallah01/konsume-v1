using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using konsume_v1.Core.Domain.Entities;

namespace konsume_v1.Core.Application.Interfaces.Services
{
    public interface IUserInteractionService
    {
        Task<UserInteraction> SaveUserInteractionAsync(int id, string question, string response);
        Task<List<UserInteraction>> GetUserInteractionsAsync(int id);
        Task<List<UserInteraction>> GetUserInteractionsAsyn(int id);
    }
}