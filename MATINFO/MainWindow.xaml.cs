using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MATINFO;
using MATINFO.Model;

namespace MATINFO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataAccess accesBD;
        bool res;
        public MainWindow()
        {
            InitializeComponent();

            accesBD = new DataAccess();
            res = accesBD.OpenConnection();
        }

        /// <summary>
        /// Cette méthode permet de détecter le clic de l'utilisateur sur un bouton "Create". Elle est universelle pour les 4 boutons : on récupère le nom pour faire les différents
        /// traitements.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClickCreate(object sender, RoutedEventArgs e)
        {
            Button bouton = (Button)sender;
            // On vérifie le nom du bouton appuyé.
            switch (bouton.Name)
            {
                // Bouton créer un personnage
                case "btCreerPers":
                    {
                        // On créée une nouvelle fenêtre creerPersonnel
                        creerPersonnel creationWinPers = new creerPersonnel();
                        // On affiche la fenêtre en mode "dialog" (va nous retourner un booléen pour savoir l'avis de l'utilisateur)
                        creationWinPers.ShowDialog();
                        if ((bool)creationWinPers.DialogResult == true)
                        {
                            // On ajoute le nouvel utilisateur dans applicationData
                            applicationdata.LesPersonnels.Add(creationWinPers.NouveauPersonnel);
                            // On le créer dans la base de données
                            creationWinPers.NouveauPersonnel.Create();
                            // On rafraîchit
                            listViewPersonnel.Items.Refresh();
                        }
                        break;
                    }
                 
                // Bouton créer un materiel
                case "btCreerMat":
                    {
                        // On créer une nouvelle fenêtre creerMateriel
                        creerMateriel creationWinMat = new creerMateriel();
                        // On définit le datacontext de cette fenêtre par le même que MainWindow
                        creationWinMat.DataContext = applicationdata;

                        // Booléen qui va nous servir à savoir si on peut rajouter le nouveau matériel ou non
                        bool? confirm = creationWinMat.ShowDialog();

                        if ( confirm == true )
                        {
                            // on fait une liaison entre materiel et categorie_materiel
                            creationWinMat.NouveauMateriel.UneCategorieM = 
                                applicationdata.LesCategoriesMateriel.ToList().Find(g => g.IdCategorie == creationWinMat.NouveauMateriel.FK_idCategorie);

                            // on ajoute le nouveau materiel à la collection observable de applicationdata
                            applicationdata.LesMateriaux.Add(creationWinMat.NouveauMateriel);

                            // on ajoute le nouveau materiel dans la base de données
                            creationWinMat.NouveauMateriel.Create();

                            // on rafraîchit
                            listViewMateriel.Items.Refresh();
                        }

                        break;
                    }

                // Bouton créer une attribution
                case "btCreerAtt":
                    {
                        // On créer la fenêtre
                        creerAttribution creationWinAtt = new creerAttribution();
                        
                        creationWinAtt.ShowDialog();
                        if ((bool)creationWinAtt.DialogResult == true)
                        {
                            // On ajoute le nouvel attribution dans applicationData
                            applicationdata.LesAttributions.Add(creationWinAtt.NouvelAttribu);
                            // On l'ajoute dans la base de données
                            creationWinAtt.NouvelAttribu.Create();
                            // On rafraichit
                            listViewAttributions.Items.Refresh();
                        }
                        break;
                    }

                // Bouton créer une catégorie
                case "btCreerCat":
                    {
                        // On créer la fenêtre pour une catégorie.
                        creerCategorie creationWinCat = new creerCategorie();
                        creationWinCat.ShowDialog();
                        if ((bool)creationWinCat.DialogResult == true)
                        {
                            // On ajoute la nouvelle catégorie dans applicationData
                            applicationdata.LesCategoriesMateriel.Add(creationWinCat.NouvelleCategorie);
                            // On l'ajoute dans la base de données
                            creationWinCat.NouvelleCategorie.Create();
                            // On rafraîchit les 2 listviews (matériel car il dépend de catégorie)
                            listViewCategories.Items.Refresh();
                            listViewMateriel.Items.Refresh();
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// Cette méthode est appelée lorsque l'utilisateur clique sur un bouton modification de n'importe quelle sorte.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClickModification(object sender, RoutedEventArgs e)
        {
            Button bouton = (Button)sender;
            switch (bouton.Name)
            {
                // Bouton modifier un personnel
                case "btModifierPers":
                {
                    // Création de la fenêtre de modification de personnage
                    modifPersonnel modificationPers = new modifPersonnel();

                    //on rentre les données dans la fenêtre de modification par défault
                    modificationPers.cbNomPersonnelModification.SelectedIndex = listViewPersonnel.SelectedIndex;
                    modificationPers.tbPrenomPersonnelModification.Text = ((Personnel)listViewPersonnel.SelectedItem).PrenomPersonnel;
                    modificationPers.tbNouveauNomPersonnelModifier.Text = ((Personnel)listViewPersonnel.SelectedItem).NomPersonnel;
                    modificationPers.tbMailPersonnelModification.Text = ((Personnel)listViewPersonnel.SelectedItem).EmailPersonnel;

                    modificationPers.ShowDialog();
                    if ((bool)modificationPers.DialogResult == true)
                    {
                        
                        List<Personnel> lePersonnel = new List<Personnel>(applicationdata.LesPersonnels);
                        // On trouve l'indice du personnel à modifier
                        int index = lePersonnel.FindIndex(x => x.IdPersonnel == modificationPers.NouveauPersonnel.IdPersonnel);

                        // On modifie le personnel dans applicationData
                        applicationdata.LesPersonnels[index] = new Personnel(index, modificationPers.NouveauPersonnel.NomPersonnel, modificationPers.NouveauPersonnel.PrenomPersonnel, modificationPers.NouveauPersonnel.EmailPersonnel);
                            
                        // Mettre à jour avec la bd
                        applicationdata.LesPersonnels[index].Update();
                        // Rafraichissement
                        listViewPersonnel.Items.Refresh();
                    }
                    break;
                }

                // Bouton modifier un matériel
                case "btModifierMat":
                {
                    // Définition de la fenêtre avec son data context
                    modifMateriel modificationMat = new modifMateriel();
                    modificationMat.DataContext = this.applicationdata;

                    modificationMat.ShowDialog();
                    if ((bool)modificationMat.DialogResult)
                    {
                        List<Materiel> ListMaterials = new List<Materiel>(applicationdata.LesMateriaux);
                        List<CategorieMateriel> ListCategories = new List<CategorieMateriel>(applicationdata.LesCategoriesMateriel);

                        // Trouver l'indice de l'élément à modifier
                        int index = ListMaterials.FindIndex(x => x.IdMateriel == modificationMat.NouveauMateriel.IdMateriel);

                        // Modification de ses propriétés une par une
                        applicationdata.LesMateriaux[index].NomMateriel = modificationMat.NouveauMateriel.NomMateriel;
                        applicationdata.LesMateriaux[index].FK_idCategorie = modificationMat.NouveauMateriel.FK_idCategorie;
                        applicationdata.LesMateriaux[index].ReferenceConstructeur = modificationMat.NouveauMateriel.ReferenceConstructeur;
                        applicationdata.LesMateriaux[index].CodeBarreInventaire = modificationMat.NouveauMateriel.CodeBarreInventaire;
                        applicationdata.LesMateriaux[index].UneCategorieM = ListCategories.Find(x => x.IdCategorie == modificationMat.NouveauMateriel.FK_idCategorie);

                        // Mettre à jour avec la bd
                        applicationdata.LesMateriaux[index].Update();
                        // Rafraichissement
                        listViewMateriel.Items.Refresh();
                    }
                    break;
                }

                // Bouton modifier un attribut
                case "btModifierAtt":
                {
                    // Création de la fenêtre avec son datacontext et ses itemsources
                    modifAttributions modificationAtt = new modifAttributions();
                    modificationAtt.Owner = this;

                    modificationAtt.DataContext = this.applicationdata;

                    // Les matériaux et les personnels pour ensuite les attributions
                    modificationAtt.cbModificationAttributionMateriel.ItemsSource = applicationdata.LesMateriaux;
                    modificationAtt.cbModificationAttributionPersonnel.ItemsSource = applicationdata.LesPersonnels;

                    modificationAtt.ShowDialog();
                    if ((bool)modificationAtt.DialogResult == true)
                    {
                        int index = 0;
                        // On modifie la dataapplication par la nouvelle attribution
                        foreach (Attributions attribut in applicationdata.LesAttributions)
                        {
                            if (attribut.FK_idMateriel == modificationAtt.NouvelAttribut.FK_idMateriel && attribut.FK_idPersonnel == modificationAtt.NouvelAttribut.FK_idPersonnel && attribut.DateAttribution == modificationAtt.NouvelAttribut.DateAttribution)
                            {
                                index = applicationdata.LesAttributions.IndexOf(attribut);
                                applicationdata.LesAttributions[index] = modificationAtt.NouvelAttribut;
                            }
                        }

                        // Mettre à jour avec la bd
                        applicationdata.LesAttributions[index].Update();
                        // Rafraichissement
                        listViewAttributions.Items.Refresh();
                    }
                    break;
                }

                // Bouton modifier une catégorie
                case "btModifierCat":
                {
                    modifCategorie modificationCat = new modifCategorie();
                    // Si une valeur est sélectionnée dans la liste, alors cette valeur sera pré-rentrée dans la combo box
                    if (listViewCategories.SelectedIndex != -1)
                            modificationCat.cbCategorieChoix.SelectedItem = (CategorieMateriel)listViewCategories.SelectedItem;
                    modificationCat.ShowDialog();

                    if ((bool)modificationCat.DialogResult)
                    {
                        List<CategorieMateriel> ListCategories = new List<CategorieMateriel>(applicationdata.LesCategoriesMateriel);
                        // On trouve l'indice
                        int index = ListCategories.FindIndex(x => x.IdCategorie == modificationCat.CatAmodifier.IdCategorie);
                        // On rajoute dans l'applicationdata
                        applicationdata.LesCategoriesMateriel[index].NomCategorie = modificationCat.CatAmodifier.NomCategorie;

                        // Mettre à jour avec la bd
                        applicationdata.LesCategoriesMateriel[index].Update();
                        // Rafrachissement de la listview correspondante
                        listViewCategories.Items.Refresh();
                    }
                    break;
                }
            }
        }
        

        private void ButtonClickSuppression(object sender, RoutedEventArgs e)
        {
            confirmation fenetreConfirmation = new confirmation();
            fenetreConfirmation.ShowDialog();

            if ((bool)fenetreConfirmation.DialogResult)
            {

                switch (((Button)(sender)).Name)
                {
                    case "btSupprimerMat":
                        {
                            Materiel selectedMaterial = (Materiel)listViewMateriel.SelectedItem;
                            List<Attributions> TemporaryList = new List<Attributions>(applicationdata.LesAttributions);
                            foreach (Attributions attribution in TemporaryList)
                            {
                                if (attribution.FK_idMateriel == selectedMaterial.IdMateriel)
                                {
                                    attribution.Delete();
                                    applicationdata.LesAttributions.Remove(attribution);
                                }
                            }
                            listViewAttributions.Items.Refresh();

                            applicationdata.LesMateriaux[listViewMateriel.SelectedIndex].Delete();
                            applicationdata.LesMateriaux.RemoveAt(listViewMateriel.SelectedIndex);
                            listViewMateriel.Items.Refresh();
                            break;
                        }

                    case "btSupprimerCat":
                        {
                            int selectedIndex = listViewCategories.SelectedIndex;
                            CategorieMateriel selectedItem = (CategorieMateriel)listViewCategories.SelectedItem;

                            // suppression de la catégorie
                            applicationdata.LesCategoriesMateriel[selectedIndex].Delete();
                            applicationdata.LesCategoriesMateriel.RemoveAt(listViewCategories.SelectedIndex);
                            listViewCategories.Items.Refresh(); 

                            // Suppression des materiaux correspondant à la catégorie
                            // On créé une list temporaire pour pouvoir supprimer.
                            List<Materiel> TemporaryList = new List<Materiel>(applicationdata.LesMateriaux);
                            foreach (Materiel materiel in TemporaryList)
                            {
                                if (materiel.FK_idCategorie == selectedItem.IdCategorie)
                                {
                                    applicationdata.LesMateriaux.Remove(materiel);
                                    materiel.Delete();
                                }
                            }
                            listViewMateriel.Items.Refresh();
                            break;
                        }                

                    case "btSupprimerPers":
                        {
                            Personnel selectedPersonnel = (Personnel)listViewPersonnel.SelectedItem;
                            List<Attributions> TemporaryList = new List<Attributions>(applicationdata.LesAttributions);
                            foreach (Attributions attribution in TemporaryList)
                            {
                                if (attribution.FK_idPersonnel == selectedPersonnel.IdPersonnel)
                                {
                                    attribution.Delete();
                                    applicationdata.LesAttributions.Remove(attribution);
                                }
                            }
                            listViewAttributions.Items.Refresh();
 

                            applicationdata.LesPersonnels[listViewPersonnel.SelectedIndex].Delete();
                            applicationdata.LesPersonnels.RemoveAt(listViewPersonnel.SelectedIndex);
                            listViewPersonnel.Items.Refresh();
                            break;
                        }
                    case "btSupprimerAtt":
                        {
                            applicationdata.LesAttributions[listViewAttributions.SelectedIndex].Delete();
                            applicationdata.LesAttributions.RemoveAt(listViewAttributions.SelectedIndex);
                            listViewAttributions.Items.Refresh();
                            break;
                        }
                }
            }
        }
    }
}
