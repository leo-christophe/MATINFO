using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
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
                idPersonnel = value;
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
                nomPersonnel = value;
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
                prenomPersonnel = value;
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
                emailPersonnel = value;
            }
        }
        public int CalculerNouvelId()
        {
            ObservableCollection<Personnel> nouvelIdList = this.FindAll();
            return nouvelIdList.Count+1;
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

        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
