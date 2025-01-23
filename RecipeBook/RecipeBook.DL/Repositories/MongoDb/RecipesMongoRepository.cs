using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RecipeBook.DL.Interfaces;
using RecipeBook.Models.Configurations;
using RecipeBook.Models.DTO;

namespace RecipeBook.DL.Repositories.MongoDb
{
    internal class RecipesMongoRepository : IRecipeRepository
    {
        private readonly IMongoCollection<Recipe> _recipesCollection;
        private readonly ILogger<RecipesMongoRepository> _logger;

        public RecipesMongoRepository(
            IOptionsMonitor<MongoDbConfiguration> mongoConfig,
            ILogger<RecipesMongoRepository> logger)
        {
            _logger = logger;

            var client = 
                new MongoClient(mongoConfig.CurrentValue.ConnectionString);
            var database = client.GetDatabase(
                mongoConfig.CurrentValue.DatabaseName);
            _recipesCollection = database.GetCollection<Recipe>("RecipesDb");
        }

        public List<Recipe> GetAll()
        {
            return _recipesCollection.Find(r => true)
                .ToList();
        }

        public Recipe? GetById(string id)
        {
            return _recipesCollection
                .Find(r => r.Id == id)
                .FirstOrDefault();
        }

        public void Add(Recipe? recipe)
        {
            if (recipe == null)
            {
                _logger.LogError("Recipe is null");
                return;
            }

            
            try
            {
                _recipesCollection.InsertOne(recipe);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to add recipe");
            }
        }

        public void Update(Recipe recipe)
        {
            _recipesCollection.ReplaceOne(r => r.Id == recipe.Id, recipe);
        }
    }
}
