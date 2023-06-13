using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
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

        public int IdMateriel
        {
            get
            {
                return idMateriel;
            }

            set
            {
                idMateriel = value;
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
                fK_idCategorie = value;
            }
        }

        public string NomMateriel
        {
            get
            {
                return nomMateriel;
            }

            set
            {
                nomMateriel = value;
            }
        }

        public string ReferenceConstructeur
        {
            get
            {
                return referenceConstructeur;
            }

            set
            {
                referenceConstructeur = value;
            }
        }

        public string CodeBarreInventaire
        {
            get
            {
                return codeBarreInventaire;
            }

            set
            {
                codeBarreInventaire = value;
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
            throw new NotImplementedException();
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

        public void Read()
        {
            ObservableCollection<Materiel> lesMateriaux = new ObservableCollection<Materiel>();
            DataAccess accesBD = new DataAccess();
            String requete = $"select * from materiel;";
            DataTable datas = accesBD.GetData(requete);
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    Console.WriteLine($"{ int.Parse(row["idmateriel"].ToString())} { int.Parse(row["idcategorie"].ToString())} { (String)row["nommateriel"]} { (String)row["referenceconstructeurmateriel"]} { (String)row["codebarreinventaire"]}");
                }
            }
        }

        public void Update()
        {
            DataAccess accesBD = new DataAccess();
            String requete = $"select * from materiel;";
            DataTable datas = accesBD.GetData(requete);
        }
    }
}
