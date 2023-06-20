using MATINFO.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

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
            Attributions attributions = new Attributions();

            LesCategoriesMateriel = categorieMateriel.FindAll();
            LesMateriaux = materiel.FindAll();
            LesPersonnels = personnel.FindAll();
            LesAttributions = attributions.FindAll();

            // POUR MATERIEL / CATEGORIE
            //pour chaque matériel, on affecte une catégorie matériel.
            foreach ( Materiel leMateriel in LesMateriaux.ToList())
            {
                leMateriel.UneCategorieM = LesCategoriesMateriel.ToList().Find(g => g.IdCategorie == leMateriel.FK_idCategorie);
                Console.WriteLine(leMateriel.UneCategorieM.ToString());
            }

            //pour chaques catégorie matériel on affecte les matériaux associés.
            foreach ( CategorieMateriel uneCategorie in LesCategoriesMateriel.ToList())
            {
                uneCategorie.LesMateriauxCM = new ObservableCollection<Materiel>(
                    LesMateriaux.ToList().FindAll(e => e.FK_idCategorie == uneCategorie.IdCategorie));
            }




            // POUR LES ATTRIBUTIONS
            // pour chaque attributions, on affecte son personnel
            foreach (Attributions uneAttribution in LesAttributions.ToList())
            {
                uneAttribution.APersonnel = LesPersonnels.ToList().Find(g => g.IdPersonnel == uneAttribution.FK_idPersonnel);
            }
            // pour chaque personnel, on affecte sont attributions
            foreach (Personnel unPersonnel in LesPersonnels.ToList())
            {
                unPersonnel.SesAttributions = new ObservableCollection<Attributions>(
                    LesAttributions.ToList().FindAll(e => e.FK_idPersonnel == unPersonnel.IdPersonnel));
            }
            // pour chaque attributions, on affecte son matériel
            foreach (Attributions uneAttribution in LesAttributions.ToList())
            {
                uneAttribution.AMateriel = LesMateriaux.ToList().Find(g => g.IdMateriel == uneAttribution.FK_idMateriel);
            }
            // pour chaque matériel, on affecte ses attributions
            foreach (Materiel unMateriel in LesMateriaux.ToList())
            {
                unMateriel.SesAttributions = new ObservableCollection<Attributions>(
                    LesAttributions.ToList().FindAll(e => e.FK_idMateriel == unMateriel.IdMateriel));
            }
        }
    }
}