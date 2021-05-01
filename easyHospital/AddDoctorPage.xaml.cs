using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace easyHospital
{
    /// <summary>
    /// Interaction logic for AddDoctorPage.xaml
    /// </summary>
    public partial class AddDoctorPage : Page
    {
        public AddDoctorPage()
        {
            InitializeComponent();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var client = new WebClient();
            NameValueCollection postData = new NameValueCollection()
            {
                { "login", loginTxt.Text },
                { "password", passwordTxt.Text }
            };
            string pagesource = Encoding.UTF8.GetString(client.UploadValues(Functions.API_DELETE_DOCTOR, postData));
            if(pagesource == "false")
            {
                MessageBox.Show("there is no person!");
            } 
            else
            {
                MessageBox.Show("it has deleted!");
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var client = new WebClient();
            NameValueCollection postData = new NameValueCollection()
            {
                { "login", loginTxt.Text },
                { "password", passwordTxt.Text },
                { "firstname", firstnameTxt.Text },
                { "lastname", lastnameTxt.Text},
                { "job", jobTxt.Text },
                { "phone", phoneTxt.Text },
                { "address", addressTxt.Text }
            };
            string pagesource = Encoding.UTF8.GetString(client.UploadValues(Functions.API_POST_DOCTOR, postData));
            if (pagesource == "false")
            {
                MessageBox.Show("there is exists!");
            }
            else
            {
                MessageBox.Show("it has saved!");
            }
        }
    }
}
