using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PAS
{
    public class SqliteDataAccess
    {

        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private SQLiteConnection sqlite;

       
        public SqliteDataAccess()
        {
            //connection to my pas-database
            sqlite = new SQLiteConnection("Data Source=./pas-database.db");

        }



        public int LoadPerson()
            {
                string stmt = "select count (patientId) from person";
                int Rowcount = 0;
            sqlite.Open();
            SQLiteCommand cmd = new SQLiteCommand(sqlite);
            cmd.CommandText = "select count (patientId) from person";

            Rowcount = Convert.ToInt32(cmd.ExecuteScalar());

            cmd.CommandText = "select patientId from person";
            SQLiteDataReader reader = cmd.ExecuteReader();

            return Rowcount;
        }
            
        
        

         




        /*
        private void LoadPerson()
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "select id, desc from mains";
            DB = new SQLiteDataAdapter(CommandText, sql_con);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            Grid.DataSource = DT;
            sql_con.Close();
        }
        */

            /*
            private void AddPerson()
        {
            string txtSQLQuery = "insert into  mains (desc) values ('" + txtDesc.Text + "')";
            ExecuteQuery(txtSQLQuery);
            }


            private void ExecuteQuery(string txtQuery) 
            { 
            SetConnection(); 
            sql_con.Open(); 
            sql_cmd = sql_con.CreateCommand(); 
            sql_cmd.CommandText=txtQuery; 
            sql_cmd.ExecuteNonQuery(); 
            sql_con.Close(); 
           }

    */


}
}
