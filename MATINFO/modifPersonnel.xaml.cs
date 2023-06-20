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
        // Le nouveau personnel modifié
        Personnel nouveauPersonnel;
        public modifPersonnel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Méthode appelée lorsque l'utilisateur décide de valider la modification d'un personnel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickModifOK(object sender, RoutedEventArgs e)
        {
            string nomPersonnel = tbNouveauNomPersonnelModifier.Text;
            string prenomPersonnel = tbPrenomPersonnelModification.Text;
            string mailNouveau = tbMailPersonnelModification.Text;

            Personnel personnel = (Personnel)cbNomPersonnelModification.SelectionBoxItem;
            this.nouveauPersonnel = new Personnel(personnel.IdPersonnel, nomPersonnel, prenomPersonnel, mailNouveau);

            DialogResult = true;
        }

        /// <summary>
        /// Méthode appelée lorsque l'utilisateur décide d'annuler la modification de personnel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Cette méthode est déclenchée lorsque la combo box du nom personnel est changée. Les différents champs du formulaires sont remplis par rapport à la personne sélectionnée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbNomPersonnelModification_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Si un élément est sélectionné 
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
