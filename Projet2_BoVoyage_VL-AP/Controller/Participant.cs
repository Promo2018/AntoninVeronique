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
    class Participant : Personne
    {

        public Participant()
        {


        }


        Participant_ADO participantADO = new Participant_ADO();

        AccesBDD bdd = new AccesBDD("localhost", "BoVoyage_VAV2", "SSPI");

        List<string> champs = new List<string>();



        public List<Participant_ADO> Find(Participant_ADO id)
        {

            // construction de la requete
            string rq = "select * from Participants where ";
            if (id.NumeroSequentiel > -1) { rq += "ID_Participant = " + id.NumeroSequentiel + " and "; }
            if (!string.IsNullOrEmpty(id.Civilite)) { rq += "Civilite = '" + id.Civilite + "' and "; }
            if (!string.IsNullOrEmpty(id.Nom)) { rq += "Nom = '" + id.Nom + "' and "; }
            if (!string.IsNullOrEmpty(id.Prenom)) { rq += "Prenom = '" + id.Prenom + "' and "; }
            if (!string.IsNullOrEmpty(id.Adresse)) { rq += "Adresse = '" + id.Adresse + "' and "; }
            if (!string.IsNullOrEmpty(id.Telephone)) { rq += "Telephone = '" + id.Telephone + "' and "; }
            if (id.DateNaissance > DateTime.Parse("02/01/1990")) { rq += "DateNaissance = '" + id.DateNaissance + "' and "; }
            if (!string.IsNullOrEmpty(id.Client.ToString())) { rq += "Client = '" + id.Client + "' and "; }
            if (!string.IsNullOrEmpty(id.Email)) { rq += "Email = '" + id.Email + "' and "; }
            // rajout de la condition, toujours vraie "1 = 1" pour terminer le dernier 'and'
            rq += "1 = 1";


            this.bdd.Connect();
            DataSet ds = this.bdd.ExecSelect(rq);
            List<Participant_ADO> maListe = new List<Participant_ADO>();
            if (ds.Tables["Resultat"].Rows.Count > 0)
            {
                foreach (DataRow ligne in ds.Tables["Resultat"].Rows)
                {
                    Participant_ADO partic = new Participant_ADO(
                        Int32.Parse(ligne["ID_Participant"].ToString()),
                        ligne["Civilite"].ToString(),
                        ligne["Nom"].ToString(),
                        ligne["Prenom"].ToString(),
                        ligne["Adresse"].ToString(),
                        ligne["Telephone"].ToString(),
                        DateTime.Parse(ligne["DateNaissance"].ToString()),
                        ligne["Age"].ToString(),
                        Boolean.Parse(ligne["Client"].ToString()),
                        ligne["Email"].ToString()
                        ); 

                    maListe.Add(partic);
                }
            }
            return maListe;
        }


        public void Ajouter(Participant_ADO p)
        {
            this.bdd.Connect();
            this.bdd.ExecUpdate("insert into Participants (Civilite,Nom,Prenom,Adresse,Telephone,DateNaissance,Age,Client,Email) values " +
                "('" + p.Civilite + "','" + p.Nom + "','" + p.Prenom + "','" + p.Adresse + "','" + p.Telephone + "','" +
                p.DateNaissance + "'," + null + "," + Convert.ToInt32(p.Client) + ",'" + p.Email + "');"
                );
        }


        public void Modifier()
        {

            participantADO.RechercheChamps();

            foreach (Participant_ADO elem in Find(participantADO))
            {
                Console.WriteLine(elem.AfficherChamps());
            }

            Affichage.ModifierChamps();

            champs = Console.ReadLine().Trim().Split(new[] { ';' }, StringSplitOptions.None).ToList();

            string rq = "update Participants set ";
            foreach (string line in champs)
            {
                if (line.ToUpper().Contains("CIV"))
                {
                    Console.WriteLine("\r\n\tQuelle nouvelle civilité ?");
                    rq += "Civilite = '" + Console.ReadLine().ToString() + "' ";
                }
                if (line.ToUpper() == "NOM")
                {
                    Console.WriteLine("\r\n\tQuel nouveau nom ?");
                    rq += "Nom = '" + Console.ReadLine().ToString() + "' ";
                }
                if (line.ToUpper() == "PRENOM")
                {
                    Console.WriteLine("\r\n\tQuel nouveau prenom ?");
                    rq += "Prenom = '" + Console.ReadLine().ToString() + "' ";
                }
                if (line.ToUpper().Contains("ADRES"))
                {
                    Console.WriteLine("\r\n\tQuelle nouvelle adresse ?");
                    rq += "Adresse = '" + Console.ReadLine().ToString() + "' ";
                }
                if (line.ToUpper().Contains("TEL"))
                {
                    Console.WriteLine("\r\n\tQuel nouveau téléphone ?");
                    rq += "Telephone = '" + Console.ReadLine().ToString() + "' ";
                }
                if (line.ToUpper().Contains("NAISS"))
                {
                    Console.WriteLine("\r\n\tQuelle nouvelle date de naissance ?");
                    rq += "DateNaissance = '" + Console.ReadLine().ToString() + "' ";
                }
                if (line.ToUpper().Contains("CLI") || line.ToUpper().Contains("STAT"))
                {
                    Console.WriteLine("\r\n\tLa personne est-elle cliente ? (true / false)");
                    rq += "Client = '" + Console.ReadLine().ToString() + "' ";
                }
                if (line.ToUpper().Contains("MAIL"))
                {
                    Console.WriteLine("\r\n\tQuelle nouvelle adresse email ?");
                    rq += "Email = '" + Console.ReadLine().ToString() + "' ";
                }
                if (champs.IndexOf(line) + 1 < champs.Count()) { rq += ","; }
            }
            rq += " where ID_Participant = " + participantADO.NumeroSequentiel + " ;";

            bdd.Connect();
            bdd.ExecUpdate(rq);

            bdd.Disconnect();
        }

        public void Rechercher()
        {
            participantADO.RechercheChamps();

            foreach (Participant_ADO elem in Find(participantADO))
            {
                Console.WriteLine(elem.AfficherChamps());
            }


        }

        public void Ajouter()
        {
            string table = "Participants";
            string rq = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME ='" + table + "';";
            bdd.Connect();
            bdd.ExecInsert(rq, table);
            bdd.Disconnect();

        }

    }

}
