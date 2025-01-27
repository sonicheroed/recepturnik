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
    }
}
