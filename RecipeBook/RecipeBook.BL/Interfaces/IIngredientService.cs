using RecipeBook.Models.DTO;
using System.Collections.Generic;

namespace RecipeBook.BL.Interfaces
{
    public interface IIngredientService
    {
        List<Ingredients> GetAll();

        Ingredients? GetById(string id);

        void Add(Ingredients ingredient);

        void Delete(string ingredientId);

        void Update(Ingredients ingredient);
    }
}