using _334_group_project_web_api.Helpers;
using _334_group_project_web_api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace _334_group_project_web_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShoppingListController : ControllerBase
    {
        private readonly ShoppingListService _shoppingListService;

        public ShoppingListController(ShoppingListService shoppingListService)
        {
            _shoppingListService = shoppingListService;
        }

        [HttpGet]
        public async Task<List<ShoppingList>> Get() =>
        await _shoppingListService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<ShoppingList>> Get(string id)
        {
            var shoppingList = await _shoppingListService.GetAsync(id);

            if (shoppingList is null)
            {
                return NotFound();
            }

            return shoppingList;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ShoppingList newShoppingList)
        {
            await _shoppingListService.CreateAsync(newShoppingList);

            return CreatedAtAction(nameof(Get), new { id = newShoppingList.Id }, newShoppingList);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, ShoppingList updateShoppingList)
        {
            var shoppingList = await _shoppingListService.GetAsync(id);

            if (shoppingList is null)
            {
                return NotFound();
            }

            updateShoppingList.Id = shoppingList.Id;

            await _shoppingListService.UpdateAsync(id, updateShoppingList);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var shoppingList = await _shoppingListService.GetAsync(id);

            if (shoppingList is null)
            {
                return NotFound();
            }

            await _shoppingListService.RemoveAsync(id);

            return NoContent();
        }


        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteItemFromList(string id, string listName, string itemId)
        {
            var shoppingList = await _shoppingListService.GetAsync(id);

            if (shoppingList is null)
            {
                return NotFound();
            }

            IList targetList = listName switch
            {
                "individualProducts" => shoppingList.IndividualProducts,
                "individualMeals" => shoppingList.IndividualMeals,
                "individualMealPlans" => shoppingList.IndividualMealPlans,
                _ => null
            };

            if (targetList == null)
            {
                return BadRequest("Invalid list name");
            }

            object itemToRemove = null;
            foreach (var item in targetList)
            {
                var itemIdProperty = item.GetType().GetProperty("Id");
                if (itemIdProperty != null && itemIdProperty.GetValue(item)?.ToString() == itemId)
                {
                    itemToRemove = item;
                    break;
                }
            }

            if (itemToRemove != null)
            {
                targetList.Remove(itemToRemove);
            }
            else
            {
                return NotFound("Item not found");
            }

            // Save the updated shopping list
            await _shoppingListService.UpdateAsync(id, shoppingList);

            return NoContent();
        }

        public class AddItemBody
        {
            public Product? Product { get; set; }
            public Meal? Meal { get; set; }
            public MealPlan? MealPlan { get; set; }
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> AddItemToList(string id, string listName, AddItemBody itemsToAdd)
        {
            var shoppingList = await _shoppingListService.GetAsync(id);

            if (shoppingList is null)
            {
                return NotFound();
            }

            IList targetList = listName switch
            {
                "individualProducts" => shoppingList.IndividualProducts,
                "individualMeals" => shoppingList.IndividualMeals,
                "individualMealPlans" => shoppingList.IndividualMealPlans,
                _ => null
            };

            if (targetList == null)
            {
                return BadRequest("Invalid list name");
            }

            if (itemsToAdd.Product != null)
            {
                targetList.Add(itemsToAdd.Product);
            } 
            else if (itemsToAdd.Meal != null)
            {
                targetList.Add(itemsToAdd.Meal);
            } 
            else if (itemsToAdd.MealPlan != null)
            {
                targetList.Add(itemsToAdd.MealPlan);
            }
            else
            {
                return NotFound("Item not found");
            }

            // Save the updated shopping list
            await _shoppingListService.UpdateAsync(id, shoppingList);

            return NoContent();
        }
    }
}
