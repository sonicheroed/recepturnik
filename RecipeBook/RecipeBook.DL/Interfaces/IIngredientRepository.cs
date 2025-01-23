using RecipeBook.Models.DTO;

namespace RecipeBook.DL.Interfaces
{
    public interface IIngredientRepository
    {
        List<Ingredients> GetAll();

        Ingredients? GetById(string id);
    }
}
