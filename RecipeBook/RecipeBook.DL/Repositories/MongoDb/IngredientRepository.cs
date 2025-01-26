using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RecipeBook.DL.Interfaces;
using RecipeBook.Models.Configurations;
using RecipeBook.Models.DTO;

namespace RecipeBook.DL.Repositories.MongoDb
{
    internal class IngredientRepository : IIngredientRepository
    {
        private readonly IMongoCollection<Ingredients> _ingredientsCollection;
        private readonly ILogger<IngredientRepository> _logger;

        public IngredientRepository(IOptionsMonitor<MongoDbConfiguration> mongoConfig, ILogger<IngredientRepository> logger)
        {
            _logger = logger;
            var client =
                new MongoClient(mongoConfig.CurrentValue.ConnectionString);
            var database = client.GetDatabase(
                mongoConfig.CurrentValue.DatabaseName);
            _ingredientsCollection = database.GetCollection<Ingredients>("Ingredients");
        }

        public void Add(Ingredients? ingredient)
        {
            if (ingredient == null)
            {
                _logger.LogError("Ingredient is null");
                return;
            }
            try
            {
                _ingredientsCollection.InsertOne(ingredient);
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
                _logger.LogError("Ingredient ID is null or empty");
                return;
            }
            try
            {
                var result = _ingredientsCollection.DeleteOne(r => r.Id == ingredientId);
                if (result.DeletedCount == 0)
                {
                    _logger.LogWarning($"No ingredient found with ID: {ingredientId}");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to delete ingredient");
            }
        }

        public List<Ingredients> GetAll()
        {
            try
            {
                return _ingredientsCollection.Find(_ => true).ToList();
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
                _logger.LogError("Ingredient ID is null or empty");
                return null;
            }
            try
            {
                return _ingredientsCollection.Find(r => r.Id == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to retrieve ingredient");
                return null;
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
                var result = _ingredientsCollection.ReplaceOne(r => r.Id == ingredient.Id, ingredient);
                if (result.MatchedCount == 0)
                {
                    _logger.LogWarning($"No ingredient found with ID: {ingredient.Id}");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update ingredient");
            }
        }
    }
}
