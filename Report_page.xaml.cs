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
        internal Report_page()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string[] path = new string[2];
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                path[0] = dialog.SelectedPath;
            }

            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                path[1] = dialog.SelectedPath;
                path_test.Text = path[1];
                
            }

            //File.WriteAllText("path.saf", JsonConvert.SerializeObject(x));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
