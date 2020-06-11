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
                DataTable Table = sqlData.DataTableQuery("SELECT * FROM person");
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
            SqliteDataAccess sqlData = new SqliteDataAccess();

            string StringGender = null;
            string StringEyeColor = null;
            decimal DecHeight = 0;
            

            if (GenderCombo.SelectedItem != null)
            {
                ComboBoxItem GenderItem = (ComboBoxItem)GenderCombo.SelectedItem;
                 StringGender = GenderItem.Content.ToString();
            }


            if (EyeColorCombo.SelectedItem != null)
            {
                ComboBoxItem EyeItem = (ComboBoxItem)EyeColorCombo.SelectedItem;
                 StringEyeColor = EyeItem.Content.ToString();
            }

            if(HeightInput.Text != "")
            {
                 DecHeight = decimal.Parse(HeightInput.Text);
            }

           //sqlData.ReadData(FNameInput.Text, SurnameInput.Text, DecHeight, StringEyeColor, StringGender);
            /*
            if(FNameInput.Text != null && FNameInput.Text.Length >=3)
            {
                

            }
            else
            {
                System.Windows.MessageBox.Show("Invalid Input");
                FNameInput.Text = string.Empty;
            }
            */
        }

        private void AddPatient_Button(object sender, RoutedEventArgs e)
        {
            
                AddPatient AddPat = new AddPatient();
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



        /*
        private void SearchResults_Loaded(object sender, RoutedEventArgs e)
        {
            SqliteDataAccess sqlData = new SqliteDataAccess();

            DataTable Table = sqlData.DataTableQuery("select * from person");
            SearchResults.ItemsSource = Table.DefaultView;
        }
        */

        private void SearchResults_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView select = SearchResults.SelectedItem as DataRowView;

            String id = select.Row[0].ToString();

            UserInfo ui = new UserInfo(select, id);
            ui.Show();
            this.Close();
        }
    }
}
