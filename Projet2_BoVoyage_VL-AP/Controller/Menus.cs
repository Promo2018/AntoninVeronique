﻿using System;
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


                case ("VOYAGE"):
                    Affichage.Actions(Menu);
                    Action = Console.ReadLine();
                    break;

                case ("CLIENT"):
                    Affichage.Actions(Menu);
                    Action = Console.ReadLine();
                    break;

                    
                case ("RECHERCHER" + "VOYAGE"):

                    //AMELIORATIONS : rechercher grâce à des ".Contains()" pour recherche puissante.
                    
                    Affichage.Rechercher(Menu);

                    voyage.Rechercher();

                    Action = "";

                    break;
                    

                    
                case ("MODIFIER" + "VOYAGE"):

                    Affichage.Modifier(Menu);

                    voyage.Modifier();

                    Action = "";

                    break;

                    

                case ("AJOUTER" + "VOYAGE"):

                    //AMELIORATIONS : prendre en compte Age (automatique) et non adresse mail si client = false.
                    //AMELIORATIONS : selectionner destinations sans utiliser seulement l'ID destination.
                    //AMELIORATIONS : utiliser la methode suivante :
                        //Voyage_ADO nouveau = new Voyage_ADO(4, "Toto", "titi", 2000, 500, "Mécanicien");
                        //quel.create(nouveau);

                    Affichage.Ajouter(Menu);

                    voyage.Ajouter();

                    Action = "";


                    break;


                case ("SUPPRIMER" + "VOYAGE"):

                    Affichage.Supprimer(Menu);

                    voyage.Supprimer();

                    Action = "";

                    break;




                case ("RECHERCHER" + "CLIENT"):

                    //AMELIORATIONS : rechercher grâce à des "contain()" pour recherche puissante.
                    
                    Affichage.Rechercher(Menu);

                    participant.Rechercher();

                    Action = "";

                    break;


                    
                case ("MODIFIER" + "CLIENT"):

                    Affichage.Modifier(Menu);

                    participant.Modifier();

                    Action = "";

                    break;
                    


                case ("AJOUTER" + "CLIENT"):

                    //AMELIORATIONS : prendre en compte Age (automatique) et non adresse mail si client = false.
                    //AMELIORATIONS : utiliser la methode suivante :
                        //Participant_ADO nouveau = new Participant_ADO(4, "Toto", "titi", 2000, 500, "Mécanicien");
                        //quel.create(nouveau);

                    Affichage.Ajouter(Menu);

                    participant.Ajouter();

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
                    Quitter();
                    break;
                case ("QUITTER" + "VOYAGE"):
                    Quitter();
                    break;
                case ("QUITTER" + "CLIENT"):
                    Quitter();
                    break;


                default:
                    Affichage.CommandeInconnue();
                    Menu = "";
                    Action = "";
                    break;


                case ("TEST"):


                    //Participant_ADO autre = new Participant_ADO();
                    //            Perso_ADO autre = new Perso_ADO(2,"","",0,0,"");
                    //foreach (Participant_ADO elem in qui.Find(autre)) { Console.WriteLine(elem.ToString()); }

                    /*
                    b.Connect();
                    Affichage.ShowDataSet(b.ExecSelect("select * from Participants"));
                    //b.ExecUpdate("update dbo.perso set salaire = 10000 where ID_Participant=5");
                    //Affichage.ShowResult(b.ExecProcStock("MAJ"));
                    //String[] parametres = { "Val", "1000", "Niveau", "5000" };
                    //Affichage.ShowResult(b.ExecProcStockWithParams("MAJ_PARAM",parametres));
                    b.Disconnect();
                    */

                    break;
                    
            }

            SwitchMenus();

        }


        public void Quitter()
        {
            Environment.Exit(0);
        }



    }
}