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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Att_report.xaml
    /// </summary>
    public partial class Att_report : Page
    {
        AttendanceReport report;
        ObservableCollection<Test> col;

        internal Att_report(AttendanceReport r)
        {
            InitializeComponent();
            report = r;
            Subject s = (Subject)(r.subject);
            LSubject.Content = s.Name;
            LCourseCode.Content = s.Code;
            LFacultyName.Content = s.FacultyName;
            LDate.Content = r.Date.ToLongDateString();
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
           /* var res = col.ToArray();
            for (int i = 0; i < res.Length; i++){report.Present[i] = res[i].Test3;}
            var text = report.ToString(); Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = ((Subject)report.subject).Code; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();
            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string path = dlg.FileName;
                System.IO.File.WriteAllText(path,text);
            }*/
            Export_Excel(report.ConvertToList());
            Report_button.Content = "Report Generated";
        }

        /* public void Export_Excel(IEnumerable<Data_Diaplay> Info)
         {
             Workbook workbook = new Workbook();
             Worksheet sheet = workbook.Worksheets[0];
             sheet.Name = "Attendance";
             sheet.Range["A1"].Text = "Roll NO";
             sheet.Range["B1"].Text = "Name";
             sheet.Range["C1"].Text = "Present";
             int i = 2;
             foreach(var data in Info)
             {
                 i++;
                 sheet.Range[("A" + i)].Text = data.roll_no;
                 sheet.Range[("B" + i)].Text = data.name;
                 sheet.Range[("C" + i)].Text = data.present;
             }
             DateTime time = DateTime.Now;

             workbook.SaveToFile("report.xls");
             System.Diagnostics.Process.Start("report.xls");
         }*/

        public void Export_Excel(IEnumerable<Data_Diaplay> Info)
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            ExcelFile myExcelFile = new ExcelFile();
            ExcelWorksheet excWsheet = myExcelFile.Worksheets.Add("Hajan's worksheet");

            excWsheet.Columns[0].Width = 10 * 256;
            excWsheet.Columns[1].Width = 30 * 256;
            excWsheet.Columns[2].Width = 10 * 256;

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
            myExcelFile.Save(@"C:\etc\Myfile");
        }
    }

}
