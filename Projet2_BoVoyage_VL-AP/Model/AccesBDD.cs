﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Projet2_BoVoyage_VL_AP.View;
using Projet2_BoVoyage_VL_AP.Controller;

namespace Projet2_BoVoyage_VL_AP.Model
{

    class AccesBDD
    {

        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader dr;
        private DataSet ds;
        private string source;
        private string catalog;
        private string security;

        public AccesBDD (string source, string catalog, string security)
        {

            this.con = new SqlConnection();
            this.cmd = new SqlCommand();
            //this.dr = new SqlDataReader();
            this.ds = new DataSet();
            this.source = source;
            this.catalog = catalog;
            this.security = security;

            ExecMAJ();

        }

       
        public string Source { get => source; set => source = value; }
        public string Catalog { get => catalog; set => catalog = value; }
        public string Security { get => security; set => security = value; }
        

        public SqlConnection Connect()
        {
            bool result = true;
            con = new SqlConnection(); 
            con.ConnectionString = @"Data Source=" + this.source + "; Initial Catalog=" + this.catalog + "; Integrated Security=" + this.security + "";

            try
            {
                this.con.Open();
            }
            catch (Exception e)
            {
                Affichage.ErreurCustom("connexion");
                Affichage.Erreur(e);
                result = false;
            }
            return result ? this.con : null;
        }


        public DataSet ExecSelect(string request)
        {
            try
            {
                this.ds.Clear();
                this.cmd.CommandText = request;
                this.cmd.Connection = this.con;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = this.cmd;
                adapter.Fill(this.ds, "Resultat");
            }
            catch (Exception e)
            {
                Affichage.ErreurCustom("requête");
                Affichage.Erreur(e); 
                this.ds = null;
            }
            return this.ds;
        }

        /*
        public DataSet ExecSelect2(string request)
        {
            try
            {
                this.ds.Clear();
                this.cmd.CommandText = request;
                this.cmd.Connection = this.con;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = this.cmd;
                adapter.Fill(this.ds, "Resultat");
            }
            catch (Exception e)
            {
                Affichage.ErreurCustom("requête");
                Affichage.Erreur(e);
                this.ds = null;
            }
            return this.ds;
        }
        */

        public Boolean ExecUpdate(string request)
        {
            bool result = true;
            try
            {
                this.cmd.CommandText = request;
                this.cmd.Connection = this.con;
                this.cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Affichage.ErreurCustom("requête");
                Affichage.Erreur(e);
                result = false;
            }
            return result;
        }


        public Boolean ExecMAJ()
        {
            bool result = true;

            Connect();

            try
            {

                this.cmd.CommandText = "execute dbo.MajAge; " +
                    "execute dbo.MajPrixTot; " +
                    "execute dbo.ChangerStatutDossierDateRetour;";
                this.cmd.Connection = this.con;
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Affichage.ErreurCustom("requête");
                Affichage.Erreur(e);
                result = false;
            }

            Disconnect();

            return result;
        }


        public Boolean ExecDossAssocie(int idPartic)
        {
            bool result = true;

            try
            {

                this.cmd.CommandText = "select distinct D.ID_Dossier, D.EtatDossResa, D.ID_Voyage " +
                    "from Dossiers D, Participer Per, Participants Pnt " +
                    "where Per.ID_Dossier = D.ID_Dossier and Per.ID_Participant = Pnt.ID_Participant and Pnt.ID_Participant =" + idPartic + ";";
                this.cmd.Connection = this.con;
                this.dr = cmd.ExecuteReader();

                Console.WriteLine("\r\n\tVoici le(s) dossier(s) relatif(s) au participant n° " + idPartic + " : \r\n");

                while (dr.Read())
                {
                    Console.WriteLine(
                        "\tIDDossier= " + dr["ID_Dossier"].ToString() +
                        "; EtatDossier= " + dr["EtatDossResa"].ToString() +
                        "; IDVoyage= " + dr["ID_Voyage"].ToString() + "\r\n"
                        );
                }

                this.dr.Close();

            }
            catch (Exception e)
            {
                Affichage.ErreurCustom("requête");
                Affichage.Erreur(e);
                result = false;
            }
            return result;
        }


