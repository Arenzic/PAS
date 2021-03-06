﻿using System;
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

        public UserInfo(DataRowView source, String userId, string doctorId)
        {
            InitializeComponent();



            string queryString = "SELECT * FROM person WHERE id = " + userId + ";";

            SQLiteDataReader reader = sqldata.Query(queryString);
            if (reader.Read())
            {

                if (doctorId == "")
                {
                    string fullname = reader.GetString(2) + " " + reader.GetString(1) + "- Patient";
                    fullName.Content = fullname;
                }
                else
                {
                    string fullname = reader.GetString(2) + " " + reader.GetString(1) + " - Doctor";
                    fullName.Content = fullname;
                    statusText.Text = "Work";
                    promDoctor_button.Visibility = Visibility.Hidden;
                }

                setId.Content = reader.GetInt16(0);
                SurnameInput.Text = reader.GetString(1);
                FNameInput.Text = reader.GetString(2);

                string gender = reader.GetString(4);

                if (gender == "male")
                {
                    GenderCombo.SelectedIndex = 0;
                }
                else if (gender == "female")
                {
                    GenderCombo.SelectedIndex = 1;
                }

                HeightInput.Text = reader.GetDecimal(3).ToString();

                //if the position is not equivelant to a db null, assign.
                if (reader.IsDBNull(5))
                {
                    StatusCombo.SelectedIndex = -1;
                }
                else
                {


                    string status = reader.GetString(5);


                    if (status == "Ward")
                    {
                        StatusCombo.SelectedIndex = 0;
                    }
                    else if (status == "ICU")
                    {
                        StatusCombo.SelectedIndex = 1;
                    }
                }

                string eyeColor = reader.GetString(6);

                switch (eyeColor)
                {
                    case "green":
                        EyeColorCombo.SelectedIndex = 0;
                        break;
                    case "brown":
                        EyeColorCombo.SelectedIndex = 1;
                        break;
                    case "gray":
                        EyeColorCombo.SelectedIndex = 2;
                        break;
                    case "blue":
                        EyeColorCombo.SelectedIndex = 3;
                        break;
                    default:
                        EyeColorCombo.SelectedIndex = -1;
                        break;

                }
            }


            reader.Close();

            //setting the id to the local string
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

            AddPatient AddPat = new AddPatient(false);
            AddPat.Show();
            this.Close();

        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            //bool doctor prom is used to determine which query it needs, doctor/patient
            bool DoctorProm = false;
            Person p = new Person();
            
            p.SurName = SurnameInput.Text;
            p.GivenName = FNameInput.Text;
            p.Height = decimal.Parse(HeightInput.Text.Replace('.', ','));


            ComboBoxItem GenderItem = (ComboBoxItem)GenderCombo.SelectedItem;
            p.Gender = GenderItem.Content.ToString();

            ComboBoxItem EyeItem = (ComboBoxItem)EyeColorCombo.SelectedItem;
            p.EyeColor = EyeItem.Content.ToString();

            ComboBoxItem StatusItem = (ComboBoxItem)StatusCombo.SelectedItem;
            p.Status = StatusItem.Content.ToString();



            String query = "UPDATE person SET SurName = @surname, GivenName = givenname, Height = @height, Gender = @gender, status = @status, EyeColor = @eyecolor WHERE id = " + userid2 + ";";
            sqldata.Update(query,p, DoctorProm);

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
            Search se = new Search(query);
            se.Show();
            this.Close();
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

        private void Prom_Button_Click(object sender, RoutedEventArgs e)
        {

            bool DoctorProm = true;
            Person p = new Person();

            ComboBoxItem StatusItem = (ComboBoxItem)StatusCombo.SelectedItem;
            p.Status = StatusItem.Content.ToString();

            string promQuery = "INSERT INTO doctor(doctorId, work) values (" + userid2 + ", @work)";
            sqldata.Update(promQuery, p, DoctorProm);
            

            //resetting the person class fields to the buttons
            p.SurName = SurnameInput.Text;
            p.GivenName = FNameInput.Text;
            p.Height = decimal.Parse(HeightInput.Text.Replace('.', ','));


            ComboBoxItem GenderItem = (ComboBoxItem)GenderCombo.SelectedItem;
            p.Gender = GenderItem.Content.ToString();

            ComboBoxItem EyeItem = (ComboBoxItem)EyeColorCombo.SelectedItem;
            p.EyeColor = EyeItem.Content.ToString();


            p.Status = "";
            DoctorProm = false;
            //updating the status
            String query = "UPDATE person SET SurName = @surname, GivenName = givenname, Height = @height, Gender = @gender, status = @status, EyeColor = @eyecolor WHERE id = " + userid2 + ";";
            sqldata.Update(query, p, DoctorProm);

            System.Windows.MessageBox.Show("Person updated to Doctor.");
        }
    }
}
