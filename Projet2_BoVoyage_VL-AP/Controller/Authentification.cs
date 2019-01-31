using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet2_BoVoyage_VL_AP.View;

namespace Projet2_BoVoyage_VL_AP.Controller
{
    class Authentification
    {

        private string nomDeCompte;
        private string mdp;

        public string NomDeCompte { get => nomDeCompte; set => nomDeCompte = value; }
        public string Mdp { get => mdp; set => mdp = value; }


        public Authentification()
        {
            NomDeCompte = "";
            Mdp = "";

        }


        public void Connexion()
        {

            Affichage.Connexion();

            NomDeCompte = "";
            Mdp = "";

            NomDeCompte = Console.ReadLine().ToUpper();
            if (NomDeCompte.ToUpper() == "QUITTER") { Menus.Quitter(1); }

            Mdp = Console.ReadLine();

            if (Mdp.ToUpper() == "QUITTER") { Menus.Quitter(2); }
            else if (nomDeCompte == "ADMIN" && mdp == "admin")
            {

            }
            else
            {
                Console.WriteLine("Identifiants incorrects");
                Connexion();
            }
        }


    }
}
