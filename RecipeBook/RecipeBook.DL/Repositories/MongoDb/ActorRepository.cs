using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeBook.DL.Interfaces;
using RecipeBook.Models.DTO;

namespace RecipeBook.DL.Repositories.MongoDb
{
    internal class ActorRepository : IActorRepository
    {
        public List<Actor> GetAll()
        {
            throw new NotImplementedException();
        }

        public Actor? GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
