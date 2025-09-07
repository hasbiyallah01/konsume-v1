using konsume_v1.Core.Application.Services;
using konsume_v1.Models;

namespace konsume_v1.Core.Application.Interfaces.Services
{
    public interface IMealRecommendationService
    {
        Task<string> GetDailyRecommendationsAsync(int profileId, string dateSeed);
        Task SaveDailyRecommendationsAsync(int profileId, string dateSeed, string meals);
        Task<BaseResponse<MealDetails>> GenerateMealDetailsAsync(string mealName, int profileId);
    }
}