using Microsoft.Extensions.DependencyInjection;
using RecipeBook.DL.Interfaces;
using RecipeBook.DL.Repositories;
using RecipeBook.DL.Repositories.MongoDb;

namespace RecipeBook.DL
{
    public static class DependenciesInjection
    {
        public static IServiceCollection 
            RegisterRepositories(this IServiceCollection services)
        {
            return
                services
                    .AddSingleton<IMovieRepository,
                        MoviesMongoRepository>()
                    .AddSingleton<IActorRepository,
                        ActorRepository>();
        }
    }
}
