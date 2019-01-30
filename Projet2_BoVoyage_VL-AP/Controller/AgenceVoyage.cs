using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet2_BoVoyage_VL_AP.View;

namespace Projet2_BoVoyage_VL_AP.Controller
{
    class AgenceVoyage
    {

        //PARAMETRE DANS CONSTRUCTEUR POUR AGREGATION
        public AgenceVoyage(List<Voyage> voyages)
        {


        }

        private string nom;
        public string Nom { get => nom; set => nom = value; }
    }
}
