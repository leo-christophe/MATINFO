using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MATINFO.Model
{
    public class Materiel
    {
        private int idMateriel;
        private int FK_idCategorie;
        private string ?nomMateriel;
        private string ?referenceConstructeur;
        private string ?codeBarreInventaire;

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

        public int FK_idCategorie1
        {
            get
            {
                return FK_idCategorie;
            }

            set
            {
                FK_idCategorie = value;
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
    }
}
