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
        private readonly IBusinessService _movieService;

        public BusinessController(IBusinessService movieService)

        {
            _movieService = movieService;
        }

        [HttpGet("GetAllDetailedMovies")]
        public IActionResult GetAllDetailedMovies()
        {
            var result =
                _movieService.GetAllMovies();

            if (result != null && result.Count > 0)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpPost("Test")]
        public IActionResult Test([FromBody] TestRequest movie)
        {
            
            return Ok();
        }
    }

    public class TestRequest
    {
        public int MagicNumber { get; set; }

        public string Text { get; set; }

        public DateTime DateTime { get; set; }
    }
}
