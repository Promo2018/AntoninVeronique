using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Projet2_BoVoyage_VL_AP.View;
using Projet2_BoVoyage_VL_AP.Model;

namespace Projet2_BoVoyage_VL_AP.Controller
{
    class Voyage_ADO 
    {

        private int id_Voyage;
        private DateTime dateAller;
        private DateTime dateRetour;
        private int placesDisponibles;
        private decimal tarifTTC;
        private string agenceVoyage;
        private int id_Destination;
        private string continent;
        private string pays;
        private string region;
        private string description;


        public Voyage_ADO()
        {

            id_Voyage = -1;
            dateAller = DateTime.Parse("01/01/1990");
            dateRetour = DateTime.Parse("01/01/1990");
            placesDisponibles = -1;
            tarifTTC = -1;
            agenceVoyage = null;
            id_Destination = -1;
            continent = null;
            pays = null;
            region = null;
            description = null;

        }

        /*
        public Voyage_ADO( int id_Voyage, DateTime dateAller, DateTime dateRetour, 
            int placesDisponibles, decimal tarifTTC, string agenceVoyage, int id_Destination )
        {

            Id_Voyage = id_Voyage;
            DateAller = dateAller;
            DateRetour = dateRetour;
            PlacesDisponibles = placesDisponibles;
            TarifTTC = tarifTTC;
            AgenceVoyage = agenceVoyage;
            Id_Destination = id_Destination;

        }
        */

        public Voyage_ADO(int id_Voyage, DateTime dateAller, DateTime dateRetour,
            int placesDisponibles, decimal tarifTTC, string agenceVoyage, int id_Destination,
            string continent, string pays, string region, string description)
        {

            Id_Voyage = id_Voyage;
            DateAller = dateAller;
            DateRetour = dateRetour;
            PlacesDisponibles = placesDisponibles;
            TarifTTC = tarifTTC;
            AgenceVoyage = agenceVoyage;
            Id_Destination = id_Destination;
            Continent = continent;
            Pays = pays;
            Region = region;
            Description = description;

        }

        public int Id_Voyage { get => id_Voyage; set => id_Voyage = value; }
        public DateTime DateAller { get => dateAller; set => dateAller = value; }
        public DateTime DateRetour { get => dateRetour; set => dateRetour = value; }
        public int PlacesDisponibles { get => placesDisponibles; set => placesDisponibles = value; }
        public decimal TarifTTC { get => tarifTTC; set => tarifTTC = value; }
        public string AgenceVoyage { get => agenceVoyage; set => agenceVoyage = value; }
        public int Id_Destination { get => id_Destination; set => id_Destination = value; }
        public string Continent { get => continent; set => continent = value; }
        public string Pays { get => pays; set => pays = value; }
        public string Region { get => region; set => region = value; }
        public string Description { get => description; set => description = value; }


        public string AfficherChamps()
        {
            return ("\r\n\tIDVoyage= " + Id_Voyage + "; DateAller= " + DateAller.ToShortDateString() + "; DateRetour= " + 
                DateRetour.ToShortDateString() + "; PlacesDisponibles=" + PlacesDisponibles + "; Tarif/Pers(EUR)= " + 
                TarifTTC + "; Agence= " + AgenceVoyage + "; IDDestination= " + Id_Destination + "; Continent= " + 
                Continent + "; Pays= " + Pays + "; Region= " + Region + "; \r\n\tDescriptionVoyage= " + Description);
        }


        public void RechercheChampsID()
        {

            try
            {
                Id_Voyage = Int32.Parse(Console.ReadLine());
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
                bool successDec = Decimal.TryParse(recherche, out decimal dec);

                AgenceVoyage = recherche.ToString();
                Continent = recherche.ToString();
                Pays = recherche.ToString();
                Region = recherche.ToString();
                Description = recherche.ToString();

                if (successInt) { Id_Voyage = num; Id_Destination = num; PlacesDisponibles = num; }
                else { Id_Voyage = -1; Id_Destination = -1; PlacesDisponibles = -1; }
                if (successDat) { DateAller = dat; DateRetour = dat; }
                else { DateAller = DateTime.Parse("01/01/1900"); DateRetour = DateTime.Parse("01/01/1900"); }
                if (successDec) { TarifTTC = dec; }
                else { TarifTTC = -1; }
                
            }
            catch (Exception e)
            {
                Affichage.Erreur(e);
            }
            

        }


    }

}
