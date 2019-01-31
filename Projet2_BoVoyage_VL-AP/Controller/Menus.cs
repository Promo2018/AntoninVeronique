using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet2_BoVoyage_VL_AP.View;
using Projet2_BoVoyage_VL_AP.Model;

namespace Projet2_BoVoyage_VL_AP.Controller
{
    class Menus
    {

        private string menu;
        private string action;
        private string actionEtMenu;


        public Menus()
        {

            //auth.Connexion();

        }

        public string Menu { get => menu; set => menu = value; }
        public string Action { get => action; set => action = value; }
        public string ActionEtMenu { get => actionEtMenu; set => actionEtMenu = value; }
        

        Authentification auth = new Authentification();

        Voyage voyage = new Voyage();
        Participant participant = new Participant();
        DossierReservation dossier = new DossierReservation();

        
        public void SwitchMenus()
        {

            ActionEtMenu = "";
            ActionEtMenu = Action + Menu;


            switch (ActionEtMenu.Trim().ToUpper())
            {

                case (""):
                    Affichage.ChoixMenu();
                    Menu = Console.ReadLine();
                    break;


                case ("DOSSIER"):
                    Affichage.Actions(Menu);
                    Action = Console.ReadLine();
                    break;

                case ("VOYAGE"):
                    Affichage.Actions(Menu);
                    Action = Console.ReadLine();
                    break;

                case ("CLIENT"):
                    Affichage.Actions(Menu);
                    Action = Console.ReadLine();
                    break;

                    
                case ("RECHERCHER" + "DOSSIER"):

                    //AMELIORATIONS : rechercher grâce à des ".Contains()" pour recherche puissante.

                    Affichage.Question(Menu, Action);

                    dossier.Rechercher();

                    Action = "";

                    break;


                case ("VERIF SOLVABILITE" + "DOSSIER"):

                    Affichage.Question(Menu, Action);

                    dossier.ValiderSolvabilite();

                    Action = "";

                    break;


                case ("ACCEPTER" + "DOSSIER"):

                    Affichage.Question(Menu, Action);

                    dossier.Accepter();

                    Action = "";

                    break;


                case ("ANNULER" + "DOSSIER"):

                    Affichage.Question(Menu, Action);

                    dossier.Annuler("raison");

                    Action = "";

                    break;


                    
                case ("RECHERCHER" + "VOYAGE"):

                    //AMELIORATIONS : rechercher grâce à des ".Contains()" pour recherche puissante.

                    Affichage.Question(Menu, Action);

                    voyage.Rechercher();

                    Action = "";

                    break;
                    

                case ("MODIFIER" + "VOYAGE"):

                    Affichage.Question(Menu, Action);

                    voyage.Modifier();

                    Action = "";

                    break;

                    

                case ("AJOUTER" + "VOYAGE"):

                    //AMELIORATIONS : prendre en compte Age (automatique) et non adresse mail si client = false.
                    //AMELIORATIONS : selectionner destinations sans utiliser seulement l'ID destination.
                    //AMELIORATIONS : utiliser la methode suivante :
                    //Voyage_ADO nouveau = new Voyage_ADO(4, "Toto", "titi", 2000, 500, "Mécanicien");
                    //quel.create(nouveau);

                    Affichage.Question(Menu, Action);

                    voyage.Ajouter();

                    Action = "";


                    break;


                case ("SUPPRIMER" + "VOYAGE"):

                    Affichage.Question(Menu, Action);

                    voyage.Supprimer();

                    Action = "";

                    break;




                case ("RECHERCHER" + "CLIENT"):

                    //AMELIORATIONS : rechercher grâce à des "Contain()" pour recherche puissante.

                    Affichage.Question(Menu, Action);

                    participant.Rechercher();

                    Action = "";

                    break;


                    
                case ("MODIFIER" + "CLIENT"):

                    Affichage.Question(Menu, Action);

                    participant.Modifier();

                    Action = "";

                    break;
                    


                case ("AJOUTER" + "CLIENT"):

                    //AMELIORATIONS : prendre en compte Age (automatique) et non adresse mail si client = false.
                    //AMELIORATIONS : utiliser la methode suivante :
                    //Participant_ADO nouveau = new Participant_ADO(4, "Toto", "titi", 2000, 500, "Mécanicien");
                    //quel.create(nouveau);

                    Affichage.Question(Menu, Action);

                    participant.Ajouter();

                    Action = "";

                    break;


                case ("RETOUR MENU" + "DOSSIER"):
                    Menu = "";
                    Action = "";
                    break;
                case ("RETOUR MENU" + "CLIENT"):
                    Menu = "";
                    Action = "";
                    break;
                case ("RETOUR MENU" + "VOYAGE"):
                    Menu = "";
                    Action = "";
                    break;

                  
                case ("DECONNEXION"):
                    auth.Connexion();
                    Menu = "";
                    Action = "";
                    break;
                case ("DECONNEXION" + "DOSSIER"):
                    auth.Connexion();
                    Menu = "";
                    Action = "";
                    break;
                case ("DECONNEXION" + "VOYAGE"):
                    auth.Connexion();
                    Menu = "";
                    Action = "";
                    break;
                case ("DECONNEXION" + "CLIENT"):
                    auth.Connexion();
                    Menu = "";
                    Action = "";
                    break;
                   

                case ("QUITTER"):
                    Quitter(3);
                    break;
                case ("QUITTER" + "DOSSIER"):
                    Quitter(4);
                    break;
                case ("QUITTER" + "VOYAGE"):
                    Quitter(5);
                    break;
                case ("QUITTER" + "CLIENT"):
                    Quitter(6);
                    break;


                default:
                    Affichage.CommandeInconnue();
                    Menu = "";
                    Action = "";
                    break;


                case ("TEST"):


                    Menu = "";
                    Action = "";

                    break;
                    
            }

            SwitchMenus();

        }

        public static void Quitter(int code)
        {
            Environment.Exit(code);
        }

    }
}
