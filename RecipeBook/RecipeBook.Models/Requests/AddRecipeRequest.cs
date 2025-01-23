namespace RecipeBook.Models.Requests
{
    public class AddRecipeRequest
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; }
    }
}
