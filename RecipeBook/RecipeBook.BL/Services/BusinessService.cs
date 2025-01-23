using RecipeBook.BL.Interfaces;
using RecipeBook.DL.Interfaces;
using RecipeBook.Models.Responses;

namespace RecipeBook.BL.Services
{
    internal class BusinessService : IBusinessService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IIngredientRepository _ingredientRepository;

        public BusinessService(
            IRecipeRepository recipeRepository,
            IIngredientRepository ingredientRepository)
        {
            _recipeRepository = recipeRepository;
            _ingredientRepository = ingredientRepository;
        }

        public List<RecipeFullDetailsResponse> GetAllRecipes()
        {
            var result = new List<RecipeFullDetailsResponse>();

            var recipes = _recipeRepository.GetAll();

            foreach (var recipe in recipes)
            {
                var detailedRecipe = new RecipeFullDetailsResponse()
                {
                    Id = recipe.Id,
                    Title = recipe.Title,
                    Description = recipe.Description
                };

                foreach (var ingredientId in recipe.Ingredients)
                {
                    var ingredient = _ingredientRepository.GetById(ingredientId);
                    if (ingredient == null) continue;
                    detailedRecipe.Ingredients.Add(ingredient);
                }

                result.Add(detailedRecipe);
            }

            return result;
        }
    }
}
