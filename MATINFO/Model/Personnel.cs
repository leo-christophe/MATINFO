using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MATINFO.Model
{
    public class Personnel : Crud<Personnel>
    {
        private int idPersonnel;
        private string nomPersonnel;
        private string prenomPersonnel;
        private string emailPersonnel;

        public Personnel()
        {
        }

        public Personnel(int idPersonnel, string nomPersonnel, string prenomPersonnel, string emailPersonnel)
        {
            this.NomPersonnel = nomPersonnel;
            this.PrenomPersonnel = prenomPersonnel;
            this.EmailPersonnel = emailPersonnel;
            this.IdPersonnel = idPersonnel;
        }

        public Personnel(string nomPersonnel, string prenomPersonnel, string emailPersonnel)
        {
            this.NomPersonnel = nomPersonnel;
            this.PrenomPersonnel = prenomPersonnel;
            this.EmailPersonnel = emailPersonnel;

            this.IdPersonnel = this.CalculerNouvelId();
        }

        public int IdPersonnel
        {
            get
            {
                return idPersonnel;
            }

            set
            {
                if (value > 0)
                    idPersonnel = value;
                else
                    throw new ArgumentException("l'ID du personnel doit être > 0");
            }
        }

        public string NomPersonnel
        {
            get
            {
                return nomPersonnel;
            }

            set
            {
                if (value.Length <= 20 && !string.IsNullOrEmpty(value))
                    nomPersonnel = value;
                else
                    throw new ArgumentException("Le nom du personnel doit être une chaîne de caractères non nulle de taille <= 20");
            }
        }

        public string PrenomPersonnel
        {
            get
            {
                return prenomPersonnel;
            }

            set
            {
                if (value.Length <= 20 && !string.IsNullOrEmpty(value))
                    prenomPersonnel = value;
                else
                    throw new ArgumentException("Le prénom du personnel doit être une chaîne de caractères non nulle de taille <= 20");
            }
        }

        public string EmailPersonnel
        {
            get
            {
                return emailPersonnel;
            }

            set
            {
                // Regex verifEmail = new Regex("^[a-z]@[a-z].[a-z]$");
                if (!string.IsNullOrEmpty(value) && value.Length <= 30 && value.GetType() == typeof(string))
                    emailPersonnel = value;
                else
                    throw new ArgumentException("Le mail du personnel doit être une chaine de caractère non nulle de longueur inférieur à 30 de format correct.");
            }
        }
        public int CalculerNouvelId()
        {
            return this.FindAll().Count + 1;
        }

        public void Create()
        {
            DataAccess accesBD = new DataAccess();
            String requete = $"INSERT INTO personnel (idpersonnel, emailpersonnel, nompersonnel, prenompersonnel) VALUES ({this.IdPersonnel}, '{this.EmailPersonnel}', '{this.NomPersonnel}', '{this.PrenomPersonnel}');";
            int datas = accesBD.SetData(requete);
        }

        public void Delete()
        {
            DataAccess accesBD = new DataAccess();
            String requete = "DELETE FROM personnel WHERE idpersonnel=" + IdPersonnel + ";";
            DataTable datas = accesBD.GetData(requete);

        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public ObservableCollection<Personnel> FindAll()
        {
            ObservableCollection<Personnel> lesPersonnels = new ObservableCollection<Personnel>();
            DataAccess accesBD = new DataAccess();
            String requete = "select idpersonnel, nompersonnel, prenompersonnel, emailpersonnel from personnel;";
            DataTable datas = accesBD.GetData(requete);
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    Personnel e = new Personnel(
                        int.Parse(row["idpersonnel"].ToString()),
                        (String)row["nompersonnel"],
                        (String)row["prenompersonnel"],
                        (String)row["emailpersonnel"]
                        );
                    lesPersonnels.Add(e);
                }
            }
            return lesPersonnels;
        }

        public ObservableCollection<Personnel> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Read()
        {
            return true;
        }

        public override string? ToString()
        {
            return $"Id : {this.IdPersonnel} \nNom : {this.NomPersonnel} \nPrenom : {this.PrenomPersonnel} \nMail : {this.EmailPersonnel}";
        }

        public void Update()
        {
            DataAccess accesBD = new DataAccess();
            String requete = $"UPDATE Personnel SET nomPersonnel = '{this.NomPersonnel}', prenomPersonnel = '{this.PrenomPersonnel}', emailPersonnel = '{this.EmailPersonnel}' WHERE idPersonnel = {this.idPersonnel};";
            int datas = accesBD.SetData(requete);
        }
    }
}
