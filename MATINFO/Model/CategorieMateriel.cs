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
        private int idCategorie;
        private string nomCategorie;

        public CategorieMateriel()
        {
        }

        public CategorieMateriel(int idCategorie, string nomCategorie)
        {
            this.IdCategorie = idCategorie;
            this.NomCategorie = nomCategorie;
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

        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
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
