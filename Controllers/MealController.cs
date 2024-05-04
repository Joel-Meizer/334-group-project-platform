using _334_group_project_web_api.Helpers;
using _334_group_project_web_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace _334_group_project_web_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly MealService _mealService;

        public MealController(MealService mealService)
        {
            _mealService = mealService;
        }

        [HttpGet]
        public async Task<List<Meal>> Get() =>
        await _mealService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Meal>> Get(string id)
        {
            var meal = await _mealService.GetAsync(id);

            if (meal is null)
            {
                return NotFound();
            }

            return meal;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Meal newMeal)
        {
            await _mealService.CreateAsync(newMeal);

            return CreatedAtAction(nameof(Get), new { id = newMeal.Id }, newMeal);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Meal updateMeal)
        {
            var meal = await _mealService.GetAsync(id);

            if (meal is null)
            {
                return NotFound();
            }

            updateMeal.Id = meal.Id;

            await _mealService.UpdateAsync(id, updateMeal);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var meal = await _mealService.GetAsync(id);

            if (meal is null)
            {
                return NotFound();
            }

            await _mealService.RemoveAsync(id);

            return NoContent();
        }
    }
}
