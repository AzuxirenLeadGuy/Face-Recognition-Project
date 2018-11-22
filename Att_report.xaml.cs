using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input; 
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FRAttendance;
using Newtonsoft.Json;
using GemBox.Spreadsheet;
using Microsoft.Win32;
namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Att_report.xaml
    /// </summary>
    public partial class Att_report : Page
    {
        AttendanceReport report;
        ObservableCollection<Test> col;
        String Date,Sub;

        internal Att_report(AttendanceReport r)
        {
            InitializeComponent();
            report = r;
            Subject s = (Subject)(r.subject);
            LSubject.Content = s.Name;
            Sub = s.Name;
            LCourseCode.Content = s.Code;
            LFacultyName.Content = s.FacultyName;
            LDate.Content = r.Date.ToLongDateString();
            Date= r.Date.ToLongDateString();
            col = new ObservableCollection<Test>();
            foreach (var k in report.ConvertToList())
            {
                var data = new Test { Test1 = k.roll_no, Test2 = k.name ,Test3=k.present=="Present" };
                col.Add(data);
            }
            Data_Grid.ItemsSource = col;
        }

        public class Test
        {
            public string Test1 { get; set; }
            public string Test2 { get; set; }
            public bool Test3 { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new First());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new FRAWPF.Test());
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Export_Excel(report.ConvertToList());
            Report_button.Content = "Report Generated";
            Report_button.IsEnabled = false;
        }

        

        public void Export_Excel(IEnumerable<Data_Diaplay> Info)
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            ExcelFile myExcelFile = new ExcelFile();
            ExcelWorksheet excWsheet = myExcelFile.Worksheets.Add("Attendance worksheet");

            excWsheet.Columns[0].Width = 10 * 256;
            excWsheet.Columns[1].Width = 30 * 256;
            excWsheet.Columns[2].Width = 15 * 256;

            excWsheet.Rows[2].Cells[0].Value = "Roll No";
            excWsheet.Rows[2].Cells[1].Value = "Name";
            excWsheet.Rows[2].Cells[2].Value = "Attendance";

            int count = 4;
            foreach (var data in Info)
            {
                excWsheet.Rows[count].Cells[0].Value = data.roll_no;
                excWsheet.Rows[count].Cells[1].Value = data.name;
                excWsheet.Rows[count].Cells[2].Value = data.present;
                count++;

            }
            var op = new SaveFileDialog();
            op.Title = "Select a Location to save Spreadsheet report";
            op.Filter = "Spreadsheet files|*.xls";;
            op.DefaultExt = "xls";
            if (op.ShowDialog() == true)
            {
                myExcelFile.Save(op.FileName);
            }
        }
    }

}
