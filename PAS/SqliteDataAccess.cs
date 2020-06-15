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
            Connect();

        }

        public SQLiteConnection Conn1 { get => sqlite; set => sqlite = value; }

        public void Connect()
        {


            string ConnectionString = "Data Source=pas-database.db";
            Conn1 = new SQLiteConnection(ConnectionString);
            try
            {
                Conn1.Open();
            }
            catch (SQLiteException Ex)
            {
                // when breaks
                Console.WriteLine(" Database connection Failed.");
                Console.WriteLine(Ex.Message);
            }
            finally
            {
                if (Conn1.State == System.Data.ConnectionState.Closed)
                    Conn1 = null;
            }

        }


        //loading different search screens based on the button click, slight changes in query (inefficient?)
        public int LoadPerson()
            {
            int Rowcount = 0;
            SQLiteCommand cmd = new SQLiteCommand(sqlite);
            cmd.CommandText = "SELECT COUNT (id) FROM person";

            Rowcount = Convert.ToInt32(cmd.ExecuteScalar());


            return Rowcount;
        }

        public int LoadWardPatients()
        {
            int Rowcount = 0;
            SQLiteCommand cmd = new SQLiteCommand(sqlite);
            cmd.CommandText = "SELECT COUNT (id) FROM person WHERE status = 'Ward'";

            Rowcount = Convert.ToInt32(cmd.ExecuteScalar());


            return Rowcount;
        }

        public int LoadIcuPatients()
        {
            int Rowcount = 0;
            SQLiteCommand cmd = new SQLiteCommand(sqlite);
            cmd.CommandText = "SELECT COUNT (id) FROM person WHERE status = 'ICU'";

            Rowcount = Convert.ToInt32(cmd.ExecuteScalar());


            return Rowcount;
        }

        /*
        public void ReadData(string surname, string givename, decimal height, string eyecolor, string gender)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;

            sqlite_cmd = sqlite.CreateCommand();
            sqlite_cmd.CommandText = "select * from person";

            if (surname.Length > 0)
            {
                sqlite_cmd.CommandText += " where  SurName like '%" + surname + "%'";
               // bool surTrue = true;
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
        */

        public DataTable DataTableQuery(String QueryString)
        {
            DataTable Table = null;
            if (Conn1 != null)
            {
                //creating a table and then filling it with the query info
                SQLiteCommand Command = new SQLiteCommand(QueryString, sqlite);
                SQLiteDataAdapter Adapter = new SQLiteDataAdapter(Command);
                Table = new DataTable();
                try
                {
                    Adapter.Fill(Table);


                }
                catch (SQLiteException Ex)
                {
                    Console.WriteLine(Ex.Message);
                }

            }

            return Table;
        }

        public SQLiteDataReader Query(string QueryString)
        {


            SQLiteDataReader Reader = null;
            if (Conn1 != null)
            {

                SQLiteCommand Command = new SQLiteCommand(QueryString, Conn1);


                try
                {
                    Reader = Command.ExecuteReader();
                }
                catch (SQLiteException Ex)
                {
                    Console.Error.WriteLine("Error executing query");
                    Console.WriteLine(Ex.Message);
                }
            }
            return Reader;
        }




        public void Update(string AddString, string SurName, string GivenName, decimal Height, string gender, string status, string EyeColor)
        {
            if (sqlite != null)
            {

                SQLiteCommand Command = new SQLiteCommand(AddString, sqlite);

                Command.Parameters.AddWithValue("@surname", SurName);
                Command.Parameters.AddWithValue("@givenname", GivenName);
                Command.Parameters.AddWithValue("@height", Height);
                Command.Parameters.AddWithValue("@gender", gender);
                Command.Parameters.AddWithValue("@status", status);
                Command.Parameters.AddWithValue("@eyecolor", EyeColor);

                try
                {

                    Command.ExecuteNonQuery();

                }
                catch (SQLiteException E)
                {
                    Console.Error.WriteLine(E.Message);
                }
            }
        }






        public void AddPerson(Person p)
        {

            SQLiteCommand insertQuery = new SQLiteCommand("insert into person (SurName, GivenName, Height, Gender, EyeColor, status)values (@sname, @gname, @height, @gender, @eyecolor, @status)", sqlite);
            
            try
            {
                //passing paramateres securely with add value
                insertQuery.Parameters.AddWithValue("@sname", p.SurName);
                insertQuery.Parameters.AddWithValue("@gname", p.GivenName);
                SQLiteParameter param = new SQLiteParameter("@height", p.Height);
                param.SourceColumn = "height";
                //param.Precision = 18;
                //param.Scale = 2;
                insertQuery.Parameters.Add(param);
                //insertQuery.Parameters.AddWithValue("@height", p.Height);
                insertQuery.Parameters.AddWithValue("@gender", p.Gender);
                insertQuery.Parameters.AddWithValue("@eyecolor", p.EyeColor);
                insertQuery.Parameters.AddWithValue("@status", p.Status);

                insertQuery.ExecuteNonQuery();
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
