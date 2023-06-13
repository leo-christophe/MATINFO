using System;
using System.Collections.Generic;
using System.Linq;
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

        confirmation fenetreConfirmation;
        public MainWindow()
        {
            InitializeComponent();

            DataAccess accesBD = new DataAccess();
            bool res = accesBD.OpenConnection();
            MessageBox.Show("Résultat de la connexion : " + res);

            /*
            CategoriesMateriel = new List<CategorieMateriel> { };
            Personnels = new List<Personnel> { };
            Materiels = new List<Materiel> { };
            Attributions = new List<Attribution> { };
            */
            creationWinPers = new creerPersonnel();
            creationWinMat = new creerMateriel();
            creationWinCat = new creerCategorie();
            creationWinAtt = new creerAttribution();

            modificationPers = new modifPersonnel();
            modificationMat = new modifMateriel();
            modificationAtt = new modifAttributions();
            modificationCat = new modifCategorie();

            fenetreConfirmation = new confirmation();

            modificationPers.Hide();
            modificationMat.Hide();
            modificationCat.Hide();
            modificationAtt.Hide();

            creationWinPers.Hide();
            creationWinCat.Hide();
            creationWinMat.Hide();
            creationWinAtt.Hide();

            fenetreConfirmation.Hide();
        }

        private void ButtonClickCreate(object sender, RoutedEventArgs e)
        {
            Button bouton = (Button)sender;
            switch (bouton.Name)
            {
                case "btCreerPers":
                    {
                        creationWinPers.Show();
                        break;
                    }
                case "btCreerMat":
                    {
                        creationWinMat.Show();
                        break;
                    }
                case "btCreerAtt":
                    {
                        creationWinAtt.Show();
                        break;
                    }
                case "btCreerCat":
                    {
                        creationWinCat.Show();
                        break;
                    }
            }
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
            fenetreConfirmation.Show();
        }
    }
}
