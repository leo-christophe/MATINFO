using Microsoft.VisualStudio.TestTools.UnitTesting;
using MATINFO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MATINFO.Model.Tests
{
    [TestClass()]
    public class PersonnelTests
    {
        private Personnel p1;

        private Personnel pVide1;
        private Personnel pVide2;
        private Personnel pVide3;

        private Personnel pMail1;
        private Personnel pMail2;
        private Personnel pMail3;

        private Personnel pLong1;
        private Personnel pLong2;
        private Personnel pLong3;

        [TestInitialize()]
        public void init()
        {
            p1 = new Personnel("Jean", "Franc", "unmail@gmail.com");
        }
        [TestMethod()]
        public void PersonnelTest()
        {
            Assert.IsTrue(p1.NomPersonnel == "Jean" && p1.PrenomPersonnel == "Franc" && p1.EmailPersonnel == "unmail@gmail.com");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException),
        "Nom vide")]
        public void PersonnelTest1()
        {
            Personnel pVide1 = new Personnel("", "Franc", "unmail@gmail.com");
            Personnel pVide2 = new Personnel("Jean", "", "unmail@gmail.com");
            Personnel pVide3 = new Personnel("Jean", "Franc", "");

            Personnel pMail1 = new Personnel("Jean", "Franc", "@gmail.com");
            Personnel pMail2 = new Personnel("Jean", "Franc", "unmail@.com");
            Personnel pMail3 = new Personnel("Jean", "Franc", "unmail@gmail");

            Personnel pLong1 = new Personnel("Jeanisfiusdfhsdifhsdsdgfdsfdsfdsfdsfdsf", "Franc", "unmail@gmail");
            Personnel pLong2 = new Personnel("Jean", "Francqsdqsdqsdqsdqsdqsdqsdsdqdqsdqsdqsdsqdqsd", "unmail@gmail");
            Personnel pLong3 = new Personnel("Jean", "Franc", "unmail@gmailqsdqsdqsdsqdsqdsqdqsdqsdsqdqsdqsdsqdqsdqsdqsdsqdqsdqsdqsdqsdqsdqsdd.fr");
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CalculerNouvelIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FindAllTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FindBySelectionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Assert.Fail();
        }
    }
}