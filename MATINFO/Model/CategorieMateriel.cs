using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MATINFO.Model
{
    public class CategorieMateriel : Crud<CategorieMateriel>

    {
        private ObservableCollection<Materiel> lesMateriauxCM;
        private int idCategorie;
        private static int _idCompteur ;
        private string nomCategorie;

        public CategorieMateriel()
        {
        }

        public CategorieMateriel(int idCategorie, string nomCategorie)
        {
            this.IdCategorie = idCategorie;
            this.NomCategorie = nomCategorie;
        }

        public CategorieMateriel(string nomCategorie)
        {
            this.IdCategorie = this.CalculerNouvelId();
            this.NomCategorie = nomCategorie;
        }

        public int CalculerNouvelId( )
        {
            ObservableCollection<CategorieMateriel> nouvelIdList = this.FindBySelection(" GROUP BY idCategorie, nomCategorie HAVING idCategorie = max(idCategorie) ; ");
            return nouvelIdList.Count;
        }

        public int IdCategorie
        {
            get
            {
                return idCategorie;
            }

            set
            {
                idCategorie = value;
            }
        }

        public string NomCategorie
        {
            get
            {
                return nomCategorie;
            }

            set
            {
                nomCategorie = value;
            }
        }

        public ObservableCollection<Materiel> LesMateriauxCM
        {
            get
            {
                return lesMateriauxCM;
            }

            set
            {
                lesMateriauxCM = value;
            }
        }

        public void Create()
        {
            DataAccess accesBD = new DataAccess();
            String requete = $"" +
                $"INSERT INTO CATEGORIE_MATERIEL " +
                $"(IdCategorie, NomCategorie) " +
                $"VALUES " +
                $"({this.IdCategorie}, '{this.NomCategorie}');";
            int datas = accesBD.SetData(requete);
        }

        public void Delete()
        {
            DataAccess accesBD = new DataAccess();
            String requete = $"DELETE FROM CATEGORIE_MATERIEL WHERE idCategorie = {this.IdCategorie};";
            DataTable datas = accesBD.GetData(requete);
        }

        public ObservableCollection<CategorieMateriel> FindAll()
        {
            ObservableCollection<CategorieMateriel> lesCategories = new ObservableCollection<CategorieMateriel>();
            DataAccess accesBD = new DataAccess();
            String requete = "select idcategorie, nomcategorie from categorie_materiel ;";
            DataTable datas = accesBD.GetData(requete);
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    CategorieMateriel e = new CategorieMateriel(
                        int.Parse(row["idCategorie"].ToString()),
                        (String)row["nomCategorie"]
                        );
                    lesCategories.Add(e);
                }
            }
            return lesCategories;
        }

        public ObservableCollection<CategorieMateriel> FindBySelection(string criteres)
        {
            ObservableCollection<CategorieMateriel> lesCategories = new ObservableCollection<CategorieMateriel>();
            DataAccess accesBD = new DataAccess();
            String requete = $"select * from categorie_Materiel {criteres};";
            DataTable datas = accesBD.GetData(requete);
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    CategorieMateriel e = new CategorieMateriel(
                         int.Parse(row["idCategorie"].ToString()),
                        (String)row["nomCategorie"]
                        );
                    lesCategories.Add(e);
                }
            }
            return lesCategories;
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
