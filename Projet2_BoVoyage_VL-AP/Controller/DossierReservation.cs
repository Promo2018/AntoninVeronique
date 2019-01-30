using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet2_BoVoyage_VL_AP.View;

namespace Projet2_BoVoyage_VL_AP.Controller
{

    class DossierReservation
    {


        //PARAMETRE DANS CONSTRUCTEUR POUR AGREGATION
        public DossierReservation(List<Participant> participants) //FAIRE UNE LISTE DE PARTICIPANTS ?
        {


        }

        private int numeroUnique;
        private string numeroCarteBancaire;

        public enum RaisonAnnulationDossier : byte { client, placesInsuffisantes };
        public enum EtatDossierReservation : byte { enAttente, enCours, refusee, acceptee };

        public int NumeroUnique { get => numeroUnique; set => numeroUnique = value; }
        public string NumeroCarteBancaire { get => numeroCarteBancaire; set => numeroCarteBancaire = value; }

        public decimal PrixTotal
        {
            get
            {
                decimal prixTotal = 100;
                return prixTotal; //Qu'est-ce que le prix total ?
            }
        }


        DossierReservation_ADO reservationADO = new DossierReservation_ADO();

        

        public void Annuler(RaisonAnnulationDossier raison)
        {

            string request = "";

            reservationADO.AnnulerADO(request);

        }

        public void ValiderSolvabilite()
        {

            string request = "";

            reservationADO.ValiderSolvabiliteADO(request);

        }

        public void Accepter()
        {

            string request = "";

            reservationADO.AccepterADO(request);

        }


        public void TestPrixTotal()
        {

            Console.WriteLine("\r\n\tLe prix total du dossier est : " + PrixTotal + ".");

        }

    }
}
