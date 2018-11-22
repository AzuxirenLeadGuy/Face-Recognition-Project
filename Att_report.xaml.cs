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
using Newtonsoft.Json;
namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Att_report.xaml
    /// </summary>
    public partial class Att_report : Page
    {
        AttendanceReport report;
        internal Att_report(AttendanceReport r)
        {
            InitializeComponent();
            report =r ;
            //TextBoxReport.Text = report.ToString();
            /*DataGridTextColumn d = new DataGridTextColumn();
            Binding b = new Binding("Roll no");
            d.Binding = b;
            d.Header = "Roll no";
            Data_Grid.Columns.Add(d);
           // Data_Grid.ItemsSource = report.ConvertToList();
            
            foreach(var k in report.ConvertToList())
            {
                var data = k;
                Data_Grid.Items.Add(k);
            }*/

            foreach (var k in report.ConvertToList())
            {
                var data = new Test { Test1 = k.roll_no, Test2 = k.name ,Test3 = k.present };
                Data_Grid.Items.Add(data);
            }

        }

        public class Test
        {
            public string Test1 { get; set; }
            public string Test2 { get; set; }
            public string Test3 { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
