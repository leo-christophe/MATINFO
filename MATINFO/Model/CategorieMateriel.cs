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


        public int CalculerNouvelId()
        {
            // Accès à la base de données
            DataAccess accesBD = new DataAccess();

            // On récupère l'ID max et on rajoute 1 pour avoir un nouvel id. On le renomme en E pour pouvoir le récupérer.
            String requete = "SELECT MAX(idcategorie) + 1 AS \"E\" FROM categorie_materiel;";
            DataTable datas = accesBD.GetData(requete);
            if (datas != null)
                // On récupère la ligne 1 (la seule puisque l'ID est unique) et on prend la colonne E : le maximum
                return int.Parse(datas.Rows[0]["E"].ToString());
            else
                return 1;
        }

        public int IdCategorie
        {
            get
            {
                return idCategorie;
            }

            set
            {
                if (!string.IsNullOrEmpty(value.ToString()) && value.GetType() == typeof(int) && value > 0)
                    idCategorie = value;
                else
                    throw new ArgumentException("L'ID de catégorie matériel doit être un entier supérieur à 0 et non null!");
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
                if ( !string.IsNullOrEmpty(value.ToString()) && value.GetType() == typeof(string) && value.Length <= 50 )
                    nomCategorie = value;
                else
                    throw new ArgumentException("Le nom de la catégorie doit être une chaîne de caractères non nulle de taille <= 50! ");
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
                if (value.GetType() == typeof(ObservableCollection<Materiel>) && value != null)
                    lesMateriauxCM = value;
                else
                    throw new ArgumentException("lesMateriauxCM doit être non null et de type ObservableCollection<Materiel>!");
            }
        }


        public void Create()
        {
            DataAccess accesBD = new DataAccess();
            String requete = $"" +
                $"INSERT INTO CATEGORIE_MATERIEL " +
                $"(IdCategorie, NomCategorie) " +
                $"VALUES " +
                $"(DEFAULT, '{this.NomCategorie}');";
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

        public bool Read()
        {
            DataAccess accesBD = new DataAccess();
            String requete = $"" +
                $"SELECT idCategorie FROM categorie_materiel WHERE idCategorie = {this.IdCategorie}";
            DataTable datas = accesBD.GetData(requete);

            // si datas est null ou vide
            if (datas == null || datas.Rows.Count <= 0)
                return true;
                // n'existe pas
            return false;
            // existe
        }

        public void Update()
        {
            DataAccess accesBD = new DataAccess();
            String requete = $"" +
                $"UPDATE CATEGORIE_MATERIEL" +
                $" SET NomCategorie = '{this.NomCategorie}'" +
                $" WHERE IdCategorie = {this.IdCategorie};";
            int datas = accesBD.SetData(requete);
        }
    }
}
