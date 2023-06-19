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
    /// Logique d'interaction pour modifMateriel.xaml
    /// </summary>
    public partial class modifMateriel : Window
    {
        Materiel nouveauMateriel;


        public modifMateriel()
        {
            InitializeComponent();
        }

        private void Button_ClickCreationOK(object sender, RoutedEventArgs e)
        {
            string nomMateriel = tbNomMaterielModificationMateriel.Text;
            string referenceConstructeur = tbReferenceConstructeurModificationMateriel.Text;
            string codeBarreInventaire = tbCodeBarreModificationMateriel.Text;

            CategorieMateriel categorie = (CategorieMateriel)cbNomCategorieModificationMateriel.SelectionBoxItem;
            Materiel materiel = (Materiel)cbNomMaterielModificationMateriel.SelectionBoxItem;

            this.NouveauMateriel = new Materiel(materiel.IdMateriel, categorie.IdCategorie, nomMateriel, referenceConstructeur, codeBarreInventaire);

            DialogResult = true;
        }

        private void Button_ClickCreationAnnuler(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public Materiel NouveauMateriel
        {
            get
            {
                return this.nouveauMateriel;
            }

            set
            {
                this.nouveauMateriel = value;
            }
        }

    }
}
