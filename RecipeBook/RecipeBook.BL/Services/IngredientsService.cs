using RecipeBook.BL.Interfaces;
using RecipeBook.DL.Interfaces;
using RecipeBook.Models.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace RecipeBook.BL.Services
{
    internal class IngredientsService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly ILogger<IngredientsService> _logger;

        public IngredientsService(
            IIngredientRepository ingredientRepository,
            ILogger<IngredientsService> logger)
        {
            _ingredientRepository = ingredientRepository;
            _logger = logger;
        }

        public List<Ingredients> GetAll()
        {
            try
            {
                return _ingredientRepository.GetAll();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to retrieve ingredients");
                return new List<Ingredients>();
            }
        }

        public Ingredients? GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _logger.LogError("Ingredient ID cannot be null or empty.");
                return null;
            }
            try
            {
                return _ingredientRepository.GetById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to retrieve ingredient");
                return null;
            }
        }

        public void Add(Ingredients ingredient)
        {
            if (ingredient == null)
            {
                _logger.LogError("Ingredient is null");
                return;
            }
            try
            {
                ingredient.Id = Guid.NewGuid().ToString();
                _ingredientRepository.Add(ingredient);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to add ingredient");
            }
        }

        public void Delete(string ingredientId)
        {
            if (string.IsNullOrEmpty(ingredientId))
            {
                _logger.LogError("Ingredient ID cannot be null or empty.");
                return;
            }
            try
            {
                _ingredientRepository.Delete(ingredientId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to delete ingredient");
            }
        }

        public void Update(Ingredients ingredient)
        {
            if (ingredient == null)
            {
                _logger.LogError("Ingredient is null");
                return;
            }
            try
            {
                _ingredientRepository.Update(ingredient);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update ingredient");
            }
        }
    }
}