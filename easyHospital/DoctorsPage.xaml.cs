using easyHospital.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for DoctorsPage.xaml
    /// </summary>
    public partial class DoctorsPage : Page
    {
        public DoctorsPage()
        {
            InitializeComponent();
        }
        //private List<Doctor> listDoctors = new List<Doctor>();
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var client = new WebClient();
            var res = await client.DownloadStringTaskAsync(new Uri(Functions.API_GET_DOCTORS));
            listDoctosDataGrid.ItemsSource = JsonConvert.DeserializeObject<List<Doctor>>(res);
        }
    }
}
