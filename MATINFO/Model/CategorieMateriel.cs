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

        /// <summary>
        /// Constructeur de CategorieMateriel avec l'ID automatiquement calculé.
        /// </summary>
        /// <param name="nomCategorie">Juste le nom de la catégorie est nécéssaire. L'ID est calculé automatiquement.</param>
        public CategorieMateriel(string nomCategorie)
        {
            this.IdCategorie = this.CalculNouvelId();
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
                if (!string.IsNullOrEmpty(value.ToString()) && value.GetType() == typeof(string) && value.Length <= 50)
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

        /// <summary>
        /// ToString permet de récupérer les propriétés de l'objet en chaîne de caractère.
        /// </summary>
        /// <returns>Une chaîne de caractère composé de l'ID catégorie et du Nom de la catégorie.</returns>
        public override string? ToString()
        {
            return $"{this.IdCategorie} \n{this.NomCategorie}";
        }

        /// <summary>
        /// Equals permet de vérifier l'égalité entre deux objets categorieMateriel (ID et le nom)
        /// </summary>
        /// <param name="obj">Le deuxième objet à vérifier l'égalité</param>
        /// <returns>true si égal, false sinon</returns>
        public override bool Equals(object? obj)
        {
            return obj is CategorieMateriel materiel &&
                   this.IdCategorie == materiel.IdCategorie &&
                   this.NomCategorie == materiel.NomCategorie;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.IdCategorie, this.NomCategorie);
        }


        /// <summary>
        /// CalculNouvelID permet de récupérer un nouvel ID inexistant grâce à une requête SQL. Ce nouvel ID permet d'ajouter une catégorie 
        /// à partir de l'ID retourné, bien que l'insertion dans la base de donnée génère un nouvel ID à chaque fois; Ce nouvel ID sert surtout
        /// à l'objet / la list lors de la création.
        /// </summary>
        /// <returns>Un nouvel ID inexistant dans la base est retourné.</returns>
        public int CalculNouvelId()
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

        /// <summary>
        /// La méthode Create permet d'insérer une nouvelle catégorie dans la table categorie_materiel. Elle envoie une requête SQL, à partir du nom de la catégorie,
        /// L'ID de la catégorie est généré automatiquement lors de l'insertion dans la table.
        /// </summary>
        public void Create()
        {
            // Accès à la base de données
            DataAccess accesBD = new DataAccess();
            // Requête : l'ID est généré automatiquement lors de l'insertion dans la base.
            String requete = $"INSERT INTO CATEGORIE_MATERIEL (nomCategorie) VALUES ('{this.NomCategorie}');";
            // Envoie de la requête
            int datas = accesBD.SetData(requete);
        }

        /// <summary>
        /// La méthode Delete permet de supprimer la catégorie dans la base de données correspondant à l'objet à partir de l'ID catégorie.
        /// </summary>
        public void Delete()
        {
            // Accès à la base de données
            DataAccess accesBD = new DataAccess();
            // Requête de suppression
            String requete = $"DELETE FROM CATEGORIE_MATERIEL WHERE idCategorie = {this.IdCategorie};";
            // Envoi de la requête
            accesBD.GetData(requete);
        }

        /// <summary>
        /// La méthode FindAll permet de retourner une collection observable composé de tous les enregistrement de la table catégorie_materiel.
        /// </summary>
        /// <returns>Une collection observable de categorieMateriel composé de tout les enregistrements de la table categorie_Materiel.</returns>
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

        /// <summary>
        /// FindBySelection permet d'affiner les résultats d'une recherche complète comme FindAll. Elle est composées au minimum d'une clause (WHERE, HAVE, ORDER...) 
        /// étant l'argument criteres : une chaîne de caractères. 
        /// </summary>
        /// <param name="criteres">criteres est une chaîne de caractères composé de clauses SQL positionné après SELECT et FROM. </param>
        /// <returns>La sélection demandée sous la forme d'une collection observable de Categoriemateriel.</returns>
        public ObservableCollection<CategorieMateriel> FindBySelection(string criteres)
        {
            ObservableCollection<CategorieMateriel> lesCategories = new ObservableCollection<CategorieMateriel>();
            // Accès à la BD
            DataAccess accesBD = new DataAccess();
            // Requête
            String requete = $"select * from categorie_Materiel {criteres};";
            // Envoi de la requête
            DataTable datas = accesBD.GetData(requete);

            // Formation des données dans une collection observable
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    CategorieMateriel categorie = new CategorieMateriel(
                         int.Parse(row["idCategorie"].ToString()),
                        (String)row["nomCategorie"]
                        );
                    lesCategories.Add(categorie);
                }
            }
            return lesCategories;
        }

        /// <summary>
        /// La méthode READ permet de savoir si une catégorie existe déjà selon son ID. 
        /// </summary>
        /// <returns> Un booléen est retourné. true si la catégorie existe déjà, false sinon.</returns>
        public bool Read()
        {
            DataAccess accesBD = new DataAccess();
            String requete = $"SELECT idCategorie FROM categorie_materiel WHERE idCategorie = {this.IdCategorie}";
            // Récupération des données
            DataTable datas = accesBD.GetData(requete);

            // Si la data n'est pas nulle et vide, alors c'est qu'il existe déjà une catégorie avec cet ID.
            if (datas != null && datas.Rows.Count > 0)
                return true;
                // existe

            // n'existe pas
            return false;
        }

        /// <summary>
        /// La méthode UPDATE permet de mettre à jour la catégorie dans la base de donnée à partir des propriétés de l'objet. On prend l'unique propriété modifiable de la table 
        /// NomCategorie et on redéfinit l'attribut correspondant dans la table. La catégorie correspondante à l'objet dans la table est retrouvé grace à l'ID catégorie de l'objet.
        /// </summary>
        public void Update()
        {
            // Accès à la base de données
            DataAccess accesBD = new DataAccess();
            // La requête
            String requete = $"" +
                $"UPDATE CATEGORIE_MATERIEL" +
                $" SET NomCategorie = '{this.NomCategorie}'" +
                $" WHERE IdCategorie = {this.IdCategorie};";
            // Envoi de la requête
            int datas = accesBD.SetData(requete);
        }
    }
}
