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
            var result =
                _recipeService.GetAllRecipes();

            if (result != null && result.Count > 0)
            {
                return Ok(result);
            }

            return NotFound();
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
