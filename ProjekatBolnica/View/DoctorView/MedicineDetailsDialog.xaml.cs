using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using Model.ManagerModel;
using ProjekatBolnica.Backend.Model.DoctorModel;
using ProjekatBolnica.DoctorView;

namespace ProjekatBolnica.View.DoctorView
{
    /// <summary>
    /// Interaction logic for MedicineDetailsDialog.xaml
    /// </summary>
    public partial class MedicineDetailsDialog : Window
    {
        public Medicine SelectedMedicine { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        public MedicineDetailsDialog(Medicine medicine)
        {
            InitializeComponent();
            if ((Application.Current.MainWindow as MainWindow).Theme == Theme.Dark) { dialogGrid.Background = Brushes.LightGray; }
            this.DataContext = this;
            SelectedMedicine = medicine;
            Ingredients = new ObservableCollection<Ingredient>(SelectedMedicine.Ingredient);
        }

        private void closeMedicineDetails(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
