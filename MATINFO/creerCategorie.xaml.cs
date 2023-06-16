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
        CategorieMateriel nouvelleCategorie;

        public CategorieMateriel NouvelleCategorie { get => nouvelleCategorie; set => nouvelleCategorie = value; }

        public creerCategorie()
        {
            InitializeComponent();
        }
        
        private void Button_ClickCreationOK(object sender, RoutedEventArgs e)
        {
            NouvelleCategorie = new CategorieMateriel(tbNomCategorieCreation.Text);
            Console.WriteLine(NouvelleCategorie.IdCategorie);
            DialogResult = true;

        }

        private void Button_ClickCreationAnnuler(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

        }
    }
}
