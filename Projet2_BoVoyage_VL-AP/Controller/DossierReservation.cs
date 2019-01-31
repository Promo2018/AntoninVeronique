using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Projet2_BoVoyage_VL_AP.Model;
using Projet2_BoVoyage_VL_AP.View;

namespace Projet2_BoVoyage_VL_AP.Controller
{

    class DossierReservation
    {

        //PARAMETRE DANS CONSTRUCTEUR POUR AGREGATION
        public DossierReservation() //FAIRE UNE LISTE DE PARTICIPANTS ?
        {


        }


        AccesBDD bdd = new AccesBDD("localhost", "BoVoyage_VAV2", "SSPI");

        List<string> champs = new List<string>();

        DossierReservation_ADO reservationADO = new DossierReservation_ADO();
        Voyage_ADO voyageADO = new Voyage_ADO();
        Participant_ADO participantADO = new Participant_ADO();


        public List<DossierReservation_ADO> Find(DossierReservation_ADO id)
        {

            // construction de la requete
            
            string rq = "select * from Dossiers where ";
            if (id.Id_Dossier > -1) { rq += "ID_Dossier = " + id.Id_Dossier + " and "; }
            if (!string.IsNullOrEmpty(id.NumeroCarteBancaire)) { rq += "numeroCB = '" + id.NumeroCarteBancaire + "' and "; }
            if (id.PrixTotal > -1) { rq += "PrixTotal = " + id.PrixTotal + " and "; }
            if (!string.IsNullOrEmpty(id.RaisonAnnulDoss)) { rq += "RaisonAnnulDoss = '" + id.RaisonAnnulDoss + "' and "; }
            if (!string.IsNullOrEmpty(id.EtatDossResa)) { rq += "EtatDossResa = '" + id.EtatDossResa + "' and "; }
            if (id.Id_Voyage > -1) { rq += "ID_Voyage = " + id.Id_Voyage + " and "; }
            if (id.Id_Participant > -1) { rq += "ID_Client = " + id.Id_Participant + " and "; }

            // rajout de la condition, toujours vraie "1 = 1" pour terminer le dernier 'and'
            rq += "1 = 1";
            

            this.bdd.Connect();
            DataSet ds = this.bdd.ExecSelect(rq);
            List<DossierReservation_ADO> maListe = new List<DossierReservation_ADO>();
            if (ds.Tables["Resultat"].Rows.Count > 0)
            {
                foreach (DataRow ligne in ds.Tables["Resultat"].Rows)
                {
                    DossierReservation_ADO reservationADO = new DossierReservation_ADO(
                        Int32.Parse(ligne["ID_Dossier"].ToString()),
                        ligne["numeroCB"].ToString(),
                        Decimal.Parse(ligne["PrixTotal"].ToString()),
                        ligne["RaisonAnnulDoss"].ToString(),
                        ligne["EtatDossResa"].ToString(),
                        Int32.Parse(ligne["ID_Voyage"].ToString()),
                        Int32.Parse(ligne["ID_Client"].ToString())
                        );

                    maListe.Add(reservationADO);
                }
            }
            return maListe;
        }



        public void Rechercher()
        {

            int idDoss = Int32.Parse(Console.ReadLine());
            bdd.Connect();

            bdd.ExecVerifDoss(idDoss);

            bdd.Disconnect();
   

        }


        public void Annuler(string raison) //AJOUTER CONDITIONS ANNULATION
        {

            reservationADO.RechercheChamps();

            foreach (DossierReservation_ADO elem in Find(reservationADO))
            {
                Affichage.Valider("annuler le dossier", elem.AfficherChamps());
            }

            if (Console.ReadLine().ToUpper() == "OUI")
            {
                string rq = "update Dossiers set EtatDossResa = '" + /*DossierReservation_ADO.EtatDossierReservation.refusee.ToString()*/ "Refusé" +
                "' where ID_Dossier = " + reservationADO.Id_Dossier + ";";

                bdd.Connect();
                bdd.ExecUpdate(rq);
                bdd.Disconnect();
            }
        }


        public void ValiderSolvabilite() //AJOUTER CONDITIONS VALIDERSOLVABILITE ET POSSIBILITE REFUS
        {

            reservationADO.RechercheChamps();

            foreach (DossierReservation_ADO elem in Find(reservationADO))
            {
                Affichage.Valider("vérifier la solvabilité du dossier", elem.AfficherChamps());
            }

            if (Console.ReadLine().ToUpper() == "OUI")
            {
                string rq = "update Dossiers set EtatDossResa = '" + /*DossierReservation_ADO.EtatDossierReservation.enCours.ToString()*/ "En cours" +
                "' where ID_Dossier = " + reservationADO.Id_Dossier + ";";

                bdd.Connect();
                bdd.ExecUpdate(rq);
                bdd.Disconnect();
            }

        }


        public void Accepter() //AJOUTER CONDITIONS ACCEPTER ET POSSIBILITE REFUS
        {

            reservationADO.RechercheChamps();

            foreach (DossierReservation_ADO elem in Find(reservationADO))
            {
                Affichage.Valider("accepter le dossier", elem.AfficherChamps());
            }

            if (Console.ReadLine().ToUpper() == "OUI")
            {
                string rq = "update Dossiers set EtatDossResa = '" + /*DossierReservation_ADO.EtatDossierReservation.acceptee.ToString()*/ "Accepté" +
                "' where ID_Dossier = " + reservationADO.Id_Dossier + ";";

                bdd.Connect();
                bdd.ExecUpdate(rq);
                bdd.Disconnect();
            }

        }
        
    }
}
