using DataLayer;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly DataService _dataService;

        public CategoriesController()
        {
            _dataService = new DataService();
        }

        // GET /api/categories
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _dataService.GetCategories();
            var categoryDTOs = categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            });
            return Ok(categoryDTOs);
        }

        // GET /api/categories/{id}
        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _dataService.GetCategory(id);
            if (category == null)
                return NotFound();

            var categoryDTO = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
            return Ok(categoryDTO);
        }

        // POST /api/categories
        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = _dataService.CreateCategory(categoryDTO.Name, categoryDTO.Description);

            categoryDTO.Id = category.Id;

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, categoryDTO);
        }

        // PUT /api/categories/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = _dataService.UpdateCategory(id, categoryDTO.Name, categoryDTO.Description);

            if (!updated)
                return NotFound();

            return Ok();
        }

        // DELETE /api/categories/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var deleted = _dataService.DeleteCategory(id);

            if (!deleted)
                return NotFound();

            return Ok();
        }
    }
}
