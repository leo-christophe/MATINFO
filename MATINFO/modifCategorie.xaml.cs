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
        CategorieMateriel catAmodifier;

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


        private void Button_ClickCreationOK(object sender, RoutedEventArgs e)
        {
            CategorieMateriel categorieTemporaire = new CategorieMateriel();
            ObservableCollection<CategorieMateriel> catMateriel = categorieTemporaire.FindBySelection($"WHERE idCategorie = {((CategorieMateriel)cbCategorieChoix.SelectionBoxItem).IdCategorie}");
            CatAmodifier = new CategorieMateriel(catMateriel[0].IdCategorie, tbNouveauNom.Text);
            DialogResult = true;
        }

        private void Button_ClickCreationAnnuler(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
