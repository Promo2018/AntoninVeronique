using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet2_BoVoyage_VL_AP.View;

namespace Projet2_BoVoyage_VL_AP.Controller
{
    class Destination
    {
        public Destination()
        {


        }

        private string continent;
        private string pays;
        private string region;
        private string description;

        public string Continent { get => continent; set => continent = value; }
        public string Pays { get => pays; set => pays = value; }
        public string Region { get => region; set => region = value; }
        public string Description { get => description; set => description = value; }


    }
}
