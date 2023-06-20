using MATINFO.Model;
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
using System.Windows.Shapes;

namespace MATINFO
{
    /// <summary>
    /// Logique d'interaction pour creerCategorie.xaml
    /// </summary>
    public partial class creerCategorie : Window
    {
        // Nouvelle catégorie qui va être communiquée à la MainWindow
        private CategorieMateriel nouvelleCategorie;

        public CategorieMateriel NouvelleCategorie
        {
            get
            {
                return nouvelleCategorie;
            }

            set
            {
                nouvelleCategorie = value;
            }
        }

        public creerCategorie()
        {
            InitializeComponent();
        }
        
        /// <summary>
        ///  Méthode déclenchée lorsque l'utilisateur décide de valider la création d'une catégorie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickCreationOK(object sender, RoutedEventArgs e)
        {
            // Si le texte est bien renseigné, on créer la nouvelle catégorie
            if (!String.IsNullOrEmpty(tbNomCategorieCreation.Text))
            {
                NouvelleCategorie = new CategorieMateriel(tbNomCategorieCreation.Text);
                DialogResult = true;
            }
            else
                MessageBox.Show("Le nom de matériel doit apparaître !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error) ;

        }

        /// <summary>
        /// Méthode déclenchée lorsque l'utilisateur décide d'annuler la création d'une catégorie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickCreationAnnuler(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

        }
    }
}
