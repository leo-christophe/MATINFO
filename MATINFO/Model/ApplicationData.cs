using MATINFO.Model;
using System.Collections.ObjectModel;
using Test_WPF_SQL.Model;

namespace Test_WPF_SQL.Model
{
    public class ApplicationData
    {
        public ObservableCollection<CategorieMateriel> CategoriesMateriel { get; set; }
        public ApplicationData()
        {
            CategorieMateriel e = new CategorieMateriel();
            CategoriesMateriel = e.FindAll();
        }
    }
}