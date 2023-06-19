using MATINFO.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MATINFO.Model
{
    public class ApplicationData
    {
        private ObservableCollection<CategorieMateriel> lesCategoriesMateriel;
        private ObservableCollection<Materiel> lesMateriaux;
        private ObservableCollection<Personnel> lesPersonnels;
        private ObservableCollection<Attributions> lesAttributions;
        public ObservableCollection<Attributions> LesAttributions
        {
            get
            {
                return lesAttributions;
            }

            set
            {
                lesAttributions = value;
            }
        }

        public ObservableCollection<Personnel> LesPersonnels
        {
            get
            {
                return lesPersonnels;
            }

            set
            {
                lesPersonnels = value;
            }
        }

        public ObservableCollection<Materiel> LesMateriaux
        {
            get
            {
                return lesMateriaux;
            }

            set
            {
                lesMateriaux = value;
            }
        }

        public ObservableCollection<CategorieMateriel> LesCategoriesMateriel
        {
            get
            {
                return lesCategoriesMateriel;
            }

            set
            {
                lesCategoriesMateriel = value;
            }
        }
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