/***********************************************************************
 * Module:  IngredientController.cs
 * Purpose: Definition of the Class Controller.ManagerController.IngredientController
 ***********************************************************************/

using Controller.HospitalController;
using Model.ManagerModel;
using Service.ManagerService;
using System.Collections.Generic;

namespace Controller.ManagerController
{
    public class IngredientController : IController<Ingredient, long>
    {
        private readonly IIngredientService _service;

        public IngredientController(IIngredientService service)
        {
            _service = service;
        }

        public void Delete(Ingredient ingredient) => _service.Delete(ingredient);

        public Ingredient Get(long id) => _service.Get(id);

        public IEnumerable<Ingredient> GetAll() => _service.GetAll();

        public void New(Ingredient ingredient) => _service.New(ingredient);

        public void Set(Ingredient ingredient) => _service.Set(ingredient);
    }
}