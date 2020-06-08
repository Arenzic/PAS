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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace PAS
{
    public class SqliteDataAccess
    {

        private SQLiteConnection sqlite;

       
        public SqliteDataAccess()
        {
            //connection to my pas-database
            sqlite = new SQLiteConnection("Data Source=pas-database.db");
            sqlite.Open();

        }



        public int LoadPerson()
            {
            int Rowcount = 0;
            SQLiteCommand cmd = new SQLiteCommand(sqlite);
            cmd.CommandText = "select count (SurName) from person";

            Rowcount = Convert.ToInt32(cmd.ExecuteScalar());


            return Rowcount;
        }

        /*
        public void ReadData()
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite.CreateCommand();
            sqlite_cmd.CommandText = "select * from person";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                Console.WriteLine(myreader);
            }
            sqlite.Close();
        }

        */









        public void AddPerson(Person p)
        {

            SQLiteCommand insertQuery = new SQLiteCommand("insert into person values (@fname, @gname, @height, @gender, @eyecolor)", sqlite);
            insertQuery.Parameters.AddWithValue("@fname", p.SurName);
            insertQuery.Parameters.AddWithValue("@gname", p.GivenName);
            SQLiteParameter param = new SQLiteParameter("@height", p.Height);
            param.SourceColumn = "height";
            //param.Precision = 18;
            //param.Scale = 2;
             insertQuery.Parameters.Add(param);
            //insertQuery.Parameters.AddWithValue("@height", p.Height);
            insertQuery.Parameters.AddWithValue("@gender", p.Gender);
            insertQuery.Parameters.AddWithValue("@eyecolor", p.EyeColor);
            try
            {
                
                int i=insertQuery.ExecuteNonQuery();
                insertQuery.Connection = sqlite;
                System.Windows.MessageBox.Show("Person Added.");
                sqlite.Close();



            }
            catch(Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }


            

    


}
}
