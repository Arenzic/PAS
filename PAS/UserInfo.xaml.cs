using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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

namespace PAS
{
    /// <summary>
    /// Interaction logic for UserInfo.xaml
    /// </summary>
    public partial class UserInfo : Window
    {
        private SqliteDataAccess sqldata = new SqliteDataAccess();
        String userid2 = "";

        public UserInfo(DataRowView source, String userId)
        {
            InitializeComponent();
            

            string queryString = "SELECT * FROM person WHERE id = " + userId + ";";

            SQLiteDataReader reader = sqldata.Query(queryString);
            if(reader.Read())
            {
                setId.Content = reader.GetInt16(0);
                FNameInput.Text = reader.GetString(1);
                SurnameInput.Text = reader.GetString(2);
                //GenderCombo.SelectedItem = reader.GetString(3);
                //Height.Text = reader.GetInt32(4);
                //StatusCombo.SelectedIndex = (int)reader.GetValue(3);
                //EyeColorCombo.SelectedIndex = (int)reader.GetValue(6);
            }

            reader.Close();

            userid2 = userId;
        }

        private void Dashboard_Button(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }


        private void AddPatient_Button(object sender, RoutedEventArgs e)
        {

            AddPatient AddPat = new AddPatient();
            AddPat.Show();
            this.Close();

        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            
            string surname = SurnameInput.Text;
            string givenname = FNameInput.Text;
            decimal height = decimal.Parse(HeightInput.Text);
            

            ComboBoxItem GenderItem = (ComboBoxItem)GenderCombo.SelectedItem;
            string gender = GenderItem.Content.ToString();

            ComboBoxItem EyeItem = (ComboBoxItem)EyeColorCombo.SelectedItem;
            string eyecolor = EyeItem.Content.ToString();

            ComboBoxItem StatusItem = (ComboBoxItem)StatusCombo.SelectedItem;
            string status = StatusItem.Content.ToString();



            String query = "UPDATE person SET SurName = @surname, GivenName = givenname, Height = @height, Gender = @gender, status = @status, EyeColor = @eyecolor WHERE id = " + userid2 + ";";
            sqldata.Update(query, surname, givenname, height, gender, status, eyecolor);

            System.Windows.MessageBox.Show("Details Updated.");
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult r = System.Windows.MessageBox.Show("Are you sure you want to delete this contact?", "Delete", MessageBoxButton.YesNo);

            if (r.Equals(MessageBoxResult.No))
                return;



            string queryDelete = "DELETE FROM person WHERE id = " + userid2 + ";";

            sqldata.Query(queryDelete);
            System.Windows.MessageBox.Show("Person deleted.");

            string query = null;
            Search srch = new Search(query);
            srch.Show();
            this.Close();
        }
    }
}
