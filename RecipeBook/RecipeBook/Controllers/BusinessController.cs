using Microsoft.AspNetCore.Mvc;
using RecipeBook.BL.Interfaces;
using RecipeBook.Models.DTO;
using RecipeBook.Models.Requests;

namespace RecipeBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessService _recipeService;

        public BusinessController(IBusinessService recipeService)

        {
            _recipeService = recipeService;
        }

        [HttpGet("GetAllDetailedRecipes")]
        public IActionResult GetAllDetailedRecipes()
        {
            var result = _recipeService.GetAllRecipes();
            if (result != null && result.Count > 0)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpPost("AddRecipe")]
        public IActionResult AddRecipe([FromBody] Recipe recipe)
        {
            if (recipe == null)
            {
                return BadRequest("Recipe cannot be null.");
            }
            try
            {
                _recipeService.AddRecipe(recipe);
                return CreatedAtAction(nameof(GetAllDetailedRecipes), new { id = recipe.Id }, recipe);
            }
            catch (Exception)
            {
                return BadRequest("Error");
            }
        }


        [HttpPost("Test")]
        public IActionResult Test([FromBody] TestRequest recipe)
        {
            return Ok();
        }
    }

    public class TestRequest
    {
        public int MagicNumber { get; set; }

        public string Text { get; set; }
    }
}
