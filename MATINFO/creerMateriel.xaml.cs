using MATINFO.Model;
using System.Windows;


namespace MATINFO
{
    /// <summary>
    /// Logique d'interaction pour creer.xaml
    /// </summary>
    public partial class creerMateriel : Window
    {
        // Nouveau matériel communiqué à la MainWindow
        private Materiel nouveauMateriel;

        public creerMateriel()
        {
            InitializeComponent();

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

        /// <summary>
        /// Méthode déclenchée lorsque l'utilisateur décide de valider la création d'un matériel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickCreationOK(object sender, RoutedEventArgs e)
        {
            // Si tout va bien, le nouveau matériel est créée
            CategorieMateriel categorie = (CategorieMateriel)cbCategorieChoixMat.SelectionBoxItem;
            if ( !string.IsNullOrEmpty( cbCategorieChoixMat.SelectionBoxItem.ToString() ) && !string.IsNullOrEmpty(cbCategorieChoixMat.SelectionBoxItem.ToString()) 
                && !string.IsNullOrEmpty(tbNomMaterielCreation.Text) && !string.IsNullOrEmpty( tbReferenceConstructeurCreation.Text ) && !string.IsNullOrEmpty( tbCodeBarreCreation.Text ))
            { 
                
                // nouvel objet
                this.NouveauMateriel = new Materiel(categorie.IdCategorie,
                    tbNomMaterielCreation.Text,
                    tbReferenceConstructeurCreation.Text,
                    tbCodeBarreCreation.Text);

            
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Tous les champs doivent être renseignés.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Méthode déclenchée lorsque l'utilisateur décide d'annuler la création d'un matériel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickCreationAnnuler(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
