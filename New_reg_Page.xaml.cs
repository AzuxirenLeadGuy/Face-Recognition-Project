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
namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for New_reg_Page.xaml
    /// </summary>
    public partial class New_reg_Page : Page
    {
        string imguri="";
        public New_reg_Page()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imguri = op.FileName;
                Image2.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            lbl.Content = "Please Wait....";
            int year;
            if (imguri != "" && NameBox.Text != ""
                && int.TryParse(YearBox.Text, out year) &&
                CourseBox.Text != "" && EnrollBox.Text != "" &&
                RollBox.Text != "")
            {
                FRAttendance.Person p = new FRAttendance.Person(NameBox.Text, RollBox.Text,
                    CourseBox.Text, YearBox.Text, EnrollBox.Text, FaceRecognitionDotNet.FaceRecognition.LoadImageFile(imguri));
                FRAttendance.AssetLoad.SavePerson(p);
                lbl.Content = "Student Registration Complete.";
            }
            else
            {
                lbl.Content = "Invalid Input! Please provide all details";
            }
        }
    }
}
