using RecipeBook.Models.DTO;

namespace RecipeBook.Models.Responses
{
    public class RecipeFullDetailsResponse
    {
        public string Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; }

        public List<Ingredients> Ingredients { get; set; } = new List<Ingredients>();
    }
}
