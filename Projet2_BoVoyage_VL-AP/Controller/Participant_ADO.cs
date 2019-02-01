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
            return ("\r\n\tIDParticipant= " + NumeroSequentiel + "; Client?= " + Client + "; " + Civilite + "; Prenom= " + Prenom + "; Nom= " + Nom + "; Adresse= " + 
                Adresse + "; Tel= " + Telephone + "; DateNaissance= " + DateNaissance.ToShortDateString() + "; Age= " + Age + "ans; Email= " + Email);
        }


        public void RechercheChampsID()
        {

            try
            {
                NumeroSequentiel = Int32.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Affichage.Erreur(e);
            }

        }


        public void RechercheChampsGeneral()
        {

            try
            {
                string recherche = Console.ReadLine();

                bool successInt = Int32.TryParse(recherche, out int num);
                bool successDat = DateTime.TryParse(recherche, out DateTime dat);
                bool successBoo = Boolean.TryParse(recherche, out bool boo);

                Civilite = recherche.ToString();
                Nom = recherche.ToString();
                Prenom = recherche.ToString();
                Adresse = recherche.ToString();
                Telephone = recherche.ToString();
                Age = recherche.ToString();
                Email = recherche.ToString();

                if (successInt) { NumeroSequentiel = num; }
                else { NumeroSequentiel = -1; }
                if (successDat) { DateNaissance = dat; }
                else { DateNaissance = DateTime.Parse("01/01/1900"); }
                if (successBoo) { Client = boo; }
                else { Client = null; }

            }
            catch (Exception e)
            {
                Affichage.Erreur(e);
            }

        }

    }
}
