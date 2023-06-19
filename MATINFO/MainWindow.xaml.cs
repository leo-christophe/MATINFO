using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MATINFO;
using MATINFO.Model;

namespace MATINFO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        creerPersonnel creationWinPers;
        creerCategorie creationWinCat;
        creerAttribution creationWinAtt;

        modifPersonnel modificationPers;
        modifAttributions modificationAtt;
        modifCategorie modificationCat;

        DataAccess accesBD;
        bool res;
        public MainWindow()
        {
            InitializeComponent();

            accesBD = new DataAccess();
            res = accesBD.OpenConnection();
        }

        private void ButtonClickCreate(object sender, RoutedEventArgs e)
        {
            Button bouton = (Button)sender;
            switch (bouton.Name)
            {
                case "btCreerPers":
                    {
                        creationWinPers = new creerPersonnel();
                        creationWinPers.ShowDialog();
                        if ((bool)creationWinPers.DialogResult == true)
                        {

                            applicationdata.LesPersonnels.Add(creationWinPers.NouveauPersonnel);

                            creationWinPers.NouveauPersonnel.Create();

                            listViewPersonnel.Items.Refresh();
                        }
                        break;
                    }
                case "btCreerMat":
                    {

                        creerMateriel creationWinMat = new creerMateriel();
                        creationWinMat.Owner = this;
                        creationWinMat.DataContext = applicationdata;




                        bool? confirm = creationWinMat.ShowDialog();

                        if (confirm == true)
                        {
                            
                            applicationdata.LesMateriaux.Add(creationWinMat.NouveauMateriel);
                            
                            
                            creationWinMat.NouveauMateriel.Create();
                            
                            listViewMateriel.Items.Refresh();
                            listViewCategories.Items.Refresh();
                        }

                        break;
                    }
                case "btCreerAtt":
                    {
                        creerAttribution creationWinAtt = new creerAttribution();
                        
                        creationWinAtt.ShowDialog();
                        if ((bool)creationWinAtt.DialogResult == true)
                        {

                            applicationdata.LesAttributions.Add(creationWinAtt.NouvelAttribu);

                            creationWinAtt.NouvelAttribu.Create();

                            listViewAttributions.Items.Refresh();
                        }
                        break;
                    }
                case "btCreerCat":
                    {
                        creationWinCat = new creerCategorie();
                        creationWinCat.ShowDialog();
                        if ((bool)creationWinCat.DialogResult == true)
                        {
                            applicationdata.LesCategoriesMateriel.Add(creationWinCat.NouvelleCategorie);
                            creationWinCat.NouvelleCategorie.Create();

                            listViewCategories.Items.Refresh();
                            listViewMateriel.Items.Refresh();
                        }
                        break;
                    }
            }
        }
        private void creerCategorieAuto(CategorieMateriel Cat)
        {
            applicationdata.LesCategoriesMateriel.Add(Cat);
            Cat.Create();
            listViewCategories.Items.Refresh();
        }

        private void ButtonClickModification(object sender, RoutedEventArgs e)
        {
            Button bouton = (Button)sender;
            switch (bouton.Name)
            {
                case "btModifierPers":
                    {
                        modificationPers = new modifPersonnel();
                        modificationPers.DataContext = this.applicationdata;

                        //on rentre les données dans la fenêtre de modification par défault
                        modificationPers.cbNomPersonnelModification.SelectedIndex = listViewPersonnel.SelectedIndex;
                        modificationPers.tbPrenomPersonnelModification.Text = ((Personnel)listViewPersonnel.SelectedItem).PrenomPersonnel;
                        modificationPers.tbMailPersonnelModification.Text = ((Personnel)listViewPersonnel.SelectedItem).EmailPersonnel;

                        modificationPers.ShowDialog();
                        if ((bool)modificationPers.DialogResult == true)
                        {
                            List<Personnel> lePersonnel = new List<Personnel>(applicationdata.LesPersonnels);
                            int index = lePersonnel.FindIndex(x => x.IdPersonnel == modificationPers.NouveauPersonnel.IdPersonnel);
                            applicationdata.LesPersonnels[index] = new Personnel(index, modificationPers.NouveauPersonnel.NomPersonnel, modificationPers.NouveauPersonnel.PrenomPersonnel, modificationPers.NouveauPersonnel.EmailPersonnel);
                            
                            // Mettre à jour avec la bd
                            applicationdata.LesPersonnels[index].Update();
                            listViewPersonnel.Items.Refresh();
                        }
                        break;
                    }
                case "btModifierMat":
                    {
                        modifMateriel modificationMat = new modifMateriel();
                        modificationMat.Owner = this;
                        modificationMat.DataContext = this.applicationdata;

                        modificationMat.ShowDialog();
                        if ((bool)modificationMat.DialogResult)
                        {
                            List<Materiel> ListMaterials = new List<Materiel>(applicationdata.LesMateriaux);
                            List<CategorieMateriel> ListCategories = new List<CategorieMateriel>(applicationdata.LesCategoriesMateriel);

                            int index = ListMaterials.FindIndex(x => x.IdMateriel == modificationMat.NouveauMateriel.IdMateriel);

                            applicationdata.LesMateriaux[index].NomMateriel = modificationMat.NouveauMateriel.NomMateriel;
                            applicationdata.LesMateriaux[index].FK_idCategorie = modificationMat.NouveauMateriel.FK_idCategorie;
                            applicationdata.LesMateriaux[index].ReferenceConstructeur = modificationMat.NouveauMateriel.ReferenceConstructeur;
                            applicationdata.LesMateriaux[index].CodeBarreInventaire = modificationMat.NouveauMateriel.CodeBarreInventaire;
                            applicationdata.LesMateriaux[index].UneCategorieM = ListCategories.Find(x => x.IdCategorie == modificationMat.NouveauMateriel.FK_idCategorie);

                            // Mettre à jour avec la bd
                            applicationdata.LesMateriaux[index].Update();
                            listViewMateriel.Items.Refresh();
                        }
                        break;
                    }
                case "btModifierAtt":
                    {
                        modificationAtt.Show();
                        break;
                    }
                case "btModifierCat":
                    {
                        modificationCat = new modifCategorie();
                        modificationCat.ShowDialog();
                        if ((bool)modificationCat.DialogResult)
                        {
                            List<CategorieMateriel> ListCategories = new List<CategorieMateriel>(applicationdata.LesCategoriesMateriel);

                            int index = ListCategories.FindIndex(x => x.IdCategorie == modificationCat.CatAmodifier.IdCategorie);

                            applicationdata.LesCategoriesMateriel[index].NomCategorie = modificationCat.CatAmodifier.NomCategorie;

                            // Mettre à jour avec la bd
                            applicationdata.LesCategoriesMateriel[index].Update();
                            listViewCategories.Items.Refresh();
                        }
                        break;
                    }
            }
        }

        private void ButtonClickSuppression(object sender, RoutedEventArgs e)
        {
            confirmation fenetreConfirmation = new confirmation();
            fenetreConfirmation.ShowDialog();

            if ((bool)fenetreConfirmation.DialogResult)
            {

                switch (((Button)(sender)).Name)
                {
                    case "btSupprimerMat":
                        {
                            applicationdata.LesMateriaux[listViewMateriel.SelectedIndex].Delete();
                            applicationdata.LesMateriaux.RemoveAt(listViewMateriel.SelectedIndex);
                            listViewMateriel.Items.Refresh();
                            break;
                        }

                    case "btSupprimerCat":
                        {
                            int selectedIndex = listViewCategories.SelectedIndex;
                            CategorieMateriel selectedItem = (CategorieMateriel)listViewCategories.SelectedItem;

                            // suppression de la catégorie
                            applicationdata.LesCategoriesMateriel[selectedIndex].Delete();
                            applicationdata.LesCategoriesMateriel.RemoveAt(listViewCategories.SelectedIndex);
                            listViewCategories.Items.Refresh(); 

                            // Suppression des materiaux correspondant à la catégorie
                            // On créé une list temporaire pour pouvoir supprimer.
                            List<Materiel> TemporaryList = new List<Materiel>(applicationdata.LesMateriaux);
                            foreach (Materiel materiel in TemporaryList)
                            {
                                if (materiel.FK_idCategorie == selectedItem.IdCategorie)
                                {
                                    applicationdata.LesMateriaux.Remove(materiel);
                                    materiel.Delete();
                                }
                            }
                            listViewMateriel.Items.Refresh();
                            break;
                        }                

                    case "btSupprimerPers":
                        {
                            applicationdata.LesPersonnels[listViewPersonnel.SelectedIndex].Delete();
                            applicationdata.LesPersonnels.RemoveAt(listViewPersonnel.SelectedIndex);
                            listViewPersonnel.Items.Refresh();
                            break;
                        }
                    case "btSupprimerAtt":
                        {
                            applicationdata.LesAttributions[listViewAttributions.SelectedIndex].Delete();
                            applicationdata.LesAttributions.RemoveAt(listViewAttributions.SelectedIndex);
                            listViewAttributions.Items.Refresh();
                            break;
                        }
                }
            }
        }
    }
}
