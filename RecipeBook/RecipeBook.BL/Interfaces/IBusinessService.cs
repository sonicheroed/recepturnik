using RecipeBook.DL.Interfaces;
using RecipeBook.Models.DTO;
using RecipeBook.Models.Responses;

namespace RecipeBook.BL.Interfaces
{
    public interface IBusinessService
    {
        List<RecipeFullDetailsResponse> GetAllRecipes();

        public void AddRecipe(Recipe recipe);
    }
}
