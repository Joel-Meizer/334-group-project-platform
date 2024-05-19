using _334_group_project_web_api.Models;
using _334_group_project_web_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace _334_group_project_web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService _inventoryService;

        public InventoryController(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInventoryItems()
        {
            var inventoryItems = await _inventoryService.GetAllInventoryItems();
            return Ok(inventoryItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInventoryItem(string id)
        {
            var inventoryItem = await _inventoryService.GetInventoryItem(id);
            if (inventoryItem == null)
            {
                return NotFound();
            }
            return Ok(inventoryItem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInventoryItem([FromBody] Inventory inventory)
        {
            await _inventoryService.CreateInventoryItem(inventory);
            return CreatedAtAction(nameof(GetInventoryItem), new { id = inventory.Id }, inventory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventoryItem(string id, [FromBody] Inventory inventory)
        {
            var existingInventoryItem = await _inventoryService.GetInventoryItem(id);
            if (existingInventoryItem == null)
            {
                return NotFound();
            }

            await _inventoryService.UpdateInventoryItem(id, inventory);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryItem(string id)
        {
            var existingInventoryItem = await _inventoryService.GetInventoryItem(id);
            if (existingInventoryItem == null)
            {
                return NotFound();
            }

            await _inventoryService.DeleteInventoryItem(id);
            return NoContent();
        }
    }
}