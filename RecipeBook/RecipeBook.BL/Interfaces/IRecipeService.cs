using RecipeBook.Models.DTO;

namespace RecipeBook.BL.Interfaces
{
    public interface IRecipeService
    {
        List<Recipe> GetAll();

        Recipe? GetById(string id);

        void Add(Recipe recipe);

        void Delete(string recipeId);

        void AddIngredientsToRecipe(string recipeId, string ingredient);

        void DeleteIngredientFromRecipe(string recipeId, string ingredientId);

        void Update(Recipe recipe);
    }
}
