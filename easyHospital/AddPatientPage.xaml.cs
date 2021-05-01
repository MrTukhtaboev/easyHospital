using Newtonsoft.Json;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZXing;

namespace easyHospital
{
    /// <summary>
    /// Interaction logic for CRUD.xaml
    /// </summary>
    public partial class AddPatientPage : Page
    {
        public AddPatientPage()
        {
            InitializeComponent();
            tokenTxt.Focus();
        }

        private void clientDownloaded(object sender, DownloadStringCompletedEventArgs e)
        {
            //try
            //{
                if (e.Result != "false" && !string.IsNullOrEmpty(tokenTxt.Text))
                {
                    var user = JsonConvert.DeserializeObject<User>(e.Result);
                    firstnameTxt.Text = user.firstname;
                    lastnameTxt.Text = user.lastname;
                    middlenameTxt.Text = user.middle_name;
                    birthdayTxt.Text = user.birthday;
                    passportTxt.Text = user.passport;
                    phoneTxt.Text = user.phone;
                    pincodeTxt.Text = user.pin_code;
                    downloadprogressbar.Value = 0;
                    //barcode generator
                    barcodeImg.Source = Functions.QR_CODE_WRITER(tokenTxt.Text);
                    tokenTxt.Focus();
                    tokenTxt.SelectAll();
                } 
                else
                {
                    //MessageBox.Show("There is no this person!");
                    clearTXT();
                }
            //}
            //catch
            //{
            //    //MessageBox.Show("Qaytadan urinib koring!");
            //}
        }

        private void clientDownloading(object sender, DownloadProgressChangedEventArgs e)
        {
            downloadprogressbar.Value = e.ProgressPercentage;
        }
        private void clearTXT()
        {
            //tokenTxt.Clear(); 
            firstnameTxt.Clear();
            lastnameTxt.Clear(); middlenameTxt.Clear();
            birthdayTxt.Text = ""; phoneTxt.Clear();
            pincodeTxt.Clear(); passportTxt.Clear();
            tokenTxt.Focus(); tokenTxt.SelectAll(); 
            //barcodeImg.Source = null;
        }

        private void tokenTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tokenTxt.Text.Length == 10)
            {
                var client = new WebClient();
                client.DownloadProgressChanged += clientDownloading;
                client.DownloadStringCompleted += clientDownloaded;
                client.DownloadStringAsync(new Uri(Functions.API_GET + tokenTxt.Text));
                barcodeImg.Source = Functions.QR_CODE_WRITER(tokenTxt.Text);
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var client = new WebClient();
            NameValueCollection postData = new NameValueCollection()
            {
                { "token", tokenTxt.Text },
                { "firstname", firstnameTxt.Text },
                { "lastname", lastnameTxt.Text },
                { "middle_name", middlenameTxt.Text },
                { "birthday", birthdayTxt.Text },
                { "passport", passportTxt.Text },
                { "phone", phoneTxt.Text },
                { "pin_code", pincodeTxt.Text }
            };
            string pagesource = Encoding.UTF8.GetString(client.UploadValues(Functions.API_POST, "POST", postData));
            MessageBox.Show("It has saved!");
            clearTXT();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var client = new WebClient();
            var response = client.DownloadString(Functions.API_DELETE + tokenTxt.Text);
            MessageBox.Show("It has deleted");
            clearTXT();
        }
    }
}
