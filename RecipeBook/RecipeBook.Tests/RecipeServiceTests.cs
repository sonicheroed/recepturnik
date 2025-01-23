using Castle.Core.Logging;
using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using Moq;
using RecipeBook.BL.Services;
using RecipeBook.DL.Interfaces;
using RecipeBook.Models.DTO;

namespace RecipeBook.Tests
{
    public class RecipeServiceTests
    {
        private readonly Mock<IRecipeRepository> _recipeRepositoryMock;
        private readonly Mock<IIngredientRepository> _ingredientRepositoryMock;

        private List<Ingredients> _ingredients = new List<Ingredients>
        {
            new Ingredients()
            {
                Id = "157af604-7a4b-4538-b6a9-fed41a41cf3a",
                Name = "Ingredient 1"
            },
            new Ingredients()
            {
                Id = "baac2b19-bbd2-468d-bd3b-5bd18aba98d7",
                Name = "Ingredient 2"
            },
            new Ingredients()
            {
                Id = "5c93ba13-e803-49c1-b465-d471607e97b3",
                Name = "Ingredient 3"
            },
        };

        private List<Recipe> _recipes = new List<Recipe>()
        {
            new Recipe()
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Recipe 1",
                Description = "Gotvi",
                Ingredients = [
                    "157af604-7a4b-4538-b6a9-fed41a41cf3a",
                    "baac2b19-bbd2-468d-bd3b-5bd18aba98d7"]
            },
            new Recipe()
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Recipe 2",
                Description = "Sgotvi",
                Ingredients = [
                    "157af604-7a4b-4538-b6a9-fed41a41cf3a",
                    "5c93ba13-e803-49c1-b465-d471607e97b3"
                ]
            }
        };

        public RecipeServiceTests()
        {
            _recipeRepositoryMock = new Mock<IRecipeRepository>();
            _ingredientRepositoryMock = new Mock<IIngredientRepository>();
        }

        [Fact]
        void GetById_Ok()
        {
            //Arrange
            var recipeId = _recipes[0].Id;
            var ingredientId = _ingredients[0].Id;

            _recipeRepositoryMock.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns((string id) => _recipes.FirstOrDefault(r => r.Id == id));

            _ingredientRepositoryMock.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns((string id) => _ingredients.FirstOrDefault(r => r.Id == id));

            var loggerMock = new Mock<ILogger<RecipesService>>();
            ILogger<RecipesService> logger = loggerMock.Object;

            //Act
            var recipeService = new RecipesService(
                _recipeRepositoryMock.Object,
                logger,
                _ingredientRepositoryMock.Object);

            var result = recipeService.GetById(recipeId);
            var result2 = recipeService.GetById(ingredientId);

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result2);
            Assert.Equal(recipeId, result.Id);
            Assert.Equal(ingredientId, result2.Id);
        }

        [Fact]
        void GetById_NotExistingId()
        {
            //Arrange
            var recipeId = Guid.NewGuid().ToString();
            var ingredientId = Guid.NewGuid().ToString();

            _recipeRepositoryMock.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns((string id) => _recipes.FirstOrDefault(r => r.Id == id));

            _ingredientRepositoryMock.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns((string id) => _ingredients.FirstOrDefault(r => r.Id == id));

            var loggerMock = new Mock<ILogger<RecipesService>>();
            ILogger<RecipesService> logger = loggerMock.Object;

            //Act
            var recipeService = new RecipesService(
                _recipeRepositoryMock.Object,
                logger,
                _ingredientRepositoryMock.Object);

            var result = recipeService.GetById(recipeId);
            var result2 = recipeService.GetById(ingredientId);

            //Assert
            Assert.Null(result);
            Assert.Null(result2);
        }

        [Fact]
        void GetById_WrongGuidId()
        {
            //Arrange
            var recipeId = "zsdfsd";
            var ingredientId = "asgsagtd";

            _recipeRepositoryMock.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns((string id) => _recipes.FirstOrDefault(r => r.Id == id));

            _ingredientRepositoryMock.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns((string id) => _ingredients.FirstOrDefault(r => r.Id == id));

            var loggerMock = new Mock<ILogger<RecipesService>>();
            ILogger<RecipesService> logger = loggerMock.Object;

            //Act
            var recipeService = new RecipesService(
                _recipeRepositoryMock.Object,
                logger,
                _ingredientRepositoryMock.Object);

            var result = recipeService.GetById(recipeId);
            var result2 = recipeService.GetById(ingredientId);

            //Assert
            Assert.Null(result);
            Assert.Null(result2);
        }
    }
}
