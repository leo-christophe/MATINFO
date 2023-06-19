﻿using MATINFO.Model;
using System.Windows;


namespace MATINFO
{
    /// <summary>
    /// Logique d'interaction pour creer.xaml
    /// </summary>
    public partial class creerMateriel : Window
    {
        private Materiel nouveauMateriel;

        public creerMateriel()
        {
            InitializeComponent();

        }
        public Materiel NouveauMateriel
        {
            get
            {
                return this.nouveauMateriel;
            }

            set
            {
                this.nouveauMateriel = value;
            }
        }

        private void Button_ClickCreationOK(object sender, RoutedEventArgs e)
        {
            CategorieMateriel categorie = (CategorieMateriel)cbCategorieChoixMat.SelectionBoxItem;

            this.NouveauMateriel = new Materiel(categorie.IdCategorie,
                tbNomMaterielCreation.Text,
                tbReferenceConstructeurCreation.Text,
                tbCodeBarreCreation.Text, categorie);

            DialogResult = true;
        }

        private void Button_ClickCreationAnnuler(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
