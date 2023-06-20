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
    /// Logique d'interaction pour creer.xaml
    /// </summary>
    public partial class creerPersonnel : Window
    {
        // Nouveau personnel communiqué à la MainWindow
        private Personnel nouveauPersonnel;

        public creerPersonnel()
        {
            InitializeComponent();
        }

        public Personnel NouveauPersonnel
        {
            get
            {
                return nouveauPersonnel;
            }

            set
            {
                nouveauPersonnel = value;
            }
        }

        /// <summary>
        ///  Cette méthode se déclenche lorsque l'utilisateur décide de valider la création
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickCreationOK(object sender, RoutedEventArgs e)
        {
            //les champs doivent êtres remplit
            if (!string.IsNullOrEmpty(tbNomPersonnel.Text) && !string.IsNullOrEmpty(tbPrenomPersonnel.Text) && !string.IsNullOrEmpty(tbMailPersonnel.Text))
            {
                NouveauPersonnel = new Personnel(tbNomPersonnel.Text, tbPrenomPersonnel.Text, tbMailPersonnel.Text);
                //si le personnel n'existe pas déjà
                if (!NouveauPersonnel.Read())
                {
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Il existe déjà un personnel", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Tous les champs doivent être renseignés.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Cette méthode se déclenche lorsque l'utilisateur annule la demande de création
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickCreationAnnuler(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}