using Mapster;
using RecipeBook.Models.DTO;
using RecipeBook.Models.Requests;

namespace RecipeBook.MapConfig
{
    public class MapsterConfiguration
    {
        public static void Configure()
        {
            TypeAdapterConfig<AddRecipeRequest, Recipe>
                .NewConfig();
        }
    }
}
