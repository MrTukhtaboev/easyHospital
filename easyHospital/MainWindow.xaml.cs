using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void addDoctorBtn_Click(object sender, RoutedEventArgs e)
        {
            _frame.Content = new AddDoctorPage();
        }

        private void addPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            _frame.Content = new AddPatientPage();
        }

        private void doctorsBtn_Click(object sender, RoutedEventArgs e)
        {
            _frame.Content = new DoctorsPage();
        }

        private void patientsBtn_Click(object sender, RoutedEventArgs e)
        {
            _frame.Content = new UsersPage();
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            mainTabcontrol.SelectedIndex = 0;
            //loginTxt.Clear();
            passwordTxt.Clear();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                this.Dispatcher.Invoke(async () =>
                {
                    if (string.IsNullOrEmpty(loginTxt.Text) || string.IsNullOrEmpty(passwordTxt.Password))
                    {
                        loginActionMessageTxt.Text = "Iltimos maydonlarni to'ldiring!";
                        loginTxt.Focus();
                    } 
                    else
                    {
                        progressStackPanel.Visibility = Visibility.Visible;
                        loginStackPanel.IsEnabled = false;

                        var client = new WebClient();
                        var result = await client.DownloadStringTaskAsync(new Uri(Functions.API_GET_ADMIN + loginTxt.Text + "/" + passwordTxt.Password));
                        if (result != "false")
                        {
                            var ob = JsonConvert.DeserializeObject<Dictionary<string, Admin>>(result);
                            //
                            loginActionMessageTxt.Text = "Hurmatli mijoz, tizimga kirish uchun login va parolingizni kiriting.";
                            loginActionMessageTxt.Foreground = System.Windows.Media.Brushes.Black;
                            loginActionMessageTxt.Opacity = 0.4;
                            //
                            if (ob["admin"].status == Status.admin)
                            {
                                addDoctorBtn.Visibility = Visibility.Collapsed;
                                doctorsBtn.Visibility = Visibility.Collapsed;
                                mainTabcontrol.SelectedIndex = 1;
                            }
                            else if (ob["admin"].status == Status.creator)
                            {
                                addDoctorBtn.Visibility = Visibility.Visible;
                                doctorsBtn.Visibility = Visibility.Visible;
                                mainTabcontrol.SelectedIndex = 1;
                            }
                        }
                        else
                        {
                            loginActionMessageTxt.Text = "Login yoki parol noto'gri";
                            loginActionMessageTxt.Foreground = System.Windows.Media.Brushes.Red;
                            loginActionMessageTxt.Opacity = 1;
                            loginTxt.Focus();
                            loginTxt.SelectAll();
                        }

                        progressStackPanel.Visibility = Visibility.Collapsed;
                        loginStackPanel.IsEnabled = true;
                    }
                    
                });
            });
            thread.Start();

        }

        private void passwordTxt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginBtn_Click(sender, null);
            }
        }

        private void loginTxt_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                passwordTxt.Focus();
                passwordTxt.SelectAll();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loginTxt.Focus();
        }
    }
}
