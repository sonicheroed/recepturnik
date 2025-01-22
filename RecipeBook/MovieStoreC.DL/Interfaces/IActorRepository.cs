using RecipeBook.Models.DTO;

namespace RecipeBook.DL.Interfaces
{
    public interface IActorRepository
    {
        List<Actor> GetAll();

        Actor? GetById(string id);
    }
}
