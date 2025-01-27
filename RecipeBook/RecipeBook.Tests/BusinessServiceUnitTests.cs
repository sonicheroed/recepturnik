using Moq;
using RecipeBook.BL.Services;
using RecipeBook.DL.Interfaces;
using RecipeBook.Models.DTO;
using Microsoft.Extensions.Logging;
using RecipeBook.BL.Interfaces;

namespace RecipeBook.Tests
{
    public class BusinessServiceUnitTests
    {
        private readonly Mock<IRecipeRepository> 
            _recipeRepositoryMock;
        private readonly Mock<IIngredientRepository> 
            _ingredientRepositoryMock;
        private readonly Mock<ILogger<BusinessService>> _loggerMock;

        private List<Ingredients> ingredients = new List<Ingredients>
        {
            new Ingredients()
            {
                Id = "157af604-7a4b-4538-b6a9-fed41a41cf3a",
                Name = "Milk",
                Calories = 120
            },
            new Ingredients()
            {
                Id = "baac2b19-bbd2-468d-bd3b-5bd18aba98d7",
                Name = "Sugar",
                Calories = 150
            },
            new Ingredients()
            {
                Id = "5c93ba13-e803-49c1-b465-d471607e97b3",
                Name = "Egg",
                Calories = 100
            },
        };

        private List<Recipe> _recipes = new List<Recipe>()
        {
            new Recipe()
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Milk dessert",
                Description = "Milk dessert with rice",
                Ingredients = [
                    "157af604-7a4b-4538-b6a9-fed41a41cf3a",
                    "baac2b19-bbd2-468d-bd3b-5bd18aba98d7" 
                ]
            },
            new Recipe()
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Musaka",
                Description = "Musaka with potatoes",
                Ingredients = [
                    "157af604-7a4b-4538-b6a9-fed41a41cf3a",
                    "5c93ba13-e803-49c1-b465-d471607e97b3"
                ]
            }
        };

        public BusinessServiceUnitTests()
        {
            _recipeRepositoryMock = new Mock<IRecipeRepository>();
            _ingredientRepositoryMock = new Mock<IIngredientRepository>();
            _loggerMock = new Mock<ILogger<BusinessService>>();
        }

        [Fact]
        public void GetAllRecipes_Ok()
        {
            //setup
            var expectedCount = 2;

            _recipeRepositoryMock.Setup(x => 
                    x.GetAll())
                .Returns(_recipes);
            _ingredientRepositoryMock.Setup(x => 
                    x.GetById(It.IsAny<string>()))
                .Returns((string id) =>
                    ingredients.FirstOrDefault(x => x.Id == id));

            //inject
            var businessService = new BusinessService(
                _recipeRepositoryMock.Object,
                _loggerMock.Object,
                _ingredientRepositoryMock.Object);

            //act
            var result = 
                businessService.GetAllRecipes();

            //assert
            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.Count);
        }

        [Fact]
        public void AddRecipe_Should_Call_Repository_Add_Method()
        {
            // Arrange
            var recipe = new Recipe
            {
                Id = "1",
                Title = "Test Recipe",
                Description = "Test Description",
                Ingredients = new List<string> { "ingredient1", "ingredient2" }
            };

            var businessService = new BusinessService(
                _recipeRepositoryMock.Object,
                _loggerMock.Object,
                _ingredientRepositoryMock.Object
            );

            // Act
            businessService.AddRecipe(recipe);

            // Assert
            _recipeRepositoryMock.Verify(r => r.Add(recipe), Times.Once);
        }
    }
}