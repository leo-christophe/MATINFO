﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net.Mail;
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

        private ObservableCollection<Attributions> sesAttributions;

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

        /// <summary>
        /// Constructeur dont l'ID personnel est automatiquement généré.
        /// </summary>
        /// <param name="nomPersonnel"></param>
        /// <param name="prenomPersonnel"></param>
        /// <param name="emailPersonnel"></param>
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
                if (value > 0 && !string.IsNullOrEmpty(value.ToString()))
                    idPersonnel = value;
                else
                    throw new ArgumentException("l'ID du personnel doit être un entier non null ou vide > 0");
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
                // vérification du format du mail
                bool mailcorrect = true;
                try { 
                    // essai création mail
                    MailAddress m = new MailAddress(value);
                    }
                catch (FormatException) // erreur de format
                    {
                    // le format du mail est alors incorrect
                    mailcorrect = false;
                    }
                
                // une autre solution : Regex verifEmail = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
                if (mailcorrect && !string.IsNullOrEmpty(value) && value.Length <= 30 && value.GetType() == typeof(string))
                    emailPersonnel = value;
                else
                    throw new ArgumentException("Le mail du personnel doit être une chaine de caractère non nulle de longueur inférieur à 30 de format correct.");
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
                if (value.GetType() == typeof(ObservableCollection<Attributions>) && value != null)
                    sesAttributions = value;
                else
                    throw new ArgumentException("sesAttributions doit être non null et de type ObservableCollection<Attributions>!");
            }
        }

        /// <summary>
        /// ToString permet de récupérer les propriétés de l'objet en chaîne de caractère.
        /// </summary>
        /// <returns>Une chaîne de caractère composé de l'ID personnel, du nom personnel du prénom personnel et de l'email du personnel.</returns>
        public override string? ToString()
        {
            return $"{this.IdPersonnel} \n{this.NomPersonnel} \n{this.PrenomPersonnel} \n{this.EmailPersonnel}";
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// CalculNouvelId permet de récupérer un nouvel ID inexistant grâce à une requête SQL. Ce nouvel ID permet d'ajouter un personnel 
        /// à partir de l'ID retourné, bien que l'insertion dans la base de donnée génère un nouvel ID à chaque fois; Ce nouvel ID sert surtout
        /// à l'objet / la list lors de la création.
        /// </summary>
        /// <returns>Un nouvel ID inexistant dans la base est retourné.</returns>
        public int CalculerNouvelId()
        {
            // Accès à la base de données
            DataAccess accesBD = new DataAccess();
            // Requête
            String requete = "SELECT MAX(idPersonnel) + 1 AS \"E\" FROM personnel;";
            // Envoi de la requête
            DataTable datas = accesBD.GetData(requete);

            // Récupération du nouvel ID
            if (datas != null && datas.Rows.Count > 0)
                return int.Parse(datas.Rows[0]["E"].ToString());
            else
                return 1;
        }

        /// <summary>
        /// La méthode Create permet d'insérer un nouveau personnel dans la table personnel. Elle envoie une requête SQL, à partir de l'ID Personnel,
        /// le nom du personnel, l'email du personnel et le prénom du personnel. 
        /// </summary>
        public void Create()
        {
            // Accès à la base de données
            DataAccess accesBD = new DataAccess();
            // Requête
            String requete = $"INSERT INTO personnel (idpersonnel, emailpersonnel, nompersonnel, prenompersonnel) VALUES ({this.IdPersonnel}, '{this.EmailPersonnel}', '{this.NomPersonnel}', '{this.PrenomPersonnel}');";
            // Envoi de la requête
            int datas = accesBD.SetData(requete);
        }

        /// <summary>
        ///  La méthode Delete permet de supprimer l'objet correspondant à partir de son ID Personnel. Une requête SQL est envoyé à la table personnel pour
        ///  que l'enregistrement correspondant à l'objet dans l'applicationData soit supprimé.
        /// </summary>
        public void Delete()
        {
            // Connexion BD
            DataAccess accesBD = new DataAccess();
            // Requete
            String requete = "DELETE FROM personnel WHERE idpersonnel=" + IdPersonnel + ";";
            // Envoi de la requête
            DataTable datas = accesBD.GetData(requete);

        }

        /// <summary>
        /// La méthode FindAll permet de retourner une collection observable composé de tous les enregistrement de la table personnel.
        /// </summary>
        /// <returns>Une collection observable de personnel composé de tout les enregistrements de la table personnel.</returns>
        public ObservableCollection<Personnel> FindAll()
        {
            ObservableCollection<Personnel> lesPersonnels = new ObservableCollection<Personnel>();
            // Connexion BD
            DataAccess accesBD = new DataAccess();
            // Requête
            String requete = "select idpersonnel, nompersonnel, prenompersonnel, emailpersonnel from personnel;";
            // Récupération des données à partir de la requête
            DataTable datas = accesBD.GetData(requete);
            
            // Formation des données
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
            // Accès à la base de données
            DataAccess accesBD = new DataAccess();
            // La requête
            String requete = $"select idPersonnel, emailPersonnel from personnel where emailPersonnel = '{this.EmailPersonnel}';";

            // Récupération des données
            DataTable datas = accesBD.GetData(requete);

            // Si la data n'est pas nulle et vide, alors c'est qu'il existe déjà un personnel avec cet id.
            if (datas != null && datas.Rows.Count > 0)
                return true;
            // existe

            // n'existe pasg
            return false;
        }
        
        public void Update()
        {
            DataAccess accesBD = new DataAccess();
            String requete = $"UPDATE Personnel SET nomPersonnel = '{this.NomPersonnel}', prenomPersonnel = '{this.PrenomPersonnel}', emailPersonnel = '{this.EmailPersonnel}' WHERE idPersonnel = {this.idPersonnel};";
            int datas = accesBD.SetData(requete);
        }
    }
}
