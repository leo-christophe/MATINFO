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
        private Personnel p2;

        [TestInitialize()]
        public void init()
        {
            p1 = new Personnel("Jean", "Franc", "unmail@gmail.com");
            p2 = new Personnel(1000, "Jean", "Franc", "unmail@gmail.com");
        }
        [TestMethod()]
        public void PersonnelTest()
        {
            Assert.IsTrue(p1.NomPersonnel == "Jean" && p1.PrenomPersonnel == "Franc" && p1.EmailPersonnel == "unmail@gmail.com");
            Assert.IsTrue(p2.IdPersonnel == 1000 && p2.NomPersonnel == "Jean" && p1.PrenomPersonnel == "Franc" && p1.EmailPersonnel == "unmail@gmail.com");
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException),
        "Nom vide ou mail mauvais format ou trop long")]
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


            Personnel pVide4 = new Personnel(1000, "", "Franc", "unmail@gmail.com");
            Personnel pVide5 = new Personnel(1000, "Jean", "", "unmail@gmail.com");
            Personnel pVide6 = new Personnel(1000, "Jean", "Franc", "");

            Personnel pMail4 = new Personnel(1000, "Jean", "Franc", "@gmail.com");
            Personnel pMail5 = new Personnel(1000, "Jean", "Franc", "unmail@.com");
            Personnel pMail6 = new Personnel(1000, "Jean", "Franc", "unmail@gmail");

            Personnel pLong4 = new Personnel(1000, "Jeanisfiusdfhsdifhsdsdgfdsfdsfdsfdsfdsf", "Franc", "unmail@gmail");
            Personnel pLong5 = new Personnel(1000, "Jean", "Francqsdqsdqsdqsdqsdqsdqsdsdqdqsdqsdqsdsqdqsd", "unmail@gmail");
            Personnel pLong6 = new Personnel(1000, "Jean", "Franc", "unmail@gmailqsdqsdqsdsqdsqdsqdqsdqsdsqdqsdqsdsqdqsdqsdqsdsqdqsdqsdqsdqsdqsdqsdd.fr");

            Personnel pId1 = new Personnel(0, "Jean", "Franc", "unmail@gmail.com");
            Personnel pId2 = new Personnel(-1, "Jean", "Franc", "unmail@gmail.com");
        }
    }
}