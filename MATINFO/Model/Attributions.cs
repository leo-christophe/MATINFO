using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if (value <= DateTime.Today.AddYears(50) && value >= DateTime.Today.AddYears(-50))
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
                // Si l'ID catégorie existe bien, n'est pas null et est un entier : 
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
                // Si l'ID catégorie existe bien, n'est pas null et est un entier : 
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

        public void Delete()
        {
            Console.WriteLine(this.DateAttribution.ToString("yyyy'-'MM'-'dd"));
            DataAccess accesBD = new DataAccess();
            String requete = $"DELETE FROM est_attribue WHERE idPersonnel = {this.FK_idPersonnel} AND idMateriel = {this.FK_idMateriel} AND dateattribution = '{this.DateAttribution.ToString("yyyy'-'MM'-'dd")}';";
            DataTable datas = accesBD.GetData(requete);
        }

        public ObservableCollection<Attributions> FindAll()
        {
            ObservableCollection<Attributions> lesAttributions = new ObservableCollection<Attributions>();
            DataAccess accesBD = new DataAccess();
            String requete = "select * from est_attribue ;";
            DataTable datas = accesBD.GetData(requete);
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
        /// FindBySelection permet d'affiner les résultats d'une recherche complète. Elle est composées au minimum d'une clause (WHERE, HAVE, ORDER...) 
        /// </summary>
        /// <param name="criteres"></param>
        /// <returns></returns>
        public ObservableCollection<Attributions> FindBySelection(string criteres)
        {
            ObservableCollection<Attributions> lesAttributions = new ObservableCollection<Attributions>();
            DataAccess accesBD = new DataAccess();
            String requete = $"select * from est_attribue {criteres};";
            DataTable datas = accesBD.GetData(requete);
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

        public bool Read()
        {
            DataAccess accesBD = new DataAccess();
            String requete = $"select idMateriel, idPersonnel from est_attribue where idMateriel = {this.FK_idMateriel}, idPersonnel = {this.FK_idPersonnel};";
            DataTable datas = accesBD.GetData(requete);

            //si datas est null ou vide
            if (datas != null || datas.Rows.Count <= 0)
            {
                //l'attribution n'existe pas
                return true;
            }
            else
                //l'attribution existe
                return false;
        }

        public void Update()
        {
            DataAccess accesBD = new DataAccess();
            String requete = $"" +
                $"UPDATE est_attribue" +
                $" SET dateattribution = '{this.DateAttribution}', " +
                $"      commenaireattribution = '{this.Commentaire}' " +
                $" WHERE idPersonnel = {this.FK_idPersonnel};" +
                $" AND idMateriel = {this.FK_idMateriel}" +
                $"AND dateattribution = {this.DateAttribution};";
            int datas = accesBD.SetData(requete);
        }
    }
}
