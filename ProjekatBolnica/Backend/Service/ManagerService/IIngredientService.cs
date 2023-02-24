/***********************************************************************
 * Module:  IIngredientService.cs
 * Purpose: Definition of the Interface Service.ManagerService.IIngredientService
 ***********************************************************************/

using Model.ManagerModel;
using Service.HospitalServices;

namespace Service.ManagerService
{
    public interface IIngredientService : IService<Ingredient, long>
    {
    }
}