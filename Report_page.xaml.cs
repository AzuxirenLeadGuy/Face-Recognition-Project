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
using FRAttendance;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;
namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Report_page.xaml
    /// </summary>
    public partial class Report_page : Page
    {
        internal string[] x=new string[3];
        internal Report_page()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.Description = "Please Select Folder for the downloaded Face DB";
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                x[0] = dialog.SelectedPath;
                path_test.Text = x[0];
            }

            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.Description = "Please Select Folder for the Stored Student Files";
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                x[1] = dialog.SelectedPath;
                path_test.Text = x[1];

            }
            try
            {
                Common.Init(x[0]);
                var sp = x[1] + @"\Students";
                if(!Directory.Exists(sp))
                    Directory.CreateDirectory(sp);
                sp = x[1] + @"\SubjectLoad";
                if(!Directory.Exists(sp))
                    Directory.CreateDirectory(sp);
                AssetLoad.AssetURI = x[1];
                File.WriteAllText("path.saf", JsonConvert.SerializeObject(x));
                
            }
            catch
            {
                path_test.Text = "Invalid Folder!!. Please Try again!";
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.Description = "Please Select Folder for the downloaded Face DB";
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                x[2] = dialog.SelectedPath;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new First());
        }
    }
}
