using Microsoft.Extensions.DependencyInjection;
using RecipeBook.BL.Interfaces;
using RecipeBook.BL.Services;

namespace RecipeBook.BL
{
    public static class DependenciesInjection
    {
        public static IServiceCollection
            RegisterServices(this IServiceCollection services)
        {
            return services
                        .AddSingleton<IMoviesService, MoviesService>()
                        .AddSingleton<IBusinessService, BusinessService>();
        }
    }
}
