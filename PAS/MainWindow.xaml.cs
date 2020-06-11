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
            int inPatient = sqldataConn.LoadInPatients();
            //need to use property only (TextboxText), as i cannot bind with a variable.
            totalPersons.DataContext = new TextboxText() { textdata = people };

            totalInPatients.DataContext = new TextboxText() { textdata = inPatient };

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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String query = null;
            Search se = new Search(query);
            se.Show();
            this.Close();
        }

        public class TextboxText
        {
            public int textdata { get; set; }

        }



        private void AddPatient_Button(object sender, RoutedEventArgs e)
        {
            AddPatient AddPat = new AddPatient();
            AddPat.Show();
            this.Close();
        }

        private void Inpatients_Button(object sender, RoutedEventArgs e)
        {
            string query = "SELECT * FROM person WHERE status = 'ICU' OR status = 'Ward'";
            Search se = new Search(query);
            se.Show();
            this.Close();
        }
    }
}
