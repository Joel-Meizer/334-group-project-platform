using _334_group_project_web_api.Helpers;
using _334_group_project_web_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace _334_group_project_web_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GrocerController : ControllerBase
    {
        private readonly GrocerService _grocerService;

        public GrocerController(GrocerService grocerService)
        {
            _grocerService = grocerService;
        }

        [HttpGet]
        public async Task<List<Grocer>> Get() =>
        await _grocerService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Grocer>> Get(string id)
        {
            var grocer = await _grocerService.GetAsync(id);

            if (grocer is null)
            {
                return NotFound();
            }

            return grocer;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Grocer newGrocer)
        {
            await _grocerService.CreateAsync(newGrocer);

            return CreatedAtAction(nameof(Get), new { id = newGrocer.Id }, newGrocer);
        }

        [HttpPost]
        public async Task<IActionResult> BulkPost(List<Grocer> newGrocerList)
        {
            foreach (Grocer newGrocer in newGrocerList)
            {
                await _grocerService.CreateAsync(newGrocer);
            }

            return Ok();
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Grocer updateGrocer)
        {
            var grocer = await _grocerService.GetAsync(id);

            if (grocer is null)
            {
                return NotFound();
            }

            updateGrocer.Id = grocer.Id;

            await _grocerService.UpdateAsync(id, updateGrocer);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var grocer = await _grocerService.GetAsync(id);

            if (grocer is null)
            {
                return NotFound();
            }

            await _grocerService.RemoveAsync(id);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> BulkAssignProductsToGrocers(List<String> productIds)
        {
            List<Grocer> grocers = await _grocerService.GetAsync();

            foreach (Grocer grocer in grocers)
            {
                Grocer updateGrocer = grocer;
                updateGrocer.ProductIds = productIds;
                await _grocerService.UpdateAsync(grocer.Id, updateGrocer);
            }

            return NoContent();
        }
    }
}
