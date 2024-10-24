using DataLayer;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly DataService _dataService;

        public ProductsController()
        {
            _dataService = new DataService();
        }

        // GET /api/products/{id}
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _dataService.GetProduct(id);
            if (product == null)
                return NotFound();

            var productDTO = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                UnitPrice = (decimal)product.UnitPrice,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitsInStock = product.UnitsInStock,
                CategoryName = product.Category.Name
            };
            return Ok(productDTO);
        }

        // GET /api/products/category/{id}
        [HttpGet("category/{id}")]
        public IActionResult GetProductsByCategory(int id)
        {
            // Check if the category exists
            var category = _dataService.GetCategory(id);
            if (category == null)
            {
                // Return 404 with an empty array
                return NotFound(new List<ProductDTO>());
            }

            var products = _dataService.GetProductByCategory(id);

            var productDTOs = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                UnitPrice = (decimal)p.UnitPrice,
                QuantityPerUnit = p.QuantityPerUnit,
                UnitsInStock = p.UnitsInStock,
                CategoryName = category.Name
            }).ToList();

            // If there are no products, return 200 OK with an empty array
            return Ok(productDTOs);
        }


        // GET /api/products?name=<substring>
        [HttpGet]
        public IActionResult GetProductsByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name query parameter is required.");

            var products = _dataService.GetProductByName(name);

            if (products == null || !products.Any())
                return NotFound(new List<DataLayer.ProductNameCategoryDTO>());

            return Ok(products);
        }

    }
}
