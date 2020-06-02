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
                //parsing height from string to decimal
                String play = HeightInput.Text;
                decimal test = decimal.Parse(play, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo);


                //combo box selected items to strings
                string genderChosen = GenderCombo.Text;
                string eyeColor = EyeColorCombo.Text;

                Console.WriteLine(FNameInput.Text);
                Console.WriteLine(SurnameInput.Text);
                Console.WriteLine(test);
                Console.WriteLine(genderChosen);
                Console.WriteLine(eyeColor);
                // Append(FNameInput.Text, SurnameInput.Text, heightValue, GenderCombo.SelectedItem, EyeColorCombo.SelectedItem);
            }
            else
            {
                MessageBox.Show("Please fill required boxes!");
            }

            
        }

        public static void Append( string FNameInput, string SurnameInput, double HeightInput, string GenderInput, string EyeColorInput)
        {
                
            //creating a new X element class with elements
            var person = new XElement("Person",
                new XElement("SurName", FNameInput),
                new XElement("GivenName", SurnameInput),
                new XElement("Height", HeightInput),
                new XElement("Gender", GenderInput),
                new XElement("EyeColor", EyeColorInput));

            //load the document and add the person
            var doc = new XDocument();
            doc = XDocument.Load("patients.xml");
            doc.Element("Person").Add(person);
                
            // initalises the new instance and saves the XDocument.
            doc = new XDocument(new XElement("Person", person));
            doc.Save("patients.xml");
        }



    }
}
