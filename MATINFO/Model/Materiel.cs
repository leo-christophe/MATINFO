using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

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
        private ObservableCollection<Attributions> sesAttributions;

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

        /// <summary>
        /// Constructeur générant l'ID du matériel automatiquement.
        /// </summary>
        /// <param name="fK_idCategorie">Id de la catégorie</param>
        /// <param name="nomMateriel">Nom du matériel</param>
        /// <param name="referenceConstructeur">La référence du constructeur du matériel</param>
        /// <param name="codeBarreInventaire">Le code barre unique du matériel.</param>
        public Materiel(int fK_idCategorie, string nomMateriel, string referenceConstructeur, string codeBarreInventaire)
        {
            this.FK_idCategorie = fK_idCategorie;
            this.NomMateriel = nomMateriel;
            this.ReferenceConstructeur = referenceConstructeur;
            this.CodeBarreInventaire = codeBarreInventaire;

            this.IdMateriel = this.CalculNouvelId();
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
                if  (!String.IsNullOrEmpty(value) && value.GetType() == typeof(string) && value.Length <= 100)
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

        public ObservableCollection<Attributions> SesAttributions
        {
            get
            {
                return sesAttributions;
            }

            set
            {
                sesAttributions = value;
            }
        }

        /// <summary>
        /// ToString permet de récupérer les propriétés de l'objet en chaîne de caractère.
        /// </summary>
        /// <returns>Une chaîne de caractère composé de l'ID matériel, ID catégorie, nom matériel, nom catégorie, référence constructeur et code barre.</returns>
        public override string? ToString()
        {
            return $"{this.IdMateriel} \n{this.NomMateriel} \n{this.FK_idCategorie} \n{this.UneCategorieM.NomCategorie} \n{this.ReferenceConstructeur} \n{this.CodeBarreInventaire}";
        }

        /// <summary>
        /// Equals permet de vérifier l'égalité entre deux objets materiel (ID Materiel, ID categorie, Nom, Reference, Code Barre)
        /// </summary>
        /// <param name="obj">Le deuxième objet à vérifier l'égalité</param>
        /// <returns>true si égal, false sinon</returns>
        public override bool Equals(object? obj)
        {
            return obj is Materiel materiel &&
                   this.IdMateriel == materiel.IdMateriel &&
                   this.FK_idCategorie == materiel.FK_idCategorie &&
                   this.NomMateriel == materiel.NomMateriel &&
                   this.ReferenceConstructeur == materiel.ReferenceConstructeur &&
                   this.CodeBarreInventaire == materiel.CodeBarreInventaire &&
                   EqualityComparer<ObservableCollection<Attributions>>.Default.Equals(this.SesAttributions, materiel.SesAttributions);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.IdMateriel, this.FK_idCategorie, this.NomMateriel, this.ReferenceConstructeur, this.CodeBarreInventaire, this.SesAttributions);
        }


        /// <summary>
        /// CalculNouvelId permet de récupérer un nouvel ID inexistant grâce à une requête SQL. Ce nouvel ID permet d'ajouter un matériel 
        /// à partir de l'ID retourné, bien que l'insertion dans la base de donnée génère un nouvel ID à chaque fois; Ce nouvel ID sert surtout
        /// à l'objet / la list lors de la création.
        /// </summary>
        /// <returns>Un nouvel ID inexistant dans la base est retourné.</returns>
        public int CalculNouvelId()
        {
            // Accès à la base de données
            DataAccess accesBD = new DataAccess();

            // On récupère l'ID max et on rajoute 1 pour avoir un nouvel id. On le renomme en E pour pouvoir le récupérer.
            String requete = "SELECT MAX(idMateriel) + 1 AS \"E\" FROM materiel;";
            DataTable datas = accesBD.GetData(requete);
            if (datas != null)
                // On récupère la ligne 1 (la seule puisque l'ID est unique) et on prend la colonne E : le maximum
                return int.Parse(datas.Rows[0]["E"].ToString());
            else
                return 1;
        }

        /// <summary>
        /// La méthode Create permet d'insérer un nouveau matériel dans la table materieL. Elle envoie une requête SQL, à partir de l'ID Catégorie,
        /// le nom du matériel, la référence du constructeur et le code barre. L'ID matériel est généré automatiquement lors de l'insertion dans la table.
        /// </summary>
        public void Create()
        {
            // Accès à la base de données
            DataAccess accesBD = new DataAccess();

            // Requête
            String requete = $"" +
                $"INSERT INTO MATERIEL " +
                $"(IdCategorie, NomMateriel, ReferenceConstructeurMateriel, CodeBarreInventaire) " +
                $"VALUES " +
                $"({this.FK_idCategorie}, '{this.NomMateriel}', '{this.ReferenceConstructeur}', '{this.CodeBarreInventaire}');";

            // Envoie de la requête
            int datas = accesBD.SetData(requete);
        }

        /// <summary>
        ///  La méthode Delete permet de supprimer l'objet correspondant à partir de son ID MatérieL. Une requête SQL est envoyé à la table matériel pour
        ///  que l'enregistrement correspondant à l'objet dans l'applicationData soit supprimé.
        /// </summary>
        public void Delete()
        {
            // Accès à la base de données
            DataAccess accesBD = new DataAccess();
            
            // La requête
            String requete = $"DELETE FROM MATERIEL WHERE idMateriel = {this.IdMateriel};";
            
            // Envoie de la requête
            DataTable datas = accesBD.GetData(requete);
        }

        /// <summary>
        /// La méthode FindAll permet de retourner une collection observable composé de tous les enregistrement de la table matériel.
        /// </summary>
        /// <returns>Une collection observable de matériel composé de tout les enregistrements de la table matériel.</returns>
        public ObservableCollection<Materiel> FindAll()
        {
            ObservableCollection<Materiel> lesMateriaux = new ObservableCollection<Materiel>();
            DataAccess accesBD = new DataAccess();
            // Requête
            String requete = "select * from materiel ;";
            DataTable datas = accesBD.GetData(requete);
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    Materiel e = new Materiel(
                        int.Parse(row["idmateriel"].ToString()),
                        int.Parse(row["idcategorie"].ToString()),
                        (String)row["nommateriel"],
                        (String)row["refconstructeur"],
                        (String)row["codebarreinventaire"]
                        );
                    lesMateriaux.Add(e);
                }
            }
            return lesMateriaux;
        }

        /// <summary>
        /// FindBySelection permet d'affiner les résultats d'une recherche complète comme FindAll. Elle est composées au minimum d'une clause (WHERE, HAVE, ORDER...) 
        /// étant l'argument criteres : une chaîne de caractères. 
        /// </summary>
        /// <param name="criteres">criteres est une chaîne de caractères composé de clauses SQL positionné après SELECT et FROM. </param>
        /// <returns>La sélection demandée sous la forme d'une collection observable de matériel.</returns>
        public ObservableCollection<Materiel> FindBySelection(string criteres)
        {
            // création de la collection observable qu'on va retourner
            ObservableCollection<Materiel> lesMateriaux = new ObservableCollection<Materiel>();
            // Accès à la base de données
            DataAccess accesBD = new DataAccess();
            // Requête
            String requete = $"select * from materiel {criteres};";

            // Récupération des données
            DataTable datas = accesBD.GetData(requete);
            // Formation des données dans des objets Materiel.
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    Materiel materiel = new Materiel(
                        int.Parse(row["idmateriel"].ToString()),
                        int.Parse(row["idcategorie"].ToString()),
                        (String)row["nommateriel"],
                        (String)row["referenceconstructeurmateriel"],
                        (String)row["codebarreinventaire"]
                        );
                    lesMateriaux.Add(materiel);
                }
            }
            return lesMateriaux;
        }

        /// <summary>
        /// La méthode READ permet de savoir si un matériel existe déjà selon son code barre qui est unique. 
        /// </summary>
        /// <returns> Un booléen est retourné. true si le matériel existe, false sinon.</returns>
        public bool Read()
        {
            // Création de la collection observable de Materiel : lesMateriaux
            ObservableCollection<Materiel> lesMateriaux = new ObservableCollection<Materiel>();
            // Accès à la base de données
            DataAccess accesBD = new DataAccess();
            // La requête
            String requete = $"select idMateriel from materiel where codeBarreinventaire = {this.CodeBarreInventaire};";

            // Récupération des données
            DataTable datas = accesBD.GetData(requete);

            // Si la data n'est pas nulle et vide, alors c'est qu'il existe déjà un matériel avec ce code barre.
            if (datas != null && datas.Rows.Count > 0)
                return true;
                // existe

            // n'existe pas
            return false;
        }

        /// <summary>
        /// La méthode UPDATE permet de mettre à jour le matériel dans la base de donnée à partir des propriétés de l'objet. On prend chaque champ et on redéfinit chaque
        /// attribut de la table par les champs de l'objet. Le matériel correspondant à l'objet dans la table est retrouvé grace à l'ID matériel de l'objet.
        /// </summary>
        public void Update()
        {
            // Accès à la base de données
            DataAccess accesBD = new DataAccess();
            // Requête SQL
            String requete = $"" +
                $"UPDATE MATERIEL" +
                $" SET NomMateriel = '{this.NomMateriel}', " +
                $"      IdCategorie = {this.FK_idCategorie}, " +
                $"      ReferenceConstructeurMateriel = '{this.ReferenceConstructeur}', " +
                $"      CodeBarreInventaire = '{this.CodeBarreInventaire}' " +
                $" WHERE IdMateriel = {this.IdMateriel};";
            // MAJ des données
            int datas = accesBD.SetData(requete);
        }
    }
}
