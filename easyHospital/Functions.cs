using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZXing;

namespace easyHospital
{
    class Functions
    {
        //server
        public static readonly string API = "https://dnet.uz/api/";
        //user
        public static readonly string API_POST = API + "postuserdata";
        public static readonly string API_GET = API + "getuserdata/";
        public static readonly string API_DELETE = API + "deleteuserdata/";
        //doctor
        public static readonly string API_POST_DOCTOR = API + "postdoctordata";
        public static readonly string API_DELETE_DOCTOR = API + "deletedoctordata";
        //admin
        public static readonly string API_GET_ADMIN = API + "login/";
        public static readonly string API_GET_DOCTORS = API + "getdoctors";
        public static readonly string API_GET_USERS = API + "getusers";

        public static ImageSource QR_CODE_WRITER(string text)
        {
            BarcodeWriter writer = new BarcodeWriter() { Format = BarcodeFormat.QR_CODE };
            //BarcodeValueImg.Source
            var bmp = writer.Write(text);
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(),
                                  IntPtr.Zero,
                                  Int32Rect.Empty,
                                  BitmapSizeOptions.FromEmptyOptions());
            return bitmapSource;
        }

        public static string QR_CODE_READER(Image image)
        {
            BarcodeReader reader = new BarcodeReader();
            ImageSource img = image.Source;
            BitmapSource bmp = (BitmapSource)img;
            return reader.Decode(bmp).Text;
        }
    }
}
