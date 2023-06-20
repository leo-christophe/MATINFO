using System;
using System.Collections.ObjectModel;
using System.Data;

namespace MATINFO.Model
{
    public class Attributions : Crud<Attributions>
    {
        private DateTime dateAttribution;
        private String commentaire;
        private int fK_idMateriel;
        private int fK_idPersonnel;

        private Materiel aMateriel;
        private Personnel aPersonnel;

        public Attributions(DateTime dateAttribution, string commentaire, int fK_idMateriel, int fK_idPersonnel)
        {
            this.DateAttribution = dateAttribution;
            this.Commentaire = commentaire;
            this.FK_idMateriel = fK_idMateriel;
            this.FK_idPersonnel = fK_idPersonnel;
        }
        public Attributions(DateTime dateAttribution, string commentaire, Materiel materiel, Personnel personnel)
        {
            this.DateAttribution = dateAttribution;
            this.Commentaire = commentaire;
            this.AMateriel = materiel;
            this.APersonnel = personnel;
            this.FK_idMateriel = materiel.IdMateriel;
            this.FK_idPersonnel = personnel.IdPersonnel;
        }
        public Attributions()
        { }

        public DateTime DateAttribution
        {
            get
            {
                return dateAttribution;
            }

            set
            {
                if (value <= DateTime.Today.AddYears(100) && value >= DateTime.Today.AddYears(-100))
                    dateAttribution = value;
                else
                    throw new ArgumentException("Date incohérente !");
            }
        }

        public string Commentaire
        {
            get
            {
                return commentaire;
            }

            set
            {
                if (!String.IsNullOrEmpty(value) && value.Length <= 1000)
                    commentaire = value;
                else
                    throw new Exception("La chaîne de caractère doit être non nulle et <= 1000 caractères.");
            }
        }

        public int FK_idMateriel
        {
            get
            {
                return fK_idMateriel;
            }

            set
            {
                // Si l'ID matériel, n'est pas null et est un entier : 
                if (!String.IsNullOrEmpty(value.ToString()) && value.GetType() == typeof(int) && value > 0)
                    fK_idMateriel = value;
                else
                    throw new Exception("La clé étrangère doit exister, ne doit pas être null ou vide et doit être un entier > 0.");
            }
        }

        public int FK_idPersonnel
        {
            get
            {
                return fK_idPersonnel;
            }

            set
            {
                // Si l'ID personnel, n'est pas null et est un entier : 
                if (!String.IsNullOrEmpty(value.ToString()) && value.GetType() == typeof(int) && value > 0)
                    fK_idPersonnel = value;
                else
                    throw new Exception("La clé étrangère doit exister, ne doit pas être null ou vide et doit être un entier > 0.");
            }
        }

        public Materiel AMateriel
        {
            get
            {
                return aMateriel;
            }

            set
            {
                aMateriel = value;
            }
        }

        public Personnel APersonnel
        {
            get
            {
                return this.aPersonnel;
            }

            set
            {
                this.aPersonnel = value;
            }
        }

        /// <summary>
        /// ToString permet de récupérer les propriétés de l'objet en chaîne de caractère.
        /// </summary>
        /// <returns>Une chaîne de caractère composé de l'ID personnel, de l'ID matériel, du commentaire et de la date d'attribution.</returns>
        public override string? ToString()
        {
            return $"\n{this.FK_idPersonnel} \n{this.FK_idMateriel} \n{this.Commentaire} \n{this.DateAttribution}";
        }

        /// <summary>
        /// Equals permet de vérifier l'égalité entre deux objets attributions (DateAttribution, Commentaire, IdMateriel, IdPersonnel)
        /// </summary>
        /// <param name="obj">Le deuxième objet à vérifier l'égalité</param>
        /// <returns>true si égal, false sinon</returns>
        public override bool Equals(object? obj)
        {
            return obj is Attributions attributions &&
                   this.DateAttribution == attributions.DateAttribution &&
                   this.Commentaire == attributions.Commentaire &&
                   this.FK_idMateriel == attributions.FK_idMateriel &&
                   this.FK_idPersonnel == attributions.FK_idPersonnel;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.DateAttribution, this.Commentaire, this.FK_idMateriel, this.FK_idPersonnel);
        }

        /// <summary>
        /// La méthode Create permet d'insérer une nouvelle attribution dans la table est_attribue. Elle envoie une requête SQL, à partir de l'ID personnel,
        /// de l'ID materiel, de la date d'attribution et du commentaireattribution.
        /// </summary>
        public void Create()
        {
            DataAccess accesBD = new DataAccess();
            String requete = $"" +
                $"INSERT INTO est_attribue " +
                $"(idpersonnel, idmateriel, dateattribution, commentaireattribution) " +
                $"VALUES " +
                $"({this.FK_idPersonnel}, {this.FK_idMateriel}, '{this.DateAttribution}', '{this.Commentaire}');";
            int datas = accesBD.SetData(requete);
        }

