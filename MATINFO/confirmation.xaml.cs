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
    /// Logique d'interaction pour confirmation.xaml
    /// </summary>
    public partial class confirmation : Window
    {

        private bool supprimer;
        public confirmation()
        {
            this.Supprimer = false;
            InitializeComponent();
            
        }

        public bool Supprimer
        {
            get
            {
                return supprimer;
            }

            set
            {
                supprimer = value;
            }
        }

        private void ButtonClickAnnuler(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.Supprimer = false; 
        }

        private void ButtonClickConfirmer(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.Supprimer = true;
        }
    }
}
