using RecipeBook.Models.DTO;

namespace RecipeBook.DL.Interfaces
{
    public interface IRecipeRepository
    {
        List<Recipe> GetAll();

        Recipe? GetById(string id);

        void Add(Recipe recipe);

        void Update(Recipe recipe);
    }
}
