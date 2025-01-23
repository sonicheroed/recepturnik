using RecipeBook.Models.DTO;

namespace RecipeBook.BL.Interfaces
{
    public interface IMoviesService
    {
        List<Movie> GetAll();

        Movie? GetById(string id);

        void Add(Movie movie);

        void AddActorToMovie(string movieId, string actor);
    }
}
