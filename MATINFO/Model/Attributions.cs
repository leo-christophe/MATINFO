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

        public Attributions(DateTime dateAttribution, string commentaire, int fK_idMateriel, int fK_idPersonnel, Materiel aMateriel, Personnel aPersonnel)
        {
            this.DateAttribution = dateAttribution;
            this.Commentaire = commentaire;
            this.FK_idMateriel = fK_idMateriel;
            this.FK_idPersonnel = fK_idPersonnel;

            this.AMateriel = aMateriel;
            this.APersonnel = aPersonnel;

        }

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
                if (value > 0)
                    fK_idMateriel = value;
                else
                    throw new ArgumentException("La clé étrangère d'ID matériel ne peut pas être <= 0!");
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
                fK_idPersonnel = value;
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
            String requete = $"DELETE FROM est_attribue WHERE idPersonnel = {this.FK_idPersonnel} AND idMateriel = {this.FK_idMateriel};";
            DataTable datas = accesBD.GetData(requete);
        }

        public void Delete()
        {
            DataAccess accesBD = new DataAccess();
            String requete = $"DELETE FROM est_attribue WHERE idPersonnel = {this.FK_idPersonnel} AND idMateriel = {this.FK_idMateriel};";
            DataTable datas = accesBD.GetData(requete);
        }

        public ObservableCollection<Attributions> FindAll()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Attributions> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            DataAccess accesBD = new DataAccess();
            String requete = $"" +
                $"UPDATE est_attribue" +
                $" SET DateAttribution = '{this.DateAttribution}', " +
                $"      CommentaireAttribution = {this.Commentaire} " +
                $" WHERE IdPersonnel = {this.FK_idPersonnel} AND IdMateriel = {this.FK_idMateriel};";
            int datas = accesBD.SetData(requete);
        }
    }
}
