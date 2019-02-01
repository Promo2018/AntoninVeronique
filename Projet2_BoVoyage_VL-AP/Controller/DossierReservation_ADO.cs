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
    class DossierReservation_ADO 
    {

        private int id_Dossier;
        private string numeroCarteBancaire;
        private decimal prixTotal;
        private string raisonAnnulDoss;
        private string etatDossResa;
        private int id_Voyage;
        private int id_Participant;

        public DossierReservation_ADO()
        {
            id_Dossier = -1;
            numeroCarteBancaire = null;
            prixTotal = -1;
            raisonAnnulDoss = null;
            etatDossResa = null;
            id_Voyage = -1;
            id_Participant = -1;
        }

        public DossierReservation_ADO(int id_Dossier, string numeroCarteBancaire, decimal prixTotal, 
            string raisonAnnulDoss, string etatDossResa, int id_Voyage, int id_Participant)
        {
            Id_Dossier = id_Dossier;
            NumeroCarteBancaire = numeroCarteBancaire;
            RaisonAnnulDoss = raisonAnnulDoss;
            EtatDossResa = etatDossResa;
            Id_Voyage = id_Voyage;
            Id_Participant = id_Participant;
        }
        
        /*
        public decimal PrixTotal
        {
            get
            {
                decimal prixTotal = 100;
                return prixTotal; //Qu'est-ce que le prix total ?
            }
        }
        */

        public int Id_Dossier { get => id_Dossier; set => id_Dossier = value; }
        public string NumeroCarteBancaire { get => numeroCarteBancaire; set => numeroCarteBancaire = value; }
        public decimal PrixTotal { get => prixTotal; set => prixTotal = value; }
        public string RaisonAnnulDoss { get => raisonAnnulDoss; set => raisonAnnulDoss = value; }
        public string EtatDossResa { get => etatDossResa; set => etatDossResa = value; }
        public int Id_Voyage { get => id_Voyage; set => id_Voyage = value; }
        public int Id_Participant { get => id_Participant; set => id_Participant = value; }


        public enum RaisonAnnulationDossier : byte { client, placesInsuffisantes };
        public enum EtatDossierReservation : byte { enAttente, enCours, refusee, acceptee };


        
        public string AfficherChamps()
        {
            return (Id_Dossier + "; " + NumeroCarteBancaire + "; " + NumeroCarteBancaire + "; " + PrixTotal + "; " +
                RaisonAnnulDoss + "; " + EtatDossResa + "; " + Id_Voyage + "; " + Id_Participant);
        }
        

        public void RechercheChampsID()
        {
            try
            {
                Id_Dossier = Int32.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Id_Dossier = 0;
            }

        }

        /*
        public void RechercheChampsGeneral()
        {
            try
            {
                Id_Dossier = Int32.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Id_Dossier = 0;
            }

        }
        */

        public void TestPrixTotal()
        {

            Console.WriteLine("\r\n\tLe prix total du dossier est : " + PrixTotal + ".");

        }

    }
}
