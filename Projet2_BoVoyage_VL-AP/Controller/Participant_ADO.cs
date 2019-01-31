using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet2_BoVoyage_VL_AP.View;
using Projet2_BoVoyage_VL_AP.Model;

namespace Projet2_BoVoyage_VL_AP.Controller
{
    class Participant_ADO : Personne
    {

        private int numeroSequentiel;
        private float reduction;

        private bool? client;
        private string email;


        public Participant_ADO()
        {

            numeroSequentiel = -1;
            civilite = null;
            nom = null;
            prenom = null;
            adresse = null;
            telephone = null;
            dateNaissance = DateTime.Parse("01/01/1990");
            age = null;
            client = null;
            email = null;

        }

        public Participant_ADO(
            int numeroSequentiel, string civilite, string nom, string prenom, 
            string adresse, string telephone, DateTime dateNaissance, string age, bool? client, string email
            )
        {

            NumeroSequentiel = numeroSequentiel;
            Civilite = civilite;
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            Telephone = telephone;
            DateNaissance = dateNaissance;
            Age = age;
            Client = client;
            Email = email;
            
        }


        public int NumeroSequentiel { get => numeroSequentiel; set => numeroSequentiel = value; }
        public float Reduction { get => reduction; set => reduction = value; }
        public bool? Client { get => client; set => client = value; }
        public string Email { get => email; set => email = value; }


        public string AfficherChamps()
        {
            return (Civilite + "; " + Prenom + "; " + Nom + "; " + Adresse + "; " + 
                Telephone + "; " + DateNaissance + "; " + Age + "; " + Client + "; " + Email);
        }


        public void RechercheChamps()
        {
            try
            { 
                NumeroSequentiel = Int32.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                NumeroSequentiel = 0;
            }


            /*
            string recherche = Console.ReadLine();

            if (Civilite.Contains(recherche)) { Civilite = recherche; }
            if (Nom.Contains(recherche)) { Nom = recherche; }
            if (Prenom.Contains(recherche)) { Prenom = recherche; }
            if (Adresse.Contains(recherche)) { Adresse = recherche; }
            if (Telephone.Contains(recherche)) { Telephone = recherche; }
            if (DateNaissance.ToString().Contains(recherche)) { DateNaissance = DateTime.Parse(recherche); }
            if (Age.Contains(recherche)) { Age = recherche; }
            if (Client.ToString().Contains(recherche)) { Client = Convert.ToBoolean(recherche); }
            if (Email.Contains(recherche)) { Email = recherche; }

            */
        }



    }
}
