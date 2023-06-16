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
        /*
        public List<CategorieMateriel> CategoriesMateriel;
        public List<Personnel> Personnels;
        public List<Materiel> Materiels;
        public List<Attribution> Attributions;*/
        creerPersonnel creationWinPers;
        creerMateriel creationWinMat;
        creerCategorie creationWinCat;
        creerAttribution creationWinAtt;

        modifPersonnel modificationPers;
        modifMateriel modificationMat;
        modifAttributions modificationAtt;
        modifCategorie modificationCat;

        DataAccess accesBD;
        bool res;
        public MainWindow()
        {
            InitializeComponent();

            accesBD = new DataAccess();
            res = accesBD.OpenConnection();

            creationWinPers = new creerPersonnel();
            creationWinMat = new creerMateriel();
            
            creationWinAtt = new creerAttribution();

            modificationPers = new modifPersonnel();
            modificationMat = new modifMateriel();
            modificationAtt = new modifAttributions();
            modificationCat = new modifCategorie();


            modificationPers.Hide();
            modificationMat.Hide();
            modificationCat.Hide();
            modificationAtt.Hide();

            creationWinPers.Hide();

            creationWinMat.Hide();
            creationWinAtt.Hide();



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
                        creationWinMat = new creerMateriel();
                        creationWinMat.ShowDialog();
                        if (creationWinMat.NouveauMateriel != null)
                        {
                            applicationdata.LesMateriaux.Add(creationWinMat.NouveauMateriel);
                            creationWinMat.NouveauMateriel.Create();
                            listViewMateriel.Items.Refresh();
                        }
                        break;
                    }
                case "btCreerAtt":
                    {
                        creationWinAtt.Show();
                        break;
                    }
                case "btCreerCat":
                    {
                        creationWinCat = new creerCategorie();
                        creationWinCat.Owner = this;
                        creationWinCat.ShowDialog();
                        if ( (bool)creationWinCat.DialogResult == true )
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
                        modificationPers.Show();
                        break;
                    }
                case "btModifierMat":
                    {
                        modificationMat.Show();
                        break;
                    }
                case "btModifierAtt":
                    {
                        modificationAtt.Show();
                        break;
                    }
                case "btModifierCat":
                    {
                        modificationCat.Show();
                        break;
                    }
            }
        }

        private void ButtonClickSuppression(object sender, RoutedEventArgs e)
        {
            confirmation fenetreConfirm = new confirmation();
            fenetreConfirm.ShowDialog();

            if ( fenetreConfirm.Supprimer )
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
                            int indexselected = listViewCategories.SelectedIndex;
                            int idchoisi = applicationdata.LesCategoriesMateriel[indexselected].IdCategorie;
                            applicationdata.LesCategoriesMateriel[indexselected].Delete();
                            applicationdata.LesCategoriesMateriel.RemoveAt(listViewCategories.SelectedIndex);
                            listViewCategories.Items.Refresh();

                            int cpt = applicationdata.LesMateriaux.Count;

                            // Supprimer les materiaux correspondant à la catégorie
                            for (int i = 0; i < cpt; i++)
                            {
                                Console.WriteLine(idchoisi);
                                if (applicationdata.LesMateriaux[i].FK_idCategorie == idchoisi)
                                {

                                    applicationdata.LesMateriaux[i].Delete();

                                    applicationdata.LesMateriaux.RemoveAt(i);

                                    listViewMateriel.Items.Refresh();

                                }
                            }
                            break;
                        }

                    case "btSupprimerPers":
                        {
                            applicationdata.LesPersonnels[listViewPersonnel.SelectedIndex].Delete();
                            applicationdata.LesPersonnels.RemoveAt(listViewPersonnel.SelectedIndex);
                            listViewPersonnel.Items.Refresh();
                            break;
                        }
                }
                fenetreConfirm.Supprimer = false;
            }
        }

        private void creerPersonnelConf(Personnel p)
        {
            applicationdata.LesPersonnels.Add(p);
            p.Create();
        }
        public int PrendreDernierIdPersonnel()
        {
            return applicationdata.LesPersonnels[-1].IdPersonnel;
        }
    }
}
