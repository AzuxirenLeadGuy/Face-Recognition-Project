using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation; 
using System.Windows.Shapes;
using FaceRecognitionDotNet;
namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Attendance.xaml
    /// This is required
    /// </summary>
    public partial class Attendance : Page
    {
        string pathuri;
        public Attendance()
        {
            InitializeComponent();
            pathuri = "";
        }
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                pathuri = op.FileName;
                Image1.Source = new BitmapImage(new Uri(op.FileName));
                if (FRAWPF.Test.LoadFaces != null && FRAWPF.Test.LoadFaces.IsAlive)
                {
                    FRAWPF.Test.LoadFaces.Abort();
                }
                FRAWPF.Test.EncodingsLoaded = false;
                FRAWPF.Test.LoadFaces = new System.Threading.Thread(Load);
                FRAWPF.Test.LoadFaces.Start();
            }
            void Load()
            {
                FRAWPF.Test.Faces = FRAttendance.Common.fr.FaceEncodings
                    (FaceRecognition.LoadImageFile(pathuri)).ToArray();
                FRAWPF.Test.EncodingsLoaded = true;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (pathuri != "")
                this.NavigationService.Navigate(new FRAWPF.Test());
            else
                Lbl.Content = "Please Load a photo for attendance";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new First());
        }
    }
}
