using konsume_v1.Core.Domain.Entities;
using konsume_v1.Models;

namespace konsume_v1.Core.Application.Interfaces.Services
{
    public interface IMealPlanService
    {
        Task<BaseResponse<ICollection<MealPlanResponse>>> Generate30DayMealPlanAsync(int id);
        Task<BaseResponse<ICollection<MealPlanResponse>>> RetrieveMealPlanAsync(int profileId);
        Task<BaseResponse<ICollection<MealPlanResponse>>> UpdateMealPlans(MealPlans mealplan, int profileid);
    }
}


