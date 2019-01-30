﻿using System;
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



        public Voyage_ADO()
        {

            id_Voyage = -1;
            dateAller = DateTime.Parse("01/01/1990");
            dateRetour = DateTime.Parse("01/01/1990");
            placesDisponibles = -1;
            tarifTTC = -1;
            agenceVoyage = null;
            id_Destination = -1;

        }

        public Voyage_ADO(
            int id_Voyage, DateTime dateAller, DateTime dateRetour, 
            int placesDisponibles, decimal tarifTTC, string agenceVoyage, int id_Destination
            )
        {

            Id_Voyage = id_Voyage;
            DateAller = dateAller;
            DateRetour = dateRetour;
            PlacesDisponibles = placesDisponibles;
            TarifTTC = tarifTTC;
            AgenceVoyage = agenceVoyage;
            Id_Destination = id_Destination;

        }


        public int Id_Voyage { get => id_Voyage; set => id_Voyage = value; }
        public DateTime DateAller { get => dateAller; set => dateAller = value; }
        public DateTime DateRetour { get => dateRetour; set => dateRetour = value; }
        public int PlacesDisponibles { get => placesDisponibles; set => placesDisponibles = value; }
        public decimal TarifTTC { get => tarifTTC; set => tarifTTC = value; }
        public string AgenceVoyage { get => agenceVoyage; set => agenceVoyage = value; }
        public int Id_Destination { get => id_Destination; set => id_Destination = value; }


        public string AfficherChamps()
        {
            return (DateAller + "; " + DateRetour + "; " + PlacesDisponibles + "; " + TarifTTC + "; " + 
                AgenceVoyage + "; " + Id_Destination);
        }

        public void RechercheChamps()
        {

            Id_Voyage = Int32.Parse(Console.ReadLine());

            /*
            string recherche = Console.ReadLine();

            if (DateAller.Contains(DateTime.TryParse(recherche))) { DateAller = DateTime.Parse(recherche); }
            if (DateRetour.Contains(DateTime.TryParse(recherche))) { DateRetour = DateTime.Parse(recherche); }
            if (PlacesDisponibles.Contains(Int32.TryParse(recherche))) { PlacesDisponibles = Int32.Parse(recherche); }
            if (TarifTTC.Contains(Decimal.Parse(recherche))) { TarifTTC = Decimal.Parse(recherche); }
            if (AgenceVoyage.Contains(recherche)) { AgenceVoyage = recherche; }
            if (Id_Destination.Contains(Int32.TryParse(recherche))) { Id_Destination = Int32.Parse(recherche); }
  
            */
        }



    }

}