        /// <summary>
        /// La méthode Delete permet de supprimer l'attribution dans la base de données correspondant à l'objet à partir de l'ID personnel, date d'Attribution et de l'ID materiel.
        /// </summary>
        public void Delete()
        {
            // Accès à la BD
            DataAccess accesBD = new DataAccess();
            // requete
            String requete = $"DELETE FROM est_attribue WHERE idPersonnel = {this.FK_idPersonnel} AND idMateriel = {this.FK_idMateriel} AND dateattribution = '{this.DateAttribution.ToString("yyyy'-'MM'-'dd")}';";
            // recuperation des données
            DataTable datas = accesBD.GetData(requete);
        }

        /// <summary>
        /// La méthode FindAll permet de retourner une collection observable composé de tous les enregistrement de la table est_attribue.
        /// </summary>
        /// <returns>Une collection observable de Attribution composé de tout les enregistrements de la table est_attribue.</returns>
        public ObservableCollection<Attributions> FindAll()
        {
            ObservableCollection<Attributions> lesAttributions = new ObservableCollection<Attributions>();
            // Accès à la BD
            DataAccess accesBD = new DataAccess();
            // Requête SQL
            String requete = "select * from est_attribue ;";
            // récupération de la data
            DataTable datas = accesBD.GetData(requete);
            
            // Formation de la data
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    Attributions e = new Attributions((DateTime)row["dateattribution"], 
                        (String)row["commentaireattribution"], 
                        int.Parse(row["idmateriel"].ToString()), 
                        int.Parse(row["idpersonnel"].ToString()));
                    lesAttributions.Add(e);
                }
            }
            return lesAttributions;
        }

        /// <summary>
        /// FindBySelection permet d'affiner les résultats d'une recherche complète. Elle est composées au minimum d'une clause (WHERE, HAVE, ORDER...) 
        /// </summary>
        /// <param name="criteres">criteres est une chaîne de caractères composé de clauses SQL positionné après SELECT et FROM. </param>
        /// <returns>La sélection demandée sous la forme d'une collection observable de Attribution.</returns>
        public ObservableCollection<Attributions> FindBySelection(string criteres)
        {
            ObservableCollection<Attributions> lesAttributions = new ObservableCollection<Attributions>();
            // Accès à la base de données
            DataAccess accesBD = new DataAccess();
            // Requête
            String requete = $"select * from est_attribue {criteres};";
            // Récupération des données
            DataTable datas = accesBD.GetData(requete);

            // Formation des données
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    Attributions e = new Attributions((DateTime)row["dateattribution"], (String)row["commentaireattribution"], int.Parse(row["idmateriel"].ToString()), int.Parse(row["idpersonnel"].ToString()));
                    lesAttributions.Add(e);
                }
            }
            return lesAttributions;
        }

        /// <summary>
        /// La méthode READ permet de savoir si une attribution existe déjà selon ses 3 id : idpersonnel, idmateriel, dateattribution. 
        /// </summary>
        /// <returns> Un booléen est retourné. true si l'attribution existe, false sinon.</returns>
        public bool Read()
        {
            // Accès à la base de données
            DataAccess accesBD = new DataAccess();
            String requete = $"select idMateriel, idPersonnel from est_attribue where idMateriel = {this.FK_idMateriel} AND idPersonnel = {this.FK_idPersonnel} AND dateAttribution = {this.DateAttribution};";
            DataTable datas = accesBD.GetData(requete);

            //si datas n'est ni null ni vide
            if (datas != null && datas.Rows.Count > 0)
                return true;
                //l'attribution existe

            //l'attribution n'existe pas
            return false;
        }

        /// <summary>
        /// La méthode UPDATE permet de mettre à jour l'attribution dans la base de donnée à partir des propriétés de l'objet. On prends les 2 propriétés modifiable de l'objet :
        /// commentaire et dateattribution et on redéfinit les attributs de la table est_attribue à l'enregistrement correspondant à l'objet (trouvé  grace aux 3 IDS)
        /// </summary>
        public void Update()
        {
            // Accès à la base de données
            DataAccess accesBD = new DataAccess();
            // Requête
            String requete = $"" +
                $"UPDATE est_attribue" +
                $" SET dateattribution = '{this.DateAttribution}', " +
                $"      commenaireattribution = '{this.Commentaire}' " +
                $" WHERE idPersonnel = {this.FK_idPersonnel};" +
                $" AND idMateriel = {this.FK_idMateriel}" +
                $" AND dateattribution = {this.DateAttribution};";
            // Envoi de la requête
            int datas = accesBD.SetData(requete);
        }
    }
}
