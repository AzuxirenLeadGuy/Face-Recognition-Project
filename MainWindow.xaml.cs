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
using Newtonsoft.Json;
using System.IO;
namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string[] x = new string[2];
            try
            {
                x = JsonConvert.DeserializeObject<string[]>(File.ReadAllText("path.saf"));
                FRAttendance.Common.Init(x[0]);
                FRAttendance.AssetLoad.AssetURI = x[1];
                main.NavigationService.Navigate(new First());
            }
            catch
            {
                main.NavigationService.Navigate(new Report_page());
            }
        }

        private void Main_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
