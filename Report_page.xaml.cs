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
            string[] x = new string[2];
            var of=new OpenFileDialog();
            of.Title = "Select the Directory for Face Database";
            of.Filter = "Directory |";
            if(of.ShowDialog() == true)
            {

            }
            of = new OpenFileDialog();
            of.Title = "Select the Directory for File Storage";
            of.Filter = "Directory |";
            if (of.ShowDialog() == true)
            {

            }
            File.WriteAllText("path.saf", JsonConvert.SerializeObject(x));
        }
    }
}
