using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace PAS
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Window
    {


        
        public Search(string query)
        {
            InitializeComponent();

            SqliteDataAccess sqlData = new SqliteDataAccess();
            if (query == null)
            {
                DataTable Table = sqlData.DataTableQuery("SELECT person.surName, person.GivenName, person.Height, person.Gender, person.status, " +
                    "person.EyeColor, person.id, doctor.doctorId FROM person LEFT JOIN doctor ON person.id = doctor.doctorId");
                SearchResults.ItemsSource = Table.DefaultView;
            }
            else
            {
                DataTable Table = sqlData.DataTableQuery(query);
                SearchResults.ItemsSource = Table.DefaultView;
            }

        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string query = "SELECT * FROM person";
            int queryCount = 0;
            SqliteDataAccess sqlData = new SqliteDataAccess();

            string StringGender = null;
            string StringEyeColor = null;

            if (FNameInput.Text != "")
            {
                //concatenating a query string based on user demands
                if (queryCount < 1)
                {
                    query = query + " WHERE GivenName LIKE '" + FNameInput.Text + "'";
                    queryCount++;
                }
                else
                {
                    query = query + " AND GivenName LIKE '" + FNameInput.Text + "'";
                }
            }

            if (SurnameInput.Text != "")
            {

                if (queryCount < 1)
                {
                    query = query + " WHERE SurName LIKE '" + SurnameInput.Text + "'";
                    queryCount++;
                }
                else
                {
                    query = query + " AND SurName LIKE '" + SurnameInput.Text + "'";
                }
            }

            if (GenderCombo.SelectedItem != null)
            {
                ComboBoxItem GenderItem = (ComboBoxItem)GenderCombo.SelectedItem;
                 StringGender = GenderItem.Content.ToString();
                if(queryCount <1)
                {
                    query = query + " WHERE Gender LIKE '"+ StringGender +"'";
                    queryCount++;
                }
                else
                {
                    query = query + " AND Gender LIKE '" + StringGender + "'";
                }
            }


            if (EyeColorCombo.SelectedItem != null)
            {
                ComboBoxItem EyeItem = (ComboBoxItem)EyeColorCombo.SelectedItem;
                 StringEyeColor = EyeItem.Content.ToString();

                if (queryCount < 1)
                {
                    query = query + " WHERE EyeColor LIKE '" + StringEyeColor + "'";
                    queryCount++;
                }
                else
                {
                    query = query + " AND EyeColor LIKE '" + StringEyeColor + "'";
                    queryCount++;
                }
            }

            if(HeightInput.Text != "")
            {
                string height = HeightInput.Text;
                string decHeight = height.ToString(System.Globalization.CultureInfo.InvariantCulture);

                if (queryCount < 1)
                {
                    query = query + " WHERE Height LIKE '" + decHeight + "'";
                    queryCount++;
                }
                else
                {
                    query = query + " AND Height LIKE '" + decHeight + "'";
                }
            }

            //returning the results to the datatable
            DataTable Table = sqlData.DataTableQuery(query);
            SearchResults.ItemsSource = Table.DefaultView;


        }

        private void AddPatient_Button(object sender, RoutedEventArgs e)
        {
            
                AddPatient AddPat = new AddPatient(false);
                AddPat.Show();
                this.Close();
            
        }

        private void Dashboard_Button(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            FNameInput.Text = "";
            SurnameInput.Text = "";
        }



    

        private void SearchResults_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //determining the selected result, and passing the id into the user info.
            DataRowView select = SearchResults.SelectedItem as DataRowView;

            try
            {
                string id = select.Row[6].ToString();
                string doctorId = select.Row[7].ToString();
               
                //id paramater
                UserInfo ui = new UserInfo(select, id, doctorId);
                ui.Show();
                this.Close();
            }
            catch (Exception exc)
            {
                System.Windows.MessageBox.Show("Please select a valid record!");
                throw new Exception(exc.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string query = "SELECT id, SurName, GivenName, Height, Gender, status, EyeColor FROM person WHERE status = 'ICU' OR status = 'Ward'";
            Search se = new Search(query);
            se.Show();
            this.Close();
        }

       

    }
}
