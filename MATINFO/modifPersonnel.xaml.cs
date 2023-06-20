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
    /// Logique d'interaction pour modifPersonnel.xaml
    /// </summary>
    public partial class modifPersonnel : Window
    {
        Personnel nouveauPersonnel;
        public modifPersonnel()
        {
            InitializeComponent();
        }

        private void Button_ClickModifOK(object sender, RoutedEventArgs e)
        {
            string nomPersonnel = tbNouveauNomPersonnelModifier.Text;
            string prenomPersonnel = tbPrenomPersonnelModification.Text;
            string mailNouveau = tbMailPersonnelModification.Text;

            Personnel personnel = (Personnel)cbNomPersonnelModification.SelectionBoxItem;
            this.nouveauPersonnel = new Personnel(personnel.IdPersonnel, nomPersonnel, prenomPersonnel, mailNouveau);

            DialogResult = true;
        }

        private void Button_ClickCreationOK(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        public Personnel NouveauPersonnel
        {
            get
            {
                return this.nouveauPersonnel;
            }

            set
            {
                this.nouveauPersonnel = value;
            }
        }

        private void cbNomPersonnelModification_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbNomPersonnelModification != null && cbNomPersonnelModification.SelectedItem != "") 
            {
                Personnel personnelSelectione = ((Personnel)cbNomPersonnelModification.SelectedItem);
                tbMailPersonnelModification.Text = personnelSelectione.EmailPersonnel;
                tbNouveauNomPersonnelModifier.Text = personnelSelectione.NomPersonnel;
                tbPrenomPersonnelModification.Text = personnelSelectione.PrenomPersonnel;
            }
        }
    }
}
