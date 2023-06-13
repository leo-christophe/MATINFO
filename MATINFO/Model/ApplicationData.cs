using MATINFO.Model;
using System.Collections.ObjectModel;

namespace MATINFO.Model
{
    public class ApplicationData
    {
        public ObservableCollection<CategorieMateriel> lesCategoriesMateriel { get; set; }
        public ObservableCollection<Materiel> lesMateriaux { get; set; }
        public ApplicationData()
        {
            CategorieMateriel categorieMateriel = new CategorieMateriel();
            Materiel materiel = new Materiel();

            lesCategoriesMateriel = categorieMateriel.FindAll();
            lesMateriaux = materiel.FindAll();
        }
    }
}