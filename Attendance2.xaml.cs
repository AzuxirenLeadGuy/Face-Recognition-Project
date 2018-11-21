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
using FRAttendance;
namespace FRAWPF
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Page
    {
        public Test()
        {
            InitializeComponent();
        }
        public static FaceEncoding[] Faces;
        public static System.Threading.Thread LoadFaces;
        public static bool EncodingsLoaded;
        internal Subject sub;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new WpfApp2.Att_report(sub.TakeAttendance(Faces))); ;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a File";
            op.Filter = "JSON files|*.uafs";
            op.InitialDirectory = FRAttendance.AssetLoad.AssetURI+@"\SubjectLoad\";

            if (op.ShowDialog() == true)
            {
               Attend_button.Content = op.FileName;
                sub = AssetLoad.SubjectLoad(op.FileName);
                if(LoadFaces.IsAlive){LoadFaces.Join();}
                SubmitButton.IsEnabled = true;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new WpfApp2.First());
        }
    }
}
