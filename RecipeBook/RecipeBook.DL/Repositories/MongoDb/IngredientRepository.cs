using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeBook.DL.Interfaces;
using RecipeBook.Models.DTO;

namespace RecipeBook.DL.Repositories.MongoDb
{
    internal class IngredientRepository : IIngredientRepository
    {
        public List<Ingredients> GetAll()
        {
            throw new NotImplementedException();
        }

        public Ingredients? GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
