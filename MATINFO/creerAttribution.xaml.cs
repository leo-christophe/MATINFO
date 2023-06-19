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
        Attributions nouvelAttribu;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbCreerAttributionCommentaire.Text) && !string.IsNullOrEmpty(tbCreerAttributionDate.Text))
            {
                Personnel personnel = (Personnel)cbCreerAttributionIdPersonnel.SelectionBoxItem;
                Materiel materiel = (Materiel)cbCreerAttributionIdMateriel.SelectionBoxItem;
                nouvelAttribu = new Attributions(
                    DateTime.Parse(tbCreerAttributionDate.Text),
                    tbCreerAttributionCommentaire.Text,
                    materiel,
                    personnel);
                DialogResult = true;
            }
            else
                MessageBox.Show("Tous les champs doivent être renseignés.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
