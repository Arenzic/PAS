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

        
        public void ReadData(string surname, string givename, decimal height, string eyecolor, string gender)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;

            sqlite_cmd = sqlite.CreateCommand();
            sqlite_cmd.CommandText = "select * from person";

            if (surname.Length > 0)
            {
                sqlite_cmd.CommandText += " where  SurName like '%" + surname + "%'";
                bool surTrue = true;
            }
            



            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                Person p = new Person();
                p.SurName = myreader;
                
                Search search = new Search();
                search.tempAddToList(myreader);
            }
            sqlite.Close();
        }

        /*
        public DataTable DataTableQuery(String QueryString)
        {
            DataTable Table = null;
            if (Conn1 != null)
            {
                SQLiteCommand Command = new SQLiteCommand(QueryString, Conn1);
                MySqlDataAdapter Adapter = new MySqlDataAdapter(Command);
                Table = new DataTable();
                try
                {
                    Adapter.Fill(Table);


                }
                catch (MySqlException Ex)
                {
                    Console.WriteLine(Ex.Message);
                }

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
