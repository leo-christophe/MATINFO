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
    /// Logique d'interaction pour creerAttribution.xaml
    /// </summary>
    public partial class creerAttribution : Window
    {
        // Champ qui va nous permettre de communiquer avec la MainWindow
        private Attributions nouvelAttribu;
        public creerAttribution()
        {
            InitializeComponent();
        }

        public Attributions NouvelAttribu
        {
            get
            {
                return this.nouvelAttribu;
            }

            set
            {
                this.nouvelAttribu = value;
            }
        }

        /// <summary>
        /// Méthode déclenchée quand l'utilisateur décide de valider la création d'une attribution
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Valider(object sender, RoutedEventArgs e)
        {
            // si tous les champs sont remplis correctement
            if (!string.IsNullOrEmpty(tbCreerAttributionCommentaire.Text) && !string.IsNullOrEmpty(tbCreerAttributionDate.Text))
            {
                // On définit la nouvelle attribution
                Personnel personnel = (Personnel)cbCreerAttributionIdPersonnel.SelectionBoxItem;
                Materiel materiel = (Materiel)cbCreerAttributionIdMateriel.SelectionBoxItem;
                nouvelAttribu = new Attributions(
                    DateTime.Parse(tbCreerAttributionDate.Text),
                    tbCreerAttributionCommentaire.Text,
                    materiel,
                    personnel);
                //si l'attribution n'existe pas déjà
                if (NouvelAttribu.Read())
                {
                    DialogResult = true;
                }
                else
                    MessageBox.Show("Il existe déjà une attribution", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("Tous les champs doivent être renseignés.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            // On prévient l'utilisateur grace à un MessageBox
        }

        /// <summary>
        /// Méthode déclenchée quand l'utilisateur décide d'annuler la création d'une attribution
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Annuler(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
