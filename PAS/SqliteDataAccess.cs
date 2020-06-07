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
                string stmt = "select count (SurName) from person";
                int Rowcount = 0;
            sqlite.Open();
            SQLiteCommand cmd = new SQLiteCommand(sqlite);
            cmd.CommandText = "select count (SurName) from person";

            Rowcount = Convert.ToInt32(cmd.ExecuteScalar());

            cmd.CommandText = "select SurName from person";
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

            
            public void AddPerson(Person p)
        {
            SQLiteCommand insertQuery = new SQLiteCommand("insert into person values (?, ?, ?, ?, ?)", sqlite);
            insertQuery.Parameters.Add(p.SurName);
            insertQuery.Parameters.Add(p.GivenName);
            insertQuery.Parameters.Add(p.Height);
            insertQuery.Parameters.Add(p.Gender);
            insertQuery.Parameters.Add(p.EyeColor);
            try
            {
                insertQuery.ExecuteNonQuery();
            }
            catch(Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }


            

    


}
}
