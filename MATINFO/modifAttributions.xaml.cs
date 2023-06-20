using MATINFO.Model;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Logique d'interaction pour modifAttributions.xaml
    /// </summary>
    public partial class modifAttributions : Window
    {
        // Nouvel attribut 
        private Attributions nouvelAttribut;
        public modifAttributions()
        {
            InitializeComponent();
        }

        public Attributions NouvelAttribut
        {
            get
            {
                return this.nouvelAttribut;
            }

            set
            {
                this.nouvelAttribut = value;
            }
        }

        /// <summary>
        /// Cette méthode se déclenche lorsque l'utilisateur décide de valider le formulaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Si tous est bien renseigné, le nouvel attribut va être créée.
            if (!String.IsNullOrEmpty(cbDate.Text) && !String.IsNullOrEmpty(cbModificationAttributionMateriel.Text) && !String.IsNullOrEmpty(cbModificationAttributionPersonnel.Text)
                && !String.IsNullOrEmpty(tbModificationAttributionCommentaire.Text) && !String.IsNullOrEmpty(dpModificationAttributionDate.Text))
            { 
                Personnel personnel = (Personnel)cbModificationAttributionPersonnel.SelectionBoxItem;
                Materiel materiel = (Materiel)cbModificationAttributionMateriel.SelectionBoxItem;
                string commentaire = tbModificationAttributionCommentaire.Text;
                DateTime date = DateTime.Parse(dpModificationAttributionDate.Text);

                this.NouvelAttribut = new Attributions(date, commentaire, materiel, personnel);

                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Tous les champs doivent être renseignés !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Cette méthode se déclenche lorsque l'utilisateur annule la demande de création
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// Cette méthode permet de mettre à jour la combo box personnel selon le matériel sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbModificationAttributionMateriel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Si la combo Box de matériel n'est pas vide
            if (cbModificationAttributionMateriel.SelectedItem != "" && cbModificationAttributionMateriel.SelectedItem != null)
            { 
                List<Attributions> attributionsMateriel = new List<Attributions>(((Materiel)cbModificationAttributionMateriel.SelectedItem).SesAttributions);

                List<Personnel> lesPersonnelsAssocies = new List<Personnel>();

                // On parcours Personnel dans les attributions en recherche des matériaux associés
                foreach (Personnel personnel in applicationData.LesPersonnels)
                {
                    foreach (Attributions attribution in attributionsMateriel)
                    {
                        if (personnel.IdPersonnel == attribution.FK_idPersonnel)
                            lesPersonnelsAssocies.Add(attribution.APersonnel);
                    }
                }

                // Bloquer la combo box matériaux s'il n'y en a aucun
                if (lesPersonnelsAssocies.Count == 0)
                    cbModificationAttributionPersonnel.IsEnabled = false;
                else
                {
                    cbModificationAttributionPersonnel.IsEnabled = true;
                    // On met à jour les autres formulaires
                    MAJ_ChampsFormulaireSelection();
                    // On change la source de la combo box par les matériaux associés au personnel.
                    cbModificationAttributionPersonnel.ItemsSource = lesPersonnelsAssocies;
                }

            }
        }

        /// <summary>
        /// Cette méthode permet de mettre à jour la combo box matériel selon le personnel sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbModificationAttributionPersonnel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Si un personnel est selectionné dans la combo box
            if (cbModificationAttributionPersonnel.SelectedItem != null && cbModificationAttributionPersonnel.SelectedItem != "")
            {
                List <Attributions> attributionsPersonnel = new List<Attributions>(((Personnel)cbModificationAttributionPersonnel.SelectedItem).SesAttributions);

                List<Materiel> lesMateriauxAssocies = new List<Materiel>();

                // On parcours les matériaux et les personnels et on récupère ceux qui sont liés par une attribution
                foreach (Materiel materiel in applicationData.LesMateriaux)
                {
                    foreach (Attributions attribution in attributionsPersonnel)
                    {
                        if (materiel.IdMateriel == attribution.FK_idMateriel)
                            lesMateriauxAssocies.Add(attribution.AMateriel);
                    }
                }
                // Bloquer la combo box matériaux s'il n'y en a aucun
                if (lesMateriauxAssocies.Count == 0)
                    cbModificationAttributionMateriel.IsEnabled = false;
                else
                {
                    cbModificationAttributionMateriel.IsEnabled = true;
                    // On met à jour les autres formulaires
                    MAJ_ChampsFormulaireSelection();
                    // On change la source de la combo box par les matériaux associés au personnel.
                    cbModificationAttributionMateriel.ItemsSource = lesMateriauxAssocies;
                }
            }
        }


        /// <summary>
        /// Cette méthode permet de mettre à jour les différents éléments du formulaire selon l'attribut choisi.
        /// </summary>
        private void MAJ_ChampsFormulaireSelection()
        {
            // Si le matériel et le personnel ont été choisi
            if (cbModificationAttributionMateriel.SelectedItem != null && cbModificationAttributionPersonnel.SelectedItem != null &&
    cbModificationAttributionMateriel.SelectedItem != "" && cbModificationAttributionPersonnel.SelectedItem != "")
            {

                // On cherche à récupérer toutes les dates correspondant à une attribution
                List<DateTime> lesDates = new List<DateTime>();
                DataAccess accesBD = new DataAccess();
                // Requête
                String requete = $"select dateAttribution from est_attribue WHERE idPersonnel = {((Personnel)cbModificationAttributionPersonnel.SelectedItem).IdPersonnel}" +
                    $" AND idMateriel = {((Materiel)cbModificationAttributionMateriel.SelectedItem).IdMateriel};";
                DataTable datas = accesBD.GetData(requete);
                if (datas != null)
                {
                    foreach (DataRow row in datas.Rows)
                        lesDates.Add(DateTime.Parse(((DateTime)row["dateAttribution"]).ToString("dd-MM-yyyy")));
                }
                // Mettre à jour l'Itemsource de la combo box de date
                cbDate.ItemsSource = lesDates;

                    // Si la date, le matériel et le personnel ont été choisi
                    if (cbDate.SelectedItem != "" && cbDate.SelectedItem != null && cbModificationAttributionPersonnel.IsEnabled == true
                        && cbModificationAttributionMateriel.IsEnabled == true)
                    { 
                    
                        Attributions attributionChoisi;

                        List<Attributions> lesAttributions = new List<Attributions>(applicationData.LesAttributions);

                    // On choisi l'attribution à afficher dans les cases
                        attributionChoisi = lesAttributions.Find(
                    g => g.FK_idMateriel == ((Materiel)cbModificationAttributionMateriel.SelectedItem).IdMateriel &&
                    g.FK_idPersonnel == ((Personnel)cbModificationAttributionPersonnel.SelectedItem).IdPersonnel &&
                    g.DateAttribution == ((DateTime)cbDate.SelectedItem));

                        tbModificationAttributionCommentaire.Text = attributionChoisi.Commentaire;

                        dpModificationAttributionDate.Text = attributionChoisi.DateAttribution.ToString();
                    }
            }  
        }

        /// <summary>
        /// Cette méthode permet de réinitialiser le filtre appliqué lors de la sélection d'un personnage, d'un matériel et d'une date.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_ReinitialiseFiltre(object sender, RoutedEventArgs e)
        {
            // On change les items sources à ceux basiques
            cbModificationAttributionMateriel.ItemsSource = applicationData.LesMateriaux;
            cbModificationAttributionPersonnel.ItemsSource = applicationData.LesPersonnels;
            cbDate.ItemsSource = "";

            // On rafraîchit les différents combo box pour qu'ils se mettent à jour
            cbModificationAttributionPersonnel.Items.Refresh();
            cbModificationAttributionMateriel.Items.Refresh();
            cbDate.Items.Refresh();
        }
    }
}
    
