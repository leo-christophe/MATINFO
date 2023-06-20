using MATINFO.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour modifCategorie.xaml
    /// </summary>
    public partial class modifCategorie : Window
    {
        // Catégorie à modifier, communiquant avec la MainWindow
        private CategorieMateriel catAmodifier;

        public modifCategorie()
        {
            InitializeComponent();
        }

        public CategorieMateriel CatAmodifier
        {
            get
            {
                return catAmodifier;
            }

            set
            {
                catAmodifier = value;
            }
        }


        /// <summary>
        /// Méthode déclenchée lorsque l'utilisateur confirme qu'il veut modifier une catégorie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickCreationOK(object sender, RoutedEventArgs e)
        {
            if (cbCategorieChoix.SelectedItem != "" && tbNouveauNom.Text != "")
            {
                CategorieMateriel categorieTemporaire = new CategorieMateriel();
                // On recherche l'ID à modifier
                ObservableCollection<CategorieMateriel> catMateriel = categorieTemporaire.FindBySelection($"WHERE idCategorie = {((CategorieMateriel)cbCategorieChoix.SelectionBoxItem).IdCategorie}");
                CatAmodifier = new CategorieMateriel(catMateriel[0].IdCategorie, tbNouveauNom.Text);
                DialogResult = true;
            }
            else
                MessageBox.Show("Veuillez renseigner tous les champs!", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Méthode déclenchée lorsque l'utilisateur confirme qu'il veut annuler la modification d'une catégorie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickCreationAnnuler(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
