using System;
using System.Collections.Generic;
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
    /// Interaction logic for BarcodeEncoderPage.xaml
    /// </summary>
    public partial class BarcodeEncoderPage : Page
    {
        public BarcodeEncoderPage()
        {
            InitializeComponent();
        }

        private void EncodeBtn_Click(object sender, RoutedEventArgs e)
        {
            BarcodeValueImg.Source = Functions.QR_CODE_WRITER(EncodeTxt.Text);
        }

        private void DecodeBtn_Click(object sender, RoutedEventArgs e)
        {
            DecodeTxt.Text = Functions.QR_CODE_READER(BarcodeValueImg);
        }
    }
}
