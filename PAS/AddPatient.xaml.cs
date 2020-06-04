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
            if (FNameInput.Text != "" && SurnameInput.Text != "")
            {
                Person p = new Person();
                p.SurName = SurnameInput.Text;
                p.GivenName = FNameInput.Text;
                //p.Height = Height.te

                ComboBoxItem GenderItem = (ComboBoxItem)GenderCombo.SelectedItem;
                p.Gender = GenderItem.Content.ToString();

                ComboBoxItem EyeItem = (ComboBoxItem)EyeColorCombo.SelectedItem;
                p.EyeColor = EyeItem.Content.ToString();

               
            }
            else
            {
                MessageBox.Show("Please fill required boxes!");
            }

            
        }

       



    }
}
