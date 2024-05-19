using _334_group_project_web_api.Helpers;
using _334_group_project_web_api.Models;
using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Mvc;

namespace _334_group_project_web_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<List<Product>> Get() =>
        await _productService.GetAsync();


        [HttpGet]
        public async Task<List<String>> GetAllIDs()
        {
            List<Product> productList = await _productService.GetAsync();
            List<String> productIDs = new List<String>();
            foreach (Product product in productList)
            {
                productIDs.Add(product.Id);
            }
            return productIDs;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var product = await _productService.GetAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Product newProduct)
        {
            await _productService.CreateAsync(newProduct);

            return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
        }

        [HttpPost]
        public async Task<IActionResult> BulkPost([FromBody] List<Product> newProductList)
        {
            foreach (Product newProduct in newProductList)
            {
                try
                {
                    await _productService.CreateAsync(newProduct);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(newProduct);
                    Console.WriteLine(ex.ToString());
                }
            }

            return Ok();
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Product updateProduct)
        {
            var product = await _productService.GetAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            updateProduct.Id = product.Id;

            await _productService.UpdateAsync(id, updateProduct);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _productService.GetAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            await _productService.RemoveAsync(id);

            return NoContent();
        }
    }
}