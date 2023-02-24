/***********************************************************************
 * Module:  MedicineService.cs
 * Purpose: Definition of the Class Service.ManagerService.MedicineService
 ***********************************************************************/

using Model.ManagerModel;
using ProjekatBolnica.Backend.Model.UserModel;
using Repository.ManagerRepository;
using Service.UserService;
using System.Collections.Generic;
using System.Linq;

namespace Service.ManagerService
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly IMedicineRepository _unapprovedMedicineRepository;
        private readonly IIngredientService _ingredientService;
        private readonly IUserService _userService;

        public MedicineService(IMedicineRepository medicineRepository, IMedicineRepository unapprovedMedicineRepository,
            IIngredientService ingredientService, IUserService userService)
        {
            _medicineRepository = medicineRepository;
            _unapprovedMedicineRepository = unapprovedMedicineRepository;
            _ingredientService = ingredientService;
            _userService = userService;
        }

        public void Delete(Medicine medicine)
        {
            for (int i = 0; i < _ingredientService.GetAll().Count(); i++)
                _ingredientService.Delete(medicine.Ingredient[i]);
            _medicineRepository.Delete(medicine);
            DeleteUnapproved(medicine);
        }

        public void DeleteUnapproved(Medicine medicine) => _unapprovedMedicineRepository.Delete(medicine);

        public Medicine Get(long id)
        {
            var medicine = _medicineRepository.Get(id);
            for (int i = 0; i < _ingredientService.GetAll().Count(); i++)
                medicine.Ingredient[i] = _ingredientService.Get(medicine.Ingredient[i].Id);
            return medicine;
        }

        public IEnumerable<Medicine> GetAll()
        {
            var ingredients = _ingredientService.GetAll();
            var medicine = _medicineRepository.GetAll();
            return medicine;
        }

        public IEnumerable<Medicine> GetAllUnapproved()
        {
            var ingredients = _ingredientService.GetAll();
            var medicine = _unapprovedMedicineRepository.GetAll();
            return medicine;
        }

        public void New(Medicine medicine)
        {
            List<Ingredient> ingr = new List<Ingredient>();
            ingr = medicine.Ingredient;
            SetMissingValues(medicine);

            for (int i = 0; i <= _ingredientService.GetAll().Count(); i++)
                _ingredientService.Set(ingr[i]);

            _medicineRepository.New(medicine);
            _unapprovedMedicineRepository.New(medicine);
            medicine.Ingredient = ingr;
        }

        public void Set(Medicine medicine)
        {
            for (int i = 0; i < _ingredientService.GetAll().Count(); i++)
                _ingredientService.Set(medicine.Ingredient[i]);
            _medicineRepository.Set(medicine);
            _unapprovedMedicineRepository.Set(medicine);
        }

        public void SendApprovalRequest(Feedback message) => _userService.AddFeedback(message);

        private void SetMissingValues(Medicine medicine) => medicine.Amount = 0;
    }
}