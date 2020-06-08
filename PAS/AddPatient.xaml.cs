using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Xml.Linq;

namespace PAS
{
    /// <summary>
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class AddPatient : Window
    {
        public AddPatient()
        {
            InitializeComponent();
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

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (FNameInput.Text != "" && SurnameInput.Text != "" && HeightInput.Text != "" && GenderCombo.SelectedIndex != -1 && EyeColorCombo.SelectedIndex != -1)
            {
                //assinging to class
                Person p = new Person();
                p.SurName = SurnameInput.Text;
                p.GivenName = FNameInput.Text;

                p.Height = decimal.Parse(HeightInput.Text);

                //combo box selections to string values.
                ComboBoxItem GenderItem = (ComboBoxItem)GenderCombo.SelectedItem;
                p.Gender = GenderItem.Content.ToString();

                ComboBoxItem EyeItem = (ComboBoxItem)EyeColorCombo.SelectedItem;
                p.EyeColor = EyeItem.Content.ToString();

                //launching the addPerson method, and passing it the class person.
                SqliteDataAccess sqlDataA = new SqliteDataAccess();
                sqlDataA.AddPerson(p);

                //reset fields.
                SurnameInput.Text = "";
                FNameInput.Text = "";
                HeightInput.Text = "";
                GenderCombo.Items.Clear();
                EyeColorCombo.Items.Clear();
               
            }
            else
            {
                MessageBox.Show("Please fill required boxes!");
            }

            
        }

       



    }
}
