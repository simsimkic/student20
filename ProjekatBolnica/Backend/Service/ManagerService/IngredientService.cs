/***********************************************************************
 * Module:  IngredientService.cs
 * Purpose: Definition of the Class Service.ManagerService.IngredientService
 ***********************************************************************/

using Model.ManagerModel;
using Repository.ManagerRepository;
using System.Collections.Generic;

namespace Service.ManagerService
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _repository;

        public IngredientService(IIngredientRepository repository)
        {
            _repository = repository;
        }

        public void Delete(Ingredient ingredient) => _repository.Delete(ingredient);

        public Ingredient Get(long id) => _repository.Get(id);

        public IEnumerable<Ingredient> GetAll() => _repository.GetAll();

        public void New(Ingredient ingredient) => _repository.New(ingredient);

        public void Set(Ingredient ingredient) => _repository.Set(ingredient);
    }
}