        public Boolean ExecVerifDoss(int idDoss)
        {
            bool result = true;

            try
            {

                this.cmd.CommandText = "select * from Dossiers where ID_Dossier =" + idDoss + ";";
                this.cmd.Connection = this.con;
                this.dr = cmd.ExecuteReader();

                Console.WriteLine("\r\n\t********** Dossier n° " + idDoss + " : \r\n");

                while (dr.Read())
                {
                    Console.WriteLine(
                        "\tIDDossier= " + dr["ID_Dossier"].ToString() +
                        "; NumeroCB= " + dr["numeroCB"].ToString() +
                        "; PrixTotal= " + dr["PrixTotal"].ToString() +
                        "; RaisonAnnulDossier= " + dr["RaisonAnnulDoss"].ToString() +
                        "; EtatDossResa= " + dr["EtatDossResa"].ToString() +
                        "; IDVoyage= " + dr["ID_Voyage"].ToString() +
                        "; IDClient= " + dr["ID_Client"].ToString()
                        );
                }

                this.dr.Close();

                this.cmd.CommandText = "select * from VoyageDossier(" + idDoss + ");";
                this.cmd.Connection = this.con;
                this.dr = cmd.ExecuteReader();

                Console.WriteLine("\r\n\r\n\t********** Pour le voyage suivant : \r\n");

                while (dr.Read())
                {
                    Console.WriteLine(
                        "\tPlacesDisponibles= " + dr["PlacesDisponibles"].ToString() +
                        "; IDVoyage= " + dr["ID_Voyage"].ToString() +
                        "; DateAller= " + dr["DateAller"].ToString() +
                        "; DateRetour= " + dr["DateRetour"].ToString() +
                        "; Tarif/Pers(EUR)= " + dr["TarifTTC"].ToString() +
                        "; Agence= " + dr["AgenceVoyage"].ToString() +
                        "; IDDestination= " + dr["ID_Destination"].ToString() +
                        "; Continent= " + dr["Continent"].ToString() +
                        "; Pays= " + dr["Pays"].ToString() +
                        "; Region= " + dr["Region"].ToString() +
                        "; \r\n\tDescriptionVoyage = " + dr["DescriptionVoyage"].ToString()
                        );
                }

                this.dr.Close();

                this.cmd.CommandText = "select * from ParticipantsDossier(" + idDoss + ");";
                this.cmd.Connection = this.con;
                this.dr = cmd.ExecuteReader();

                Console.WriteLine("\r\n\r\n\t********** Voyageurs inscrits au dossier : \r\n");

                while (dr.Read())
                {
                    Console.WriteLine(
                        "\tClient?= " + dr["Client"].ToString() +
                        "; ID= " + dr["ID_Participant"].ToString() +
                        "; " + dr["Civilite"].ToString() +
                        "; Prenom= " + dr["Prenom"].ToString() +
                        "; Nom= " + dr["Nom"].ToString() +
                        "; Adresse= " + dr["Adresse"].ToString() +
                        "; Tel= " + dr["Telephone"].ToString() +
                        "; Email= " + dr["Email"].ToString() +
                        "; DateNaissance= " + dr["DateNaissance"].ToString() +
                        "; Age= " + dr["Age"].ToString() + "ans"
                        );
                }

                this.dr.Close();
                this.cmd.CommandText = "select * from AssurancesDossier(" + idDoss + ");";
                this.cmd.Connection = this.con;
                this.dr = cmd.ExecuteReader();

                Console.WriteLine("\r\n\r\n\t********** Assurances souscrites par le client : \r\n");

                while (dr.Read())
                {
                    Console.WriteLine(
                        "\tIDAssurance= " + dr["ID_Assurance"].ToString() +
                        "; TypeAssurance= " + dr["Type_Assurance"].ToString()
                        );
                }

                this.dr.Close();

            }
            catch (Exception e)
            {
                Affichage.ErreurCustom("requête");
                Affichage.Erreur(e);
                result = false;
            }
            return result;
        }


        public Boolean ExecInsert(string rq1, string table)
        {
            bool result = true;

            List<string> columns = new List<string>();

            try
            {
                
                this.cmd.CommandText = rq1;
                this.cmd.Connection = this.con;
                this.dr = cmd.ExecuteReader();

                for (int i = 0; i < this.dr.FieldCount; i++)
                {

                    while (this.dr.Read())
                    {
                        columns.Add(this.dr.GetValue(i).ToString());
                    }

                }

                this.dr.Close();

                string rq2 = "INSERT INTO " + table + " VALUES('";
                for (int j = 1; j < columns.Count(); j++)
                {
                    Console.WriteLine("\tEntrez " + columns[j]);

                    if (j == columns.Count() - 1)
                    { rq2 += Console.ReadLine(); }

                    else
                    { rq2 += Console.ReadLine() + "', '"; }
                }
                rq2 += "');";

                this.cmd.CommandText = rq2;
                this.cmd.Connection = this.con;
                this.dr = cmd.ExecuteReader();
                this.dr.Close();

            }
            catch (Exception e)
            {
                Affichage.ErreurCustom("requête");
                Affichage.Erreur(e);
                result = false;
            }

            return result;
        }


        public Boolean Disconnect()
        {
            bool result = true;
            try
            {
                this.con.Close();
            }
            catch (Exception e)
            {
                Affichage.ErreurCustom("connexion");
                Affichage.Erreur(e);
                result = false;
            }
            return result;
        }
        

    }
}
