using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Projet2_BoVoyage_VL_AP.Controller;
using Projet2_BoVoyage_VL_AP.Model;

namespace Projet2_BoVoyage_VL_AP.View
{
    class Affichage
    {

        public Affichage()
        {

            
        }


        public static void ChoixMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("\r\n\t***********************************************");
            Console.WriteLine("\tMENU | A quelle interface voulez-vous accéder ?");
            Console.WriteLine("\t     | Voyage, Client, Deconnexion, Quitter\r\n");
        }

        public static void Actions(string menu)
        {
            string suppr = "";

            if (menu.ToUpper() == "VOYAGE") { suppr = "Supprimer, "; }
            else { suppr = ""; }

            Console.WriteLine("");
            Console.WriteLine("\r\n\t***********************************************");
            Console.WriteLine("\t" + menu.ToUpper() + " | Que souhaitez-vous faire ?");
            Console.WriteLine("\t       | Rechercher, Modifier, Ajouter, " + suppr + "Retour Menu, Deconnexion, Quitter\r\n");

        }

        public static void Rechercher(string menu)
        {
            Console.WriteLine("\r\n\tQuel " + menu + " souhaitez-vous rechercher ?");
        }

        
        public static void CommandeInconnue()
        {
            Console.WriteLine("\r\n\tJe n'ai pas compris la commande.");
        }

        public static void CheminDossier()
        {
            Console.WriteLine("\r\n\tDossier Actuel:\r\n\t" + Environment.CurrentDirectory);
        }

        public static void Erreur(Exception e)
        {
            Console.WriteLine("\r\n\tCode d'erreur :\r\n\t" + e);
        }

        public static void ErreurCustom(string raison)
        {
            Console.WriteLine("\r\n\tUn probleme de " + raison + " est survenu.");
        }

        public static void Supprimer(string menu)
        {
            Console.WriteLine("\r\n\tQuel " + menu + " souhaitez-vous supprimer ? (entrer son identifiant)");
        }

        public static void Valider(string cela)
        {
            Console.WriteLine("\r\n\tEtes-vous sûrs de vouloir supprimer :\r\n" +
                "\r\n\t" + cela + 
                "\r\n\t(oui / non)");
        }

        public static void Requete()
        {
            Console.WriteLine("\r\n\tQuelle requete SQL souhaitez-vous envoyer à la base de données ?");
        }

        public static void Reponse()
        {
            Console.WriteLine("\r\n\tQuelle reponse souhaitez-vous recevoir de la base de données (noms colonnes à afficher séparés d'un ';')?");
        }

        public static void Connexion()
        {
            Console.WriteLine("");
            Console.WriteLine("\r\n\t***********************************************");
            Console.WriteLine("\tAUTH | Veuillez entrer vos identifiants");
            Console.WriteLine("\t     | Identifiant puis Mot de Passe :");
        }

        public static void Deconnexion()
        {
            Console.WriteLine("\r\n\tVous êtes déconnecté");
        }

        public static void Ajouter(string menu)
        {
            Console.WriteLine("\r\n\tVeuillez entrer une par une les valeurs du " + menu + " à ajouter.");
        }

        public static void Modifier(string menu)
        {
            Console.WriteLine("\r\n\tQuel " + menu + " voulez-vous modifier ?");
        }

        public static void ModifierChamps()
        {
            Console.WriteLine("\r\n\tQuels champs voulez-vous modifier (séparés d'un ';') ?");
        }

        public static void ShowDataSet(DataSet ds)
        {
            if (ds.Tables["Resultat"].Rows.Count > 0)
            {
                foreach (DataRow ligne in ds.Tables["Resultat"].Rows)
                {
                    Console.WriteLine("\t " + ligne["nom"] + ", " + ligne["prenom"]);
                }
            }
            else
            {
                Console.WriteLine("aucune ligne à afficher");
            }
        }

        public static void ShowResult(int combien)
        {
            Console.WriteLine("\trésultat de la procédure = {0} lignes concernées", combien.ToString());
        }

    }
}
