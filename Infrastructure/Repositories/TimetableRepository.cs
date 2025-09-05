using konsume_v1.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using konsume_v1.Core.Application.Interfaces.Repositories;
using konsume_v1.Core.Domain.Entities;

public class TimetableRepository : ITimetableRepository
{
    private readonly KonsumeContext _context;

    public TimetableRepository(KonsumeContext context)
    {
        _context = context;
    }


    public async Task<ICollection<MealPlans>> GenerateMealPlanAsync(MealPlans mealPlan)
    {
        if (_context == null) throw new ObjectDisposedException(nameof(KonsumeContext));

        await _context.Set<MealPlans>().AddAsync(mealPlan);
        await _context.SaveChangesAsync();

        return new List<MealPlans> { mealPlan }; 
    }

    public async Task<ICollection<MealPlans>> GetMealPlanByProfileIdAsync(int profileId)
    {
        return await _context.MealPlans
        .Include(mp => mp.MealPlan)
            .ThenInclude(mp => mp.NutritionalInfo) 
        .Where(mp => mp.MealPlan.ProfileId == profileId)
        .ToListAsync();
    }


    public async Task<MealPlans> GetMealPlansByIdAsync(int mealId)
    {
        return await _context.MealPlans
            .Include(mp => mp.MealPlan)
                .ThenInclude(mp => mp.NutritionalInfo) 
            .FirstOrDefaultAsync(mp => mp.MealPlan.Id == mealId);
    }
    public async Task<MealPlan> GetMealPlanByIdAsync(int mealId)
    {
        return await _context.MealPlans
            .Where(mp => mp.MealPlan.Id == mealId)
            .Select(mp => mp.MealPlan)
            .Include(mp => mp.NutritionalInfo) 
            .FirstOrDefaultAsync();
    }


    public async Task<bool> UpdateMealPlan(MealPlans mealplan)
    {
        _context.MealPlans.Update(mealplan);
        var result =  await _context.SaveChangesAsync();
        return result > 0;
    }
     public  async Task<bool> UpdateMealPlans(MealPlans mealplan)
     {
         _context.MealPlans.Update(mealplan);
        var result = await _context.SaveChangesAsync();
        return result > 0;
     }
}

