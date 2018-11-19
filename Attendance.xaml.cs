﻿using Microsoft.Win32;
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
namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Attendance.xaml
    /// This is required
    /// </summary>
    public partial class Attendance : Page
    {
        string pathuri;
        public Attendance()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FRAttendance.Subject s = new FRAttendance.Subject();
            var rep = s.TakeAttendance(FaceRecognition.LoadImageFile(pathuri));
            this.NavigationService.Navigate(new Att_report(rep));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                pathuri = op.FileName;
                Image1.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new FRAWPF.Test() );
        }
    }
}
