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


            
            SqliteDataAccess sqldataConn = new SqliteDataAccess();
            int people = sqldataConn.LoadPerson();
            int inWard = sqldataConn.LoadWardPatients();
            int inIcu = sqldataConn.LoadIcuPatients();

            //need to use property only (TextboxText), as i cannot bind with a variable.
            totalPersons.DataContext = new TextboxText() { textdata = people };

            totalWard.DataContext = new TextboxText() { textdata = inWard };

            totalIcu.DataContext = new TextboxText() { textdata = inWard };

        }

        private int LoadPatientInfo()
        {
            var nodeCount = 0;
            using (var reader = XmlReader.Create("patients.xml"))
            {
                while (reader.Read())
                {
                    //if the reader finds an element by the name of surname, add one to count.
                    if (reader.NodeType == XmlNodeType.Element &&
                        reader.Name == "SurName")
                    {
                        nodeCount++;
                    }
                }
            }
            return nodeCount;
        }



        public class TextboxText
        {
            public int textdata { get; set; }

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
            string query = "SELECT id, SurName, GivenName, Height, Gender, status, EyeColor FROM person WHERE status = 'ICU'";
            Search se = new Search(query);
            se.Show();
            this.Close();
        }

        private void Ward_Click(object sender, RoutedEventArgs e)
        {
            string query = "SELECT id, SurName, GivenName, Height, Gender, status, EyeColor FROM person WHERE status = 'Ward'";
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
    }
}
