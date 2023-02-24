/***********************************************************************
 * Module:  IIngredientRepository.cs
 * Purpose: Definition of the Interface Repository.ManagerRepository.IIngredientRepository
 ***********************************************************************/

using Model.ManagerModel;
using Repository.HospitalRepository;

namespace Repository.ManagerRepository
{
    public interface IIngredientRepository : IRepository<Ingredient, long>
    {
    }
}