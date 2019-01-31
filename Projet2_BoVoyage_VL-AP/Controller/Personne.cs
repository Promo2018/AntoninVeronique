using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet2_BoVoyage_VL_AP.View;

namespace Projet2_BoVoyage_VL_AP.Controller
{
    class Personne
    {

        public Personne()
        {


        }

        protected string civilite;
        protected string nom;
        protected string prenom;
        protected string adresse;
        protected string telephone;
        protected DateTime dateNaissance;
        protected string age;

        public string Civilite { get => civilite; set => civilite = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public DateTime DateNaissance { get => dateNaissance; set => dateNaissance = value; }
        public string Age { get => age; }

        /*{
            get
            {
                int age = DateTime.Now.Year - DateNaissance.Year - (DateTime.Now.Month < DateNaissance.Month ? 1 : DateTime.Now.Day < DateNaissance.Day ? 1 : 0);
                return age;
            }
        }
        

        public void TestAge()
        {
            Console.WriteLine("\r\n\tVeuillez entrer une date de naissance :");
            DateNaissance = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("\r\n\tL'âge de la personne est " + Age + " ans.");

        }
        */

    }
}
