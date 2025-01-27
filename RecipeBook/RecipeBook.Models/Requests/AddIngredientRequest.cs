namespace RecipeBook.Models.Requests
{
    public class AddIngredientRequest
    {
        public string Name { get; set; } = string.Empty;

        public int Calories { get; set; }
    }
}
