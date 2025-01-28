using RecipeBook.Models.DTO;

namespace RecipeBook.Models.Responses
{
    public class IngredientFullDetailsResponse
    {
        public string Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Calories { get; set; }
    }
}