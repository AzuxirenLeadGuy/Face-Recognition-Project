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
            op.Title = "Select a File";
            op.Filter = "JSON files|*.uaf"; 
              
            if (op.ShowDialog() == true)
            {
                File_button.Content = op.FileName;
                
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new FRAWPF.Reg_confirm());
        }
    }
}
