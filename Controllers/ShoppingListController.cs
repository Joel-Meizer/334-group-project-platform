using _334_group_project_web_api.Helpers;
using _334_group_project_web_api.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
