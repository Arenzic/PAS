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
        public AddPatient(bool isDoctor)
        {
            InitializeComponent();
            if (isDoctor)
            {
                string test = statusText.Text;
                statusText.Text = "Work";
            }
            
            
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
            if (statusText.Text != "Work")
            {
                if (FNameInput.Text != "" && SurnameInput.Text != "" && HeightInput.Text != "" && GenderCombo.SelectedIndex != -1 && EyeColorCombo.SelectedIndex != -1 && StatusCombo.SelectedIndex != -1)
                {
                    bool doctor = false;
                    //assinging to class
                    Person p = new Person();
                    Doctor d = new Doctor();
                    p.SurName = SurnameInput.Text;
                    p.GivenName = FNameInput.Text;


                    p.Height = decimal.Parse(HeightInput.Text.Replace('.', ','));

                    //combo box selections to string values.
                    ComboBoxItem GenderItem = (ComboBoxItem)GenderCombo.SelectedItem;
                    p.Gender = GenderItem.Content.ToString();

                    ComboBoxItem EyeItem = (ComboBoxItem)EyeColorCombo.SelectedItem;
                    p.EyeColor = EyeItem.Content.ToString();

                    ComboBoxItem StatusItem = (ComboBoxItem)StatusCombo.SelectedItem;
                    p.Status = StatusItem.Content.ToString();


                    //launching the addPerson method, and passing it the class person.
                    SqliteDataAccess sqlDataA = new SqliteDataAccess();
                    sqlDataA.AddPerson(p,d,doctor);

                    //reset fields.
                    SurnameInput.Text = "";
                    FNameInput.Text = "";
                    HeightInput.Text = "";
                    GenderCombo.Items.Clear();
                    EyeColorCombo.Items.Clear();
                    StatusCombo.Items.Clear();

                }
                else
                {
                    MessageBox.Show("Please fill required boxes!");
                }
            }
            else
            {
                if (FNameInput.Text != "" && SurnameInput.Text != "" && HeightInput.Text != "" && GenderCombo.SelectedIndex != -1 && EyeColorCombo.SelectedIndex != -1 && StatusCombo.SelectedIndex != -1)
                {
                    bool doctor = true;
                    //assinging to class
                    Person p = new Person();
                    Doctor d = new Doctor();
                    d.SurName = SurnameInput.Text;
                    d.GivenName = FNameInput.Text;


                    d.Height = decimal.Parse(HeightInput.Text.Replace('.', ','));

                    //combo box selections to string values.
                    ComboBoxItem GenderItem = (ComboBoxItem)GenderCombo.SelectedItem;
                    d.Gender = GenderItem.Content.ToString();

                    ComboBoxItem EyeItem = (ComboBoxItem)EyeColorCombo.SelectedItem;
                    d.EyeColor = EyeItem.Content.ToString();

                    ComboBoxItem WorkItem = (ComboBoxItem)StatusCombo.SelectedItem;
                    d.work = WorkItem.Content.ToString();


                    //launching the addPerson method, and passing it the class person.
                    SqliteDataAccess sqlDataA = new SqliteDataAccess();
                    sqlDataA.AddPerson(p,d,doctor);

                    //reset fields.
                    SurnameInput.Text = "";
                    FNameInput.Text = "";
                    HeightInput.Text = "";
                    GenderCombo.Items.Clear();
                    EyeColorCombo.Items.Clear();
                    StatusCombo.Items.Clear();

                }
                else
                {
                    MessageBox.Show("Please fill required boxes!");
                }
            }

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string query = "SELECT * FROM person";
            Search se = new Search(query);
            se.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string query = "SELECT * FROM person WHERE status = 'ICU' OR status = 'Ward'";
            Search se = new Search(query);
            se.Show();
            this.Close();
        }
    }
}
