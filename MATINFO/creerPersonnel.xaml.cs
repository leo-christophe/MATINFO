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
        Personnel nouveauPersonnel;

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

        private void Button_ClickCreationOK(object sender, RoutedEventArgs e)
        {
            NouveauPersonnel = new Personnel(tbNomPersonnel.Text, tbPrenomPersonnel.Text, tbMailPersonnel.Text);
            DialogResult = true;
        }
        private void Button_ClickCreationAnnuler(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

        }
    }
}