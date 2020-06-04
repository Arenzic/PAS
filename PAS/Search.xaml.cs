using System;
using System.Collections.Generic;
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
        public Search()
        {
            InitializeComponent();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(FNameInput.Text != null && FNameInput.Text.Length >=3)
            {
                

            }
            else
            {
                System.Windows.MessageBox.Show("Invalid Input");
                FNameInput.Text = string.Empty;
            }
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
            FSurnameInput.Text = "";
        }
    }
}
