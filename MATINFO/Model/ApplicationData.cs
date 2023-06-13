using MATINFO.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MATINFO.Model
{
    public class ApplicationData
    {
        public ObservableCollection<CategorieMateriel> LesCategoriesMateriel { get; set; }
        public ObservableCollection<Materiel> LesMateriaux { get; set; }
        public ObservableCollection<Personnel> LesPersonnels { get; set; }
        public ApplicationData()
        {
            CategorieMateriel categorieMateriel = new CategorieMateriel();
            Personnel personnel = new Personnel();
            Materiel materiel = new Materiel();

            LesCategoriesMateriel = categorieMateriel.FindAll();
            LesMateriaux = materiel.FindAll();
            LesPersonnels = personnel.FindAll();

            foreach ( Materiel leMateriel in LesMateriaux.ToList())
            {
                leMateriel.UneCategorieM = LesCategoriesMateriel.ToList().Find(g => g.IdCategorie == leMateriel.FK_idCategorie);

            }

            foreach ( CategorieMateriel uneCategorie in LesCategoriesMateriel.ToList())
            {
                uneCategorie.LesMateriauxCM = new ObservableCollection<Materiel>(
                    LesMateriaux.ToList().FindAll(e => e.FK_idCategorie == uneCategorie.IdCategorie));
            }


        }
    }
}