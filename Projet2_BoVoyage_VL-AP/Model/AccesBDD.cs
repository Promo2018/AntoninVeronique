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


        public Boolean ExecVerifDoss(int idDoss)
        {
            bool result = true;

            List<string> columns = new List<string>();

            try
            {

                this.cmd.CommandText = "select * from VoyageDossier(" + idDoss + ");";
                this.cmd.Connection = this.con;
                this.dr = cmd.ExecuteReader();

                Console.WriteLine("\r\n\tVoici le voyage relatif au dossier n° " + idDoss + " : \r\n");

                while (dr.Read())
                {
                    Console.WriteLine(
                        "\tPlaces disponibles = " + dr["PlacesDisponibles"].ToString() +
                        "; " + dr["ID_Voyage"].ToString() +
                        "; " + dr["DateAller"].ToString() +
                        "; " + dr["DateRetour"].ToString() +
                        "; " + dr["TarifTTC"].ToString() +
                        "; " + dr["AgenceVoyage"].ToString() +
                        "; " + dr["ID_Destination"].ToString() +
                        "; " + dr["Continent"].ToString() +
                        "; " + dr["Pays"].ToString() +
                        "; " + dr["Region"].ToString() +
                        "; \r\n\tDescription Voyage : " + dr["DescriptionVoyage"].ToString()
                        );
                }

                this.dr.Close();

                this.cmd.CommandText = "select * from ParticipantsDossier(" + idDoss + ");";
                this.cmd.Connection = this.con;
                this.dr = cmd.ExecuteReader();

                Console.WriteLine("\r\n\tVoici les voyageurs inscrits au même dossier : \r\n");

                while (dr.Read())
                {
                    Console.WriteLine(
                        "\tClient = " + dr["Client"].ToString() +
                        "; " + dr["ID_Participant"].ToString() +
                        "; " + dr["Civilite"].ToString() +
                        "; " + dr["Prenom"].ToString() +
                        "; " + dr["Nom"].ToString() +
                        "; " + dr["Adresse"].ToString() +
                        "; " + dr["Telephone"].ToString() +
                        "; " + dr["Email"].ToString() +
                        "; " + dr["DateNaissance"].ToString() +
                        "; " + dr["Age"].ToString()
                        );
                }

                this.dr.Close();
                this.cmd.CommandText = "select * from AssurancesDossier(" + idDoss + ");";
                this.cmd.Connection = this.con;
                this.dr = cmd.ExecuteReader();

                Console.WriteLine("\r\n\tVoici les assurances souscrites par le client : \r\n");

                while (dr.Read())
                {
                    Console.WriteLine(
                        "\t" + dr["ID_Assurance"].ToString() +
                        "; " + dr["Type_Assurance"].ToString()
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
