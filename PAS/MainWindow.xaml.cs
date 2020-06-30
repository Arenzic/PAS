using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace PAS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


           //setting the values of speicifed fields in the database
            SqliteDataAccess sqldataConn = new SqliteDataAccess();
            int doctors = sqldataConn.LoadDoctor();
            int inWard = sqldataConn.LoadWardPatients();
            int inIcu = sqldataConn.LoadIcuPatients();
            int totalP = sqldataConn.LoadPerson();

            //need to use property only (TextboxText), as i cannot bind with a variable.
            totalDoctors.DataContext = new TextboxText() { queryValue = doctors };

            totalWard.DataContext = new TextboxText() { queryValue = inWard };

            totalIcu.DataContext = new TextboxText() { queryValue = inIcu };

            totalPeople.DataContext = new TextboxText() { queryValue = totalP };

        }



        //used for setting the values of database 
        public class TextboxText
        {
            public int queryValue { get; set; }

        }



        private void AddPatient_Button(object sender, RoutedEventArgs e)
        {
            AddPatient AddPat = new AddPatient(false);
            AddPat.Show();
            this.Close();
        }

        //dashboard buttons that pass the query as a parameter, if it needs to search a given group.
        private void ICU_Button(object sender, RoutedEventArgs e)
        {
            string query = "SELECT person.surName, person.GivenName, person.Height, person.Gender, person.status, " +
                    "person.EyeColor, person.id, doctor.doctorId " +
                    "FROM person LEFT JOIN doctor ON person.id = doctor.doctorId WHERE status = 'ICU'";
            Search se = new Search(query);
            se.Show();
            this.Close();
        }

        private void Ward_Click(object sender, RoutedEventArgs e)
        {
            string query = "SELECT person.surName, person.GivenName, person.Height, person.Gender, person.status, " +
                    "person.EyeColor, person.id, doctor.doctorId FROM person LEFT JOIN doctor " +
                    "ON person.id = doctor.doctorId WHERE status = 'Ward'";
            Search se = new Search(query);
            se.Show();
            this.Close();
        }

        private void allPatients_Button(object sender, RoutedEventArgs e)
        {
            String query = null;
            Search se = new Search(query);
            se.Show();
            this.Close();
        }

        private void AddDoctor_Button(object sender, RoutedEventArgs e)
        {
            AddPatient AddPat = new AddPatient(true);
            AddPat.Show();
            this.Close();
        }

        private void doctors_Button(object sender, RoutedEventArgs e)
        {
            string query = "SELECT person.SurName, person.givenName, person.Height, person.Gender, doctor.work, person.EyeColor, person.id, doctor.doctorId FROM person " +
                "INNER JOIN doctor ON person.id = doctor.doctorId; ";
            Search se = new Search(query);
            se.Show();
            this.Close();
        }
    }
}
