using konsume_v1.Core.Domain.Entities;

namespace konsume_v1.Core.Application.Interfaces.Repositories
{
    public interface ITimetableRepository
    {
        Task<ICollection<MealPlans>> GenerateMealPlanAsync(MealPlans mealPlan);
        Task<ICollection<MealPlans>> GetMealPlanByProfileIdAsync(int profileId);
        Task <bool> UpdateMealPlan(MealPlans mealplan);
        Task <bool> UpdateMealPlans(MealPlans mealplan);
        Task<MealPlans> GetMealPlansByIdAsync(int mealId);
        Task<MealPlan> GetMealPlanByIdAsync(int mealId);
    }

}
