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
    public partial class creerMateriel : Window
    {
        Materiel nouveauMateriel;

        public Materiel NouveauMateriel { get => nouveauMateriel; set => nouveauMateriel = value; }

        public creerMateriel()
        {
            InitializeComponent();
            
        }
        
        private void Button_ClickCreationOK(object sender, RoutedEventArgs e)
        {
            string nomMateriel = tbNomMaterielCreation.Text;
            string referenceConstructeur = tbReferenceConstructeurCreation.Text;
            string codeBarreInventaire = tbCodeBarreCreation.Text;
            CategorieMateriel categorie = (CategorieMateriel)cbCategorieChoix.SelectionBoxItem;
            this.NouveauMateriel = new Materiel(categorie.IdCategorie, nomMateriel, referenceConstructeur, codeBarreInventaire, categorie);
            
            DialogResult = true;
        }

        private void Button_ClickCreationAnnuler(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
