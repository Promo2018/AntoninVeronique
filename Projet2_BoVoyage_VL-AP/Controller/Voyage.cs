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
    class Voyage
    {

        public Voyage()
        {


        }
        
        Voyage_ADO voy = new Voyage_ADO();

        AccesBDD bdd = new AccesBDD("localhost", "BoVoyage_VAV2", "SSPI");
        
        List<string> champs = new List<string>();


        public List<Voyage_ADO> Find(int id_voyage)
        {
            this.bdd.Connect();
            DataSet ds = this.bdd.ExecSelect("select * from Voyages where ID_Voyage=" + id_voyage + ";");
            List<Voyage_ADO> maListe = new List<Voyage_ADO>();

            if (ds.Tables["Resultat"].Rows.Count > 0)
            {
                foreach (DataRow ligne in ds.Tables["Resultat"].Rows)
                {
                    Voyage_ADO voy = new Voyage_ADO(
                        Int32.Parse(ligne["ID_Voyage"].ToString()),
                        DateTime.Parse(ligne["DateAller"].ToString()),
                        DateTime.Parse(ligne["DateRetour"].ToString()),
                        Int32.Parse(ligne["PlacesDisponibles"].ToString()),
                        Decimal.Parse(ligne["TarifTTC"].ToString()),
                        ligne["AgenceVoyage"].ToString(),
                        Int32.Parse(ligne["Id_Destination"].ToString())
                        );
                    maListe.Add(voy);
                }
            }
            return maListe;
        }

        

        public List<Voyage_ADO> Find(Voyage_ADO id)
        {
            
            // construction de la requete
            string rq = "select * from Voyages where ";
            if (id.Id_Voyage > -1) { rq += "ID_Voyage = " + id.Id_Voyage + " and "; }
            if (id.DateAller > DateTime.Parse("02/01/1990")) { rq += "DateAller = '" + id.DateAller + "' and "; }
            if (id.DateRetour > DateTime.Parse("02/01/1990")) { rq += "DateRetour = '" + id.DateRetour + "' and "; }
            if (id.PlacesDisponibles > -1) { rq += "PlacesDisponibles = " + id.PlacesDisponibles + " and "; }
            if (id.TarifTTC > -1) { rq += "TarifTTC = " + id.TarifTTC + " and "; }
            if (!string.IsNullOrEmpty(id.AgenceVoyage)) { rq += "AgenceVoyage = '" + id.AgenceVoyage + "' and "; }
            if (id.Id_Destination > -1) { rq += "ID_Destination = " + id.Id_Destination + " and "; }
            // rajout de la condition, toujours vraie "1 = 1" pour terminer le dernier 'and'
            rq += "1 = 1";

            this.bdd.Connect();
            DataSet ds = this.bdd.ExecSelect(rq);
            List<Voyage_ADO> maListe = new List<Voyage_ADO>();
            if (ds.Tables["Resultat"].Rows.Count > 0)
            {
                foreach (DataRow ligne in ds.Tables["Resultat"].Rows)
                {
                    Voyage_ADO voy = new Voyage_ADO(
                        Int32.Parse(ligne["ID_Voyage"].ToString()),
                        DateTime.Parse(ligne["DateAller"].ToString()),
                        DateTime.Parse(ligne["DateRetour"].ToString()),
                        Int32.Parse(ligne["PlacesDisponibles"].ToString()),
                        Decimal.Parse(ligne["TarifTTC"].ToString()),
                        ligne["AgenceVoyage"].ToString(),
                        Int32.Parse(ligne["ID_Destination"].ToString())
                        );

                    maListe.Add(voy);
                }
            }
            return maListe;
        }




        
        public void Ajouter(Voyage_ADO v)
        {
            this.bdd.Connect();
            this.bdd.ExecUpdate("insert into Voyages (DateAller,DateRetour,PlacesDisponibles,TarifTTC,AgenceVoyage,ID_Destination) values " +
                "('" + v.DateAller + "','" + v.DateRetour + "'," + v.PlacesDisponibles + "," + v.TarifTTC + ",'" +
                v.AgenceVoyage + "'," + v.Id_Destination + ");"
                );
        }
        



        public void Supprimer(int id)
        {
            this.bdd.Connect();
            this.bdd.ExecUpdate("delete from Voyages where ID_Voyage=" + id + ";");
        }


        public void Modifier()
        {
            
            voy.RechercheChamps();

            foreach (Voyage_ADO elem in Find(voy))
            {
                Console.WriteLine(elem.AfficherChamps());
            }

            Affichage.ModifierChamps();

            champs = Console.ReadLine().Trim().Split(new[] { ';' }, StringSplitOptions.None).ToList();

            string rq = "update Voyages set ";
            foreach (string line in champs)
            {
                if (line.ToUpper().Contains("ALLER"))
                {
                    Console.WriteLine("\r\n\tQuelle nouvelle date aller ?");
                    rq += "DateAller = '" + Console.ReadLine().ToString() + "' ";
                }
                if (line.ToUpper().Contains("RETOUR"))
                {
                    Console.WriteLine("\r\n\tQuelle nouvelle date retour ?");
                    rq += "DateRetour = '" + Console.ReadLine().ToString() + "' ";
                }
                if (line.ToUpper().Contains("PLACE") || line.ToUpper().Contains("DISPO"))
                {
                    Console.WriteLine("\r\n\tCombien de places disponibles à présent ?");
                    rq += "PlacesDisponibles = " + Int32.Parse(Console.ReadLine()) + " ";
                }
                if (line.ToUpper().Contains("TARIF"))
                {
                    Console.WriteLine("\r\n\tQuel nouveau tarif TTC ?");
                    rq += "TarifTTC = " + Decimal.Parse(Console.ReadLine()) + " ";
                }
                if (line.ToUpper().Contains("AGENCE"))
                {
                    Console.WriteLine("\r\n\tQuelle nouvelle agence ?");
                    rq += "AgenceVoyage = '" + Console.ReadLine().ToString() + "' ";
                }
                if (line.ToUpper().Contains("DESTI"))
                {
                    Console.WriteLine("\r\n\tQuel ID de nouvelle destination ?");
                    rq += "ID_Destination = " + Int32.Parse(Console.ReadLine()) + " ";
                }
                if (champs.IndexOf(line) + 1 < champs.Count()) { rq += ","; }
            }
            rq += " where ID_Voyage = " + voy.Id_Voyage + " ;";

            bdd.Connect();
            bdd.ExecUpdate(rq);

            bdd.Disconnect();

        }


        public void Rechercher()
        {
            voy.RechercheChamps();

            foreach (Voyage_ADO elem in Find(voy))
            {
                Console.WriteLine(elem.AfficherChamps());
            }
        }


        public void Ajouter()
        {
            string table = "Voyages";
            string rq = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + table + "';";
            bdd.Connect();
            bdd.ExecInsert(rq, table);
            bdd.Disconnect();

        }

        public void Supprimer()
        {
            voy.RechercheChamps();

            foreach (Voyage_ADO elem in Find(voy))
            {
                Affichage.Valider(elem.AfficherChamps());
            }

            if (Console.ReadLine().ToUpper() == "OUI")
            {
                Supprimer(voy.Id_Voyage);
            }
        }
        

        /*
        public void Reserver(int places)
        {

            places = Int32.Parse(Console.ReadLine());

            if (places <= PlacesDisponibles)
            {

                string request = "";
                


                voyageADO.ReserverADO(request);

            }
            */
            

        


    }


}
