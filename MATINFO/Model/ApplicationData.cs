using MATINFO.Model;
using System.Collections.ObjectModel;

namespace MATINFO.Model
{
    public class ApplicationData
    {
        public ObservableCollection<CategorieMateriel> lesCategoriesMateriel { get; set; }
        public ObservableCollection<Materiel> lesMateriaux { get; set; }
        public ObservableCollection<Personnel> lesPersonnels { get; set; }
        public ApplicationData()
        {
            CategorieMateriel categorieMateriel = new CategorieMateriel();
            Materiel materiel = new Materiel();

            lesCategoriesMateriel = categorieMateriel.FindAll();
            Personnel personnel = new Personnel();
            lesMateriaux = materiel.FindAll();
            lesPersonnels = personnel.FindAll();
        }
    }
}