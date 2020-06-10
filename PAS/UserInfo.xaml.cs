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
        public UserInfo(DataRowView source, String userId)
        {
            InitializeComponent();

            string queryString = "Select * from person where id = " + userId + ";";

            SQLiteDataReader reader = sqldata.Query(queryString);
            if(reader.Read())
            {
                FNameInput.Text = reader.GetString(1);
                SurnameInput.Text = reader.GetString(2);
                GenderCombo
                //Height.Text = reader.GetInt32(4);
                //StatusCombo.SelectedIndex = (int)reader.GetValue(5);
                //EyeColorCombo.SelectedIndex = (int)reader.GetValue(6);
            }
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

        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
