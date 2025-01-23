using RecipeBook.Models.DTO;

namespace RecipeBook.BL.Interfaces
{
    public interface IRecipeService
    {
        List<Recipe> GetAll();

        Recipe? GetById(string id);

        void Add(Recipe recipe);

        void AddIngredientsToRecipe(string recipeId, string ingredient);
    }
}
