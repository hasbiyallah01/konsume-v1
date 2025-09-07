using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using konsume_v1.Core.Application.Services;
using konsume_v1.Models;

namespace konsume_v1.Core.Application.Interfaces.Services
{
    public interface IMealRecommendationService
    {
        Task SaveDailyRecommendationsAsync(int profileId, string dateSeed, string meals);
        //Task<string> GetDailyRecommendationsAsync(int profileId, string dateSeed);
        Task<BaseResponse<MealDetails>> GenerateMealDetailsAsync(string mealName, int profileId);
    }
}