using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MATINFO.Model
{
    public class Materiel : Crud<Materiel>
    {
        private int idMateriel;
        private int fK_idCategorie;
        private string? nomMateriel;
        private string? referenceConstructeur;
        private string? codeBarreInventaire;

        private CategorieMateriel uneCategorieM;

        public static int cptId = 1;

        public Materiel()
        {
        }

        public Materiel(int idMateriel, int fK_idCategorie, string nomMateriel, string referenceConstructeur, string codeBarreInventaire)
        {
            this.IdMateriel = idMateriel;
            this.FK_idCategorie = fK_idCategorie;
            this.NomMateriel = nomMateriel;
            this.ReferenceConstructeur = referenceConstructeur;
            this.CodeBarreInventaire = codeBarreInventaire;
        }

        public Materiel(int fK_idCategorie, string nomMateriel, string referenceConstructeur, string codeBarreInventaire, CategorieMateriel uneCategorieM)
        {
            this.FK_idCategorie = fK_idCategorie;
            this.NomMateriel = nomMateriel;
            this.ReferenceConstructeur = referenceConstructeur;
            this.CodeBarreInventaire = codeBarreInventaire;
            this.UneCategorieM = uneCategorieM;

            this.IdMateriel = cptId;
            cptId++;
        }

        public int IdMateriel
        {
            get
            {
                return idMateriel;
            }

            set
            {
                // Si l'ID n'existe pas, et est un entier non null ou vide > 0.
                if (!String.IsNullOrEmpty(value.ToString()) && value.GetType() == typeof(int) && value > 0)
                    idMateriel = value;
                else
                    throw new ArgumentException("L'ID_MATERIEL doit être un entier > 0 non null ou vide qui ne doit pas exister.");
            }
        }

        public int FK_idCategorie
        {
            get
            {
                return fK_idCategorie;
            }

            set
            {
                // Si l'ID catégorie existe bien, n'est pas null et est un entier : 
                if (!String.IsNullOrEmpty(value.ToString()) && value.GetType() == typeof(int) && value > 0)
                    fK_idCategorie = value;
                else
                    throw new Exception("La clé étrangère doit exister, ne doit pas être null ou vide et doit être un entier > 0.");
            }
        }

        public string? NomMateriel
        {
            get
            {
                return nomMateriel;
            }

            set
            {
                if (!String.IsNullOrEmpty(value) && value.GetType() == typeof(string) && value.Length <= 100)
                    nomMateriel = value;
                else
                    throw new ArgumentException("Le nom du matériel doit être une chaîne de caractères non nulle ou vide");
            }
        }

        public string? ReferenceConstructeur
        {
            get
            {
                return referenceConstructeur;
            }

            set
            {
                if (!String.IsNullOrEmpty(value) && value.GetType() == typeof(string) && value.Length <= 100)
                    referenceConstructeur = value;
                else
                    throw new ArgumentNullException("La référence constructeur ne doit pas être null ou vide !");
            }
        }

        public string? CodeBarreInventaire
        {
            get
            {
                return codeBarreInventaire;
            }

            set
            {
                if (!String.IsNullOrEmpty(value) && value.GetType() == typeof(string) && value.Length <= 100)
                    codeBarreInventaire = value;
                else
                    throw new ArgumentNullException("Le code barre ne doit pas être null ou vide !");
            }
        }

        public CategorieMateriel UneCategorieM
        {
            get
            {
                return uneCategorieM;
            }

            set
            {
                uneCategorieM = value;
            }
        }

        public void Create()
        {
            DataAccess accesBD = new DataAccess();
            String requete = $"" +
                $"INSERT INTO MATERIEL " +
                $"(IdCategorie, NomMateriel, ReferenceConstructeurMateriel, CodeBarreInventaire) " +
                $"VALUES " +
                $"({this.FK_idCategorie}, '{this.NomMateriel}', '{this.ReferenceConstructeur}', '{this.CodeBarreInventaire}');";
            int datas = accesBD.SetData(requete);
            // nextval('materiel_idmateriel_seq'::regclass)
        }

        public void Delete()
        {
            DataAccess accesBD = new DataAccess();
            String requete = $"DELETE FROM MATERIEL WHERE idMateriel = {this.IdMateriel};";
            DataTable datas = accesBD.GetData(requete);
        }

        public ObservableCollection<Materiel> FindAll()
        {
            ObservableCollection<Materiel> lesMateriaux = new ObservableCollection<Materiel>();
            DataAccess accesBD = new DataAccess();
            String requete = "select idmateriel, idcategorie, nommateriel, referenceconstructeurmateriel, codebarreinventaire from materiel ;";
            DataTable datas = accesBD.GetData(requete);
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    Materiel e = new Materiel(
                        int.Parse(row["idmateriel"].ToString()),
                        int.Parse(row["idcategorie"].ToString()),
                        (String)row["nommateriel"],
                        (String)row["referenceconstructeurmateriel"],
                        (String)row["codebarreinventaire"]
                        );
                    lesMateriaux.Add(e);
                }
            }
            return lesMateriaux;
        }

        /// <summary>
        /// FindBySelection permet d'affiner les résultats d'une recherche complète. Elle est composées au minimum d'une clause (WHERE, HAVE, ORDER...) 
        /// </summary>
        /// <param name="criteres"></param>
        /// <returns></returns>
        public ObservableCollection<Materiel> FindBySelection(string criteres)
        {
            ObservableCollection<Materiel> lesMateriaux = new ObservableCollection<Materiel>();
            DataAccess accesBD = new DataAccess();
            String requete = $"select idmateriel, idcategorie, nommateriel, referenceconstructeurmateriel, codebarreinventaire from materiel {criteres};";
            DataTable datas = accesBD.GetData(requete);
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    Materiel e = new Materiel(
                        int.Parse(row["idmateriel"].ToString()),
                        int.Parse(row["idcategorie"].ToString()),
                        (String)row["nommateriel"],
                        (String)row["referenceconstructeurmateriel"],
                        (String)row["codebarreinventaire"]
                        );
                    lesMateriaux.Add(e);
                }
            }
            return lesMateriaux;
        }

        public bool Read()
        {
            ObservableCollection<Materiel> lesMateriaux = new ObservableCollection<Materiel>();
            DataAccess accesBD = new DataAccess();
            String requete = $"select idMateriel from materiel where codeBarreinventaire = {this.codeBarreInventaire};";
            DataTable datas = accesBD.GetData(requete);

            int cpt = 0;
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    cpt += 1;
                }
            }

            if (cpt > 0)
            { 
                Console.WriteLine("CPT > 0");
                return true;
            }
            else
            {
                Console.WriteLine("CPT <= 0");
                return true;
            }
        }

        public void Update()
        {
            DataAccess accesBD = new DataAccess();
            String requete = $"" +
                $"UPDATE MATERIEL" +
                $" SET NomMateriel = '{this.NomMateriel}', " +
                $"      IdCategorie = {this.FK_idCategorie}, " +
                $"      ReferenceConstructeurMateriel = '{this.ReferenceConstructeur}', " +
                $"      CodeBarreInventaire = '{this.CodeBarreInventaire}' " +
                $" WHERE IdMateriel = {this.IdMateriel};";
            int datas = accesBD.SetData(requete);
        }
    }
}
