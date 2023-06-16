using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MATINFO.Model
{
    public class Attributions : Crud<Attributions>
    {
        private DateTime dateAttribution;
        private String commentaire;
        private int FK_idMateriel;
        private int FK_idPersonnel;

        private Materiel AMateriel;
        private Personnel APersonnel;

        public Attributions(DateTime dateAttribution, string commentaire, int fK_idMateriel1, int fK_idPersonnel1, Materiel aMateriel1, Personnel aPersonnel1)
        {
            this.DateAttribution = dateAttribution;
            this.Commentaire = commentaire;
            this.FK_idMateriel1 = fK_idMateriel1;
            this.FK_idPersonnel1 = fK_idPersonnel1;
            this.AMateriel1 = aMateriel1;
            this.APersonnel1 = aPersonnel1;
        }

        public DateTime DateAttribution
        {
            get
            {
                return dateAttribution;
            }

            set
            {
                dateAttribution = value;
            }
        }

        public string Commentaire
        {
            get
            {
                return commentaire;
            }

            set
            {
                commentaire = value;
            }
        }

        public int FK_idMateriel1
        {
            get
            {
                return FK_idMateriel;
            }

            set
            {
                FK_idMateriel = value;
            }
        }

        public int FK_idPersonnel1
        {
            get
            {
                return FK_idPersonnel;
            }

            set
            {
                FK_idPersonnel = value;
            }
        }

        public Materiel AMateriel1
        {
            get
            {
                return AMateriel;
            }

            set
            {
                AMateriel = value;
            }
        }

        public Personnel APersonnel1
        {
            get
            {
                return APersonnel;
            }

            set
            {
                APersonnel = value;
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

        public ObservableCollection<Attributions> FindAll()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Attributions> FindBySelection(string criteres)
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
