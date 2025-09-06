using Microsoft.AspNetCore.Mvc;
using konsume_v1.Core.Application.Interfaces.Services;
using konsume_v1.Core.Domain.Entities;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace konsume_v1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MealPlanController : ControllerBase
    {
        private readonly IMealPlanService _mealPlanService;

        public MealPlanController(IMealPlanService mealPlanService)
        {
            _mealPlanService = mealPlanService;
        }

        [HttpGet("GenMeal")]
        public async Task<IActionResult> GenerateMealPlan([FromQuery] int profileId)
        {
            if (profileId <= 0)
            {
                return BadRequest("Invalid user ID.");
            }

            var response = await _mealPlanService.Generate30DayMealPlanAsync(profileId);

            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(500, response.Message);
            }
        }

        public async Task<IActionResult> UpdateMealPlans([FromBody] MealPlans mealPlan, [FromRoute] int profileId)
        {
            var response = await _mealPlanService.UpdateMealPlans(mealPlan, profileId);

            if (response.IsSuccessful)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }
    }
}
