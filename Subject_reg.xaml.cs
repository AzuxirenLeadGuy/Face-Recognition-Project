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
    /// Interaction logic for Subject_reg.xaml
    /// </summary>
    public partial class Subject_reg : Page
    {
        public Subject_reg()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog op = new OpenFileDialog();
            op.Multiselect = true;
            op.Title = "Select a File";
            op.Filter = "JSON files|*.uafp";
            op.InitialDirectory = FRAttendance.AssetLoad.AssetURI+@"\Students\";
            if (op.ShowDialog() == true)
            {
                var x=op.FileNames.Length;
                var p = op.FileNames;
                for(int i=0;i<x;i++)
                {
                    FRAttendance.Person pp = FRAttendance.AssetLoad.PersonLoad(p[i]);
                    StudentList.Items.Insert(i,pp.roll);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Lbl.Content = "Please wait...";
            int year;
            if(CCodeBox.Text!=""&&CTitleBox.Text!=""&&FBox.Text!=""&&!StudentList.Items.IsEmpty&&int.TryParse(YBox.Text,out year))
            {
                var loader = new FRAttendance.SubjectLoader();
                loader.Code = CCodeBox.Text;
                loader.Name = CTitleBox.Text;
                loader.Fname = FBox.Text;
                loader.year = year;
                var l = StudentList.Items;
                loader.StudentRolls = new string[l.Count];
                year = 0;
                foreach(string s in l){loader.StudentRolls[year++] = s;}
                FRAttendance.AssetLoad.SaveSubject(loader);
                Lbl.Content = "Subject Data Stored Successfully";
            }
            else
            {
                Lbl.Content = "Invalid Data! Please submit valid details.";
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            StudentList.Items.RemoveAt(StudentList.SelectedIndex);
        }

        private void StudentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StudentList.SelectedIndex != -1) { RemoveSelect.IsEnabled = true; }
            else { RemoveSelect.IsEnabled = false; }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new First());
        }
    }
}
