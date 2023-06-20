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
        Attributions nouvelAttribut;
        int index;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Personnel personnel = (Personnel)cbModificationAttributionPersonnel.SelectionBoxItem;
            Materiel materiel = (Materiel)cbModificationAttributionMateriel.SelectionBoxItem;
            string commentaire = tbModificationAttributionCommentaire.Text;
            DateTime date = DateTime.Parse(dpModificationAttributionDate.Text);

            this.NouvelAttribut = new Attributions(date, commentaire, materiel, personnel);

            DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cbModificationAttributionMateriel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Si la combo Box de matériel n'est pas vide
            if (cbModificationAttributionMateriel.SelectedItem != "" && cbModificationAttributionMateriel.SelectedItem != null)
            { 
                List<Attributions> attributionsMateriel = new List<Attributions>(((Materiel)cbModificationAttributionMateriel.SelectedItem).SesAttributions);

                List<Personnel> lesPersonnelsAssocies = new List<Personnel>();

                foreach (Personnel personnel in applicationData.LesPersonnels)
                {
                    foreach (Attributions attribution in attributionsMateriel)
                    {
                        if (personnel.IdPersonnel == attribution.FK_idPersonnel)
                            lesPersonnelsAssocies.Add(attribution.APersonnel);
                    }
                }

                if (lesPersonnelsAssocies.Count == 0)
                    cbModificationAttributionPersonnel.IsEnabled = false;
                else
                {
                    cbModificationAttributionPersonnel.IsEnabled = true;
                    MAJ_ChampsFormulaireSelection();
                    cbModificationAttributionPersonnel.ItemsSource = lesPersonnelsAssocies;
                }

            }
        }

        private void cbModificationAttributionPersonnel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbModificationAttributionPersonnel.SelectedItem != null && cbModificationAttributionPersonnel.SelectedItem != "")
            {
                List <Attributions> attributionsPersonnel = new List<Attributions>(((Personnel)cbModificationAttributionPersonnel.SelectedItem).SesAttributions);

                List<Materiel> lesMateriauxAssocies = new List<Materiel>();

                foreach (Materiel materiel in applicationData.LesMateriaux)
                {
                    foreach (Attributions attribution in attributionsPersonnel)
                    {
                        if (materiel.IdMateriel == attribution.FK_idMateriel)
                            lesMateriauxAssocies.Add(attribution.AMateriel);
                    }
                }
                if (lesMateriauxAssocies.Count == 0)
                    cbModificationAttributionMateriel.IsEnabled = false;
                else
                {
                    cbModificationAttributionMateriel.IsEnabled = true;
                    MAJ_ChampsFormulaireSelection();
                    cbModificationAttributionMateriel.ItemsSource = lesMateriauxAssocies;
                }
            }
        }


        private void MAJ_ChampsFormulaireSelection()
        {
            if (cbModificationAttributionMateriel.SelectedItem != null && cbModificationAttributionPersonnel.SelectedItem != null &&
    cbModificationAttributionMateriel.SelectedItem != "" && cbModificationAttributionPersonnel.SelectedItem != "")
            {

                List<DateTime> lesDates = new List<DateTime>();
                DataAccess accesBD = new DataAccess();
                // Requête
                String requete = $"select dateAttribution from est_attribue WHERE idPersonnel = {((Personnel)cbModificationAttributionPersonnel.SelectedItem).IdPersonnel}" +
                    $" AND idMateriel = {((Materiel)cbModificationAttributionMateriel.SelectedItem).IdMateriel};";
                DataTable datas = accesBD.GetData(requete);
                if (datas != null)
                {
                    foreach (DataRow row in datas.Rows)
                    {
                        lesDates.Add(DateTime.Parse(((DateTime)row["dateAttribution"]).ToString("dd-MM-yyyy")));

                    }
                }

                cbDate.ItemsSource = lesDates;

                if (cbDate.SelectedItem != "" && cbDate.SelectedItem != null && cbModificationAttributionPersonnel.IsEnabled == true
                    && cbModificationAttributionMateriel.IsEnabled == true)
                { 
                    
                    Attributions attributionChoisi;

                    List<Attributions> lesAttributions = new List<Attributions>(applicationData.LesAttributions);

                    attributionChoisi = lesAttributions.Find(
                g => g.FK_idMateriel == ((Materiel)cbModificationAttributionMateriel.SelectedItem).IdMateriel &&
                g.FK_idPersonnel == ((Personnel)cbModificationAttributionPersonnel.SelectedItem).IdPersonnel &&
                g.DateAttribution == ((DateTime)cbDate.SelectedItem));

                    tbModificationAttributionCommentaire.Text = attributionChoisi.Commentaire;

                    dpModificationAttributionDate.Text = attributionChoisi.DateAttribution.ToString();
                }
            }



                
        }

        private void Button_Click_ReinitialiseFiltre(object sender, RoutedEventArgs e)
        {
            cbModificationAttributionMateriel.ItemsSource = applicationData.LesMateriaux;
            cbModificationAttributionPersonnel.ItemsSource = applicationData.LesPersonnels;
            cbDate.ItemsSource = "";
            cbModificationAttributionPersonnel.Items.Refresh();
            cbModificationAttributionMateriel.Items.Refresh();
            cbDate.Items.Refresh();
        }
    }
}
    
