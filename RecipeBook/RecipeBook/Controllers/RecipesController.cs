using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.BL.Interfaces;
using RecipeBook.Models.DTO;
using RecipeBook.Models.Requests;

namespace RecipeBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        private readonly IMapper _mapper;

        public RecipesController(
            IRecipeService recipeService,
            IMapper mapper)
        {
            _recipeService = recipeService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _recipeService.GetAll();
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
                return BadRequest($"Wrong ID:{id}");
            }
            var result = _recipeService.GetById(id);
            if (result == null)
            {
                return NotFound($"Recipe with ID:{id} not found");
            }
            return Ok(result);
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] AddRecipeRequest recipe)
        {
            var recipeDto = _mapper.Map<Recipe>(recipe);
            _recipeService.Add(recipeDto);
            return Ok();
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] Recipe recipe)
        {
            if (recipe == null)
            {
                return BadRequest("Recipe cannot be null.");
            }
            if (string.IsNullOrEmpty(recipe.Id))
            {
                return BadRequest("Recipe ID cannot be null or empty.");
            }
            try
            {
                _recipeService.Update(recipe);
                return StatusCode(200, "Recipe Updated.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Recipe with ID {recipe.Id} not found.");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(string recipeId)
        {
            if (string.IsNullOrEmpty(recipeId))
            {
                return BadRequest("Recipe ID cannot be null or empty.");
            }
            try
            {
                _recipeService.Delete(recipeId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Recipe with ID {recipeId} not found.");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost("AddIngredients")]
        public IActionResult AddIngredientsToRecipe(string recipeId, [FromBody] string ingredient)
        {
            if (string.IsNullOrEmpty(recipeId))
            {
                return BadRequest("Recipe ID cannot be null or empty.");
            }
            if (string.IsNullOrEmpty(ingredient))
            {
                return BadRequest("Ingredient cannot be null or empty.");
            }
            try
            {
                _recipeService.AddIngredientsToRecipe(recipeId, ingredient);
                return StatusCode(200, "Ingredient added to recipe.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Recipe with ID {recipeId} not found.");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("DeleteIngredient")]
        public IActionResult DeleteIngredientFromRecipe(string recipeId, string ingredientId)
        {
            if (string.IsNullOrEmpty(recipeId))
            {
                return BadRequest("Recipe ID cannot be null or empty.");
            }
            if (string.IsNullOrEmpty(ingredientId))
            {
                return BadRequest("Ingredient ID cannot be null or empty.");
            }
            try
            {
                _recipeService.DeleteIngredientFromRecipe(recipeId, ingredientId);
                return StatusCode(200, "Ingredient removed from recipe.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Recipe with ID {recipeId} or Ingredient with ID {ingredientId} not found.");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
