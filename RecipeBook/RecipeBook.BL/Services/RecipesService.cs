using Microsoft.Extensions.Logging;
using RecipeBook.BL.Interfaces;
using RecipeBook.DL.Interfaces;
using RecipeBook.Models.DTO;
using System.Runtime.CompilerServices;

namespace RecipeBook.BL.Services
{
    internal class RecipesService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly ILogger<RecipesService> _logger;

        public RecipesService(
            IRecipeRepository recipeRepository,
            ILogger<RecipesService> logger,
            IIngredientRepository ingredientRepository)
        {
            _recipeRepository = recipeRepository;
            _logger = logger;
            _ingredientRepository = ingredientRepository;
        }

        public void Add(Recipe recipe)
        {
            if (recipe == null)
            {
                _logger.LogError("Recipe is null");
                return;
            }

            recipe.Id = Guid.NewGuid().ToString();

            _recipeRepository.Add(recipe);

        }

        public void Delete(string recipeId)
        {
            var recipe = _recipeRepository.GetById(recipeId);

            if (recipe == null)
            {
                _logger.LogError("Recipe is null");
                return;
            }
            _recipeRepository.Delete(recipeId);
        }

        public void AddIngredientsToRecipe(string recipeId, string ingredientId)
        {
            if (string.IsNullOrEmpty(recipeId) || string.IsNullOrEmpty(ingredientId))
            {
                _logger.LogError("RecipeId or Ingredient is null");
                return;
            }

            if (Guid.TryParse(recipeId, out _) || Guid.TryParse(ingredientId, out _))
            {
                _logger.LogError("RecipeId or Ingredient is not valid");
                return;
            }

            var recipe = _recipeRepository.GetById(recipeId);

            if (recipe == null)
            {
                _logger.LogError("Recipe not found");
                return;
            }

            var ingredient = _ingredientRepository.GetById(ingredientId);

            if (ingredient == null)
            {
                _logger.LogError("Ingredient not found");
                return;
            }

            if (recipe.Ingredients == null)
            {
                recipe.Ingredients = new List<string>();
            }

            recipe.Ingredients.Add(ingredientId);

            _recipeRepository.Update(recipe);
        }

        public void DeleteIngredientFromRecipe(string recipeId, string ingredientId)
        {
            if (string.IsNullOrEmpty(recipeId) || string.IsNullOrEmpty(ingredientId))
            {
                _logger.LogError("RecipeId or IngredientId is null");
                return;
            }

            if (!Guid.TryParse(recipeId, out _) || !Guid.TryParse(ingredientId, out _))
            {
                _logger.LogError("RecipeId or IngredientId is not valid");
                return;
            }

            var recipe = _recipeRepository.GetById(recipeId);

            if (recipe == null)
            {
                _logger.LogError("Recipe not found");
                return;
            }

            if (recipe.Ingredients == null || !recipe.Ingredients.Contains(ingredientId))
            {
                _logger.LogError("Ingredient not found in the recipe");
                return;
            }

            recipe.Ingredients.Remove(ingredientId);

            _recipeRepository.Update(recipe);
        }

        public List<Recipe> GetAll()
        {
            return _recipeRepository.GetAll();
        }

        public Recipe? GetById(string id)
        {
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out _)) return null;

            return _recipeRepository.GetById(id);
        }
    }
}
