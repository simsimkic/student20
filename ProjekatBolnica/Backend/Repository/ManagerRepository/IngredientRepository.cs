/***********************************************************************
 * Module:  IngredientRepository.cs
 * Purpose: Definition of the Class Repository.ManagerRepository.IngredientRepository
 ***********************************************************************/

using Model.ManagerModel;
using ProjekatBolnica.Backend.Repository.CSV;
using ProjekatBolnica.Backend.Repository.CSV.Stream;
using ProjekatBolnica.Backend.Repository.Sequencer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.ManagerRepository
{
    public class IngredientRepository : CSVRepository<Ingredient, long>,
        IIngredientRepository,
        IEagerCSVRepository<Ingredient, long>
    {
        private const String ENTITY_NAME = "Ingredient";
        private const String NOT_UNIQUE_ERROR = "Ingredient name {0} is not unique!";

        public IngredientRepository(ICSVStream<Ingredient> stream, ISequencer<long> sequencer)
            : base(ENTITY_NAME, stream, sequencer)
        {
        }

        public new IEnumerable<Ingredient> Find(Func<Ingredient, bool> predicate)
            => GetAllEager().Where(predicate);

        public new void New(Ingredient ingredient)
        {
            if (IsNameUnique(ingredient.Name))
                base.New(ingredient);
            else
                throw new NotUniqueException(String.Format(NOT_UNIQUE_ERROR, ingredient.Name));
        }

        public IEnumerable<Ingredient> GetAllEager() => GetAll();

        public Ingredient GetEager(long id) => Get(id);

        private bool IsNameUnique(String name)
          => GetIngredientByName(name) == null;

        private Ingredient GetIngredientByName(String name)
            => _stream.ReadAll().SingleOrDefault(ingredient => ingredient.Name.Equals(name));
    }
}