using RecipeBook.Models.Responses;

namespace RecipeBook.BL.Interfaces
{
    public interface IBusinessService
    {
        List<RecipeFullDetailsResponse> GetAllRecipes();
    }
}
