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

        //trying to open the connection
        public void Connect()
        {

            try
            {
                sqlite.Open();
            }
            catch (SQLiteException Ex)
            {
                // when breaks
                Console.WriteLine(" Database connection Failed.");
                Console.WriteLine(Ex.Message);
            }
            finally
            {
                if (sqlite.State == System.Data.ConnectionState.Closed)
                    sqlite = null;
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
        //loading the count of Ward for the dashboard 
        public int LoadWardPatients()
        {
            int Rowcount = 0;
            SQLiteCommand cmd = new SQLiteCommand(sqlite);
            cmd.CommandText = "SELECT COUNT (id) FROM person WHERE status = 'Ward'";

            Rowcount = Convert.ToInt32(cmd.ExecuteScalar());


            return Rowcount;
        }
        //loading the count of ICU for the dashboard 
        public int LoadIcuPatients()
        {
            int Rowcount = 0;
            SQLiteCommand cmd = new SQLiteCommand(sqlite);
            cmd.CommandText = "SELECT COUNT (id) FROM person WHERE status = 'ICU'";

            Rowcount = Convert.ToInt32(cmd.ExecuteScalar());


            return Rowcount;
        }
        //loading the count of doctors for the dashboard 
        public int LoadDoctor()
        {
            int Rowcount = 0;
            SQLiteCommand cmd = new SQLiteCommand(sqlite);
            cmd.CommandText = "SELECT COUNT (id) FROM person WHERE status = 'ICU'";

            Rowcount = Convert.ToInt32(cmd.ExecuteScalar());


            return Rowcount;
        }




        public DataTable DataTableQuery(String QueryString)
        {
            DataTable Table = null;
            if (sqlite != null)
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
            if (sqlite != null)
            {

                SQLiteCommand Command = new SQLiteCommand(QueryString, sqlite);


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




        public void Update(string AddString, Person p, bool DoctorPromote)
        {
            if (sqlite != null)
            {
                //determining whether it has the doctor promote value
                if (DoctorPromote)
                {
                    SQLiteCommand promote = new SQLiteCommand(AddString, sqlite);
                    promote.Parameters.AddWithValue("@work", p.Status);

                    try
                    {

                        promote.ExecuteNonQuery();

                    }
                    catch (SQLiteException E)
                    {
                        Console.Error.WriteLine(E.Message);
                    }
                }


                SQLiteCommand Command = new SQLiteCommand(AddString, sqlite);

                Command.Parameters.AddWithValue("@surname", p.SurName);
                Command.Parameters.AddWithValue("@givenname", p.GivenName);
                Command.Parameters.AddWithValue("@height", p.Height);
                Command.Parameters.AddWithValue("@gender", p.Gender);
                Command.Parameters.AddWithValue("@status", p.Status);
                Command.Parameters.AddWithValue("@eyecolor", p.EyeColor);

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
    







    public void AddPerson(Person p, Doctor d, bool isDoctor)
        {
            if (!isDoctor)
            {


                SQLiteCommand insertQuery = new SQLiteCommand("insert into person (SurName, GivenName, Height, Gender, EyeColor, status)values (@sname, @gname, @height, @gender, @eyecolor, @status)", sqlite);


                //passing paramateres securely with add value
                insertQuery.Parameters.AddWithValue("@sname", p.SurName);
                insertQuery.Parameters.AddWithValue("@gname", p.GivenName);
                //SQLiteParameter param = new SQLiteParameter("@height", p.Height);
                //SourceColumn = "height";
                //insertQuery.Parameters.Add(param);
                insertQuery.Parameters.AddWithValue("@height", p.Height);
                insertQuery.Parameters.AddWithValue("@gender", p.Gender);
                insertQuery.Parameters.AddWithValue("@eyecolor", p.EyeColor);
                insertQuery.Parameters.AddWithValue("@status", p.Status);


                try
                {
                    insertQuery.ExecuteNonQuery();
                    System.Windows.MessageBox.Show("Person Added.");
                }

                catch (Exception exc)
                {
                    throw new Exception(exc.Message);
                }
            }





            else
            {
                SQLiteCommand insertQuery = new SQLiteCommand("insert into person (SurName, GivenName, Height, Gender, EyeColor)values (@sname, @gname, @height, @gender, @eyecolor)", sqlite);

                try
                {
                    //passing paramateres securely with add value
                    insertQuery.Parameters.AddWithValue("@sname", d.SurName);
                    insertQuery.Parameters.AddWithValue("@gname", d.GivenName);
                    //SQLiteParameter param = new SQLiteParameter("@height", p.Height);
                    //param.SourceColumn = "height";
                    //insertQuery.Parameters.Add(param);
                    insertQuery.Parameters.AddWithValue("@height", d.Height);
                    insertQuery.Parameters.AddWithValue("@gender", d.Gender);
                    insertQuery.Parameters.AddWithValue("@eyecolor", d.EyeColor);

                    insertQuery.ExecuteNonQuery();
                    insertQuery.Connection = sqlite;

                    //return the new added 
                    SQLiteCommand docIdQuery = new SQLiteCommand("SELECT rowid from person order by ROWID DESC limit 1;", sqlite);
                    int docId = Convert.ToInt32(docIdQuery.ExecuteScalar());

                    SQLiteCommand insertDoc = new SQLiteCommand("insert into doctor (doctorId, work)values (@docId, @work)", sqlite);
                    insertDoc.Parameters.AddWithValue("@docId", docId);
                    insertDoc.Parameters.AddWithValue("@work", d.work);

                    insertDoc.ExecuteNonQuery();
                    insertDoc.Connection = sqlite;


                    System.Windows.MessageBox.Show("Doctor Added.");
                    sqlite.Close();



                }
                catch (Exception exc)
                {
                    throw new Exception(exc.Message);
                }
            }

        }
    }
}
            


            

    

