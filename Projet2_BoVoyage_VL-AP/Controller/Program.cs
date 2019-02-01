using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Projet2_BoVoyage_VL_AP.View;
using Projet2_BoVoyage_VL_AP.Model;
using System.Reflection;

namespace Projet2_BoVoyage_VL_AP.Controller
{

    class Program
    {

        static void Main(string[] args)
        {
            
            Menus menus = new Menus();
            
            menus.SwitchMenus();

            Menus.Quitter(0);


            // AMELIORATIONS :
            // -- Separer en mode OBJET le + possible (pour l'instant c'est pas ouf)
            // -- VOIR MENU
            // -- VOIR CONSTRUCTEUR AccesBDD
            // -- VOIR DossierReservation

        }
    }
}
