using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.BL.Interfaces;
using RecipeBook.Models.DTO;
using RecipeBook.Models.Requests;

namespace RecipeBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;
        private readonly IMapper _mapper;

        public IngredientsController(
            IIngredientService ingredientService,
            IMapper mapper)
        {
            _ingredientService = ingredientService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _ingredientService.GetAll();

            if (result != null && result.Count > 0)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpGet("GetById")]
        public IActionResult GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Ingredient ID cannot be null or empty.");
            }
            var ingredient = _ingredientService.GetById(id);
            if (ingredient == null)
            {
                return NotFound($"Ingredient with ID {id} not found.");
            }
            return Ok(ingredient);
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] AddIngredientRequest request)
        {
            if (request == null)
            {
                return BadRequest("Ingredient request cannot be null.");
            }
            var ingredient = _mapper.Map<Ingredients>(request);
            try
            {
                _ingredientService.Add(ingredient);
                return StatusCode(200, "Ingredient added successfully.");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] Ingredients ingredient)
        {
            if (ingredient == null)
            {
                return BadRequest("Ingredient cannot be null.");
            }
            try
            {
                _ingredientService.Update(ingredient);
                return StatusCode(200, "Ingredient updated successfully.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Ingredient with ID {ingredient.Id} not found.");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(string ingredientId)
        {
            if (string.IsNullOrEmpty(ingredientId))
            {
                return BadRequest("Ingredient ID cannot be null or empty.");
            }
            try
            {
                _ingredientService.Delete(ingredientId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Ingredient with ID {ingredientId} not found.");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}