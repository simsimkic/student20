using Model.ManagerModel;
using ProjekatBolnica.Backend.Repository.CSV;
using ProjekatBolnica.Backend.Repository.CSV.Stream;
using ProjekatBolnica.Backend.Repository.Sequencer;
using Repository.ManagerRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjekatBolnica.Backend.Repository.ManagerRepository
{
    public class UnapprovedMedicineRepository : CSVRepository<Medicine, long>,
        IMedicineRepository,
        IEagerCSVRepository<Medicine, long>
    {
        private const String ENTITY_NAME = "Unapproved medicine";
        private const String NOT_UNIQUE_ERROR = "Medicine name {0} is not unique!";
        private readonly IEagerCSVRepository<Ingredient, long> _ingredientRepository;

        public UnapprovedMedicineRepository(ICSVStream<Medicine> stream, ISequencer<long> sequencer,
            IEagerCSVRepository<Ingredient, long> ingredientRepository)
           : base(ENTITY_NAME, stream, sequencer)
        {
            _ingredientRepository = ingredientRepository;
        }

        public new IEnumerable<Medicine> Find(Func<Medicine, bool> predicate)
            => GetAllEager().Where(predicate);

        public new void New(Medicine medicine)
        {
            if (IsNameUnique(medicine.Name))
                base.New(medicine);
            else
                throw new NotUniqueException(String.Format(NOT_UNIQUE_ERROR, medicine.Name));
        }

        public IEnumerable<Medicine> GetAllEager()
        {
            var ingredients = _ingredientRepository.GetAllEager();
            var medicine = GetAll();
            BindIngredientsWithMedicine(ingredients, medicine);
            return medicine;
        }

        public Medicine GetEager(long id)
        {
            var medicine = Get(id);
            for (int i = 0; i < _ingredientRepository.GetAllEager().Count(); i++)
                medicine.Ingredient[i] = _ingredientRepository.GetEager(medicine.Ingredient[i].Id);
            return medicine;
        }

        public Medicine GetMedicineByName(String name)
            => _stream.ReadAll().SingleOrDefault(medicine => medicine.Name.Equals(name));

        private bool IsNameUnique(String name)
             => GetMedicineByName(name) == null;

        private void BindIngredientsWithMedicine(IEnumerable<Ingredient> ingredients, IEnumerable<Medicine> medicine)
        {
            List<Ingredient> found = new List<Ingredient>();
            for (int i = 0; i < ingredients.Count(); i++)
                medicine.ToList().ForEach(med => med.Ingredient = FindIngredientById(ingredients, med.Ingredient[i].Id, found));
        }

        private List<Ingredient> FindIngredientById(IEnumerable<Ingredient> ingredients, long id, List<Ingredient> found)
        {
            found.Add(ingredients.SingleOrDefault(ing => ing.Id == id));
            return found;
        }
    }
}