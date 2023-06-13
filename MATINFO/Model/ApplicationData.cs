using MATINFO.Model;
using System.Collections.ObjectModel;
using Test_WPF_SQL.Model;

namespace Test_WPF_SQL.Model
{
    public class ApplicationData
    {
        public ObservableCollection<CategorieMateriel> CategoriesMateriel { get; set; }
        public ObservableCollection<Materiel> lesMateriaux { get; set; }
        public ObservableCollection<Personnel> lesPersonnels { get; set; }
        public ApplicationData()
        {
            CategorieMateriel e = new CategorieMateriel();
            Materiel materiel = new Materiel();
            Personnel personnel = new Personnel();
            CategoriesMateriel = e.FindAll();
            lesMateriaux = materiel.FindAll();
            lesPersonnels = personnel.FindAll();
        }
    }
}