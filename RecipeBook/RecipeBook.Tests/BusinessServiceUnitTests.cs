using Moq;
using RecipeBook.BL.Services;
using RecipeBook.DL.Interfaces;
using RecipeBook.Models.DTO;

namespace RecipeBook.Tests
{
    public class BusinessServiceUnitTests
    {
        private readonly Mock<IRecipeRepository> 
            _recipeRepositoryMock;
        private readonly Mock<IIngredientRepository> 
            _ingredientRepositoryMock;

        private List<Ingredients> ingredients = new List<Ingredients>
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

        public BusinessServiceUnitTests()
        {
            _recipeRepositoryMock = new Mock<IRecipeRepository>();
            _ingredientRepositoryMock = new Mock<IIngredientRepository>();
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
                _ingredientRepositoryMock.Object);

            //act
            var result = 
                businessService.GetAllRecipes();

            //assert
            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.Count);
        }
    }
}