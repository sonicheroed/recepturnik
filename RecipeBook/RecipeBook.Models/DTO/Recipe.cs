namespace RecipeBook.Models.DTO
{
    public class Recipe
    {
        public string Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; }

        public List<string> Ingredients { get; set; }
    }
}
