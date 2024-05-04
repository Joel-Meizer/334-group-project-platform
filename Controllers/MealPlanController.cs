using _334_group_project_web_api.Helpers;
using _334_group_project_web_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace _334_group_project_web_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MealPlanController : ControllerBase
    {
        private readonly MealPlanService _mealPlanService;

        public MealPlanController(MealPlanService mealPlanService)
        {
            _mealPlanService = mealPlanService;
        }

        [HttpGet]
        public async Task<List<MealPlan>> Get() =>
        await _mealPlanService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<MealPlan>> Get(string id)
        {
            var mealPlan = await _mealPlanService.GetAsync(id);

            if (mealPlan is null)
            {
                return NotFound();
            }

            return mealPlan;
        }

        [HttpPost]
        public async Task<IActionResult> Post(MealPlan newMealPlan)
        {
            await _mealPlanService.CreateAsync(newMealPlan);

            return CreatedAtAction(nameof(Get), new { id = newMealPlan.Id }, newMealPlan);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, MealPlan updateMealPlan)
        {
            var mealPlan = await _mealPlanService.GetAsync(id);

            if (mealPlan is null)
            {
                return NotFound();
            }

            updateMealPlan.Id = mealPlan.Id;

            await _mealPlanService.UpdateAsync(id, updateMealPlan);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var mealPlan = await _mealPlanService.GetAsync(id);

            if (mealPlan is null)
            {
                return NotFound();
            }

            await _mealPlanService.RemoveAsync(id);

            return NoContent();
        }
    }
}
