using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS
{
    public class SqliteDataAccess
    {
        public static List<Person> LoadPeople()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Person>("select * from person", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SavePerson(Person person)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into person(SurName, GivenName, Height, Gender,EyeColor) values (@SurName, @GivenName, @Height, @Gender, @EyeColor", person);
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            //look for all connections strings with id of default
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
