using Microsoft.Extensions.Logging;
using RecipeBook.BL.Interfaces;
using RecipeBook.DL.Interfaces;
using RecipeBook.Models.DTO;
using RecipeBook.Models.Responses;

namespace RecipeBook.BL.Services
{
    internal class BusinessService : IBusinessService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly ILogger<BusinessService> _logger;

        public BusinessService(
            IRecipeRepository recipeRepository,
            ILogger<BusinessService> logger,
            IIngredientRepository ingredientRepository)
        {
            _recipeRepository = recipeRepository;
            _logger = logger;
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

        public void AddRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null.");
            }
            try
            {    
                recipe.Id = Guid.NewGuid().ToString();
                _recipeRepository.Add(recipe);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update ingredient");
            }
        }
    }
}
