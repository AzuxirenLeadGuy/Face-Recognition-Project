using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.IO;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            string[] x=new string[2];
            x = JsonConvert.DeserializeObject<string[]>(File.ReadAllText("path.saf"));
            FRAttendance.Common.Init(x[0]);
            FRAttendance.AssetLoad.AssetURI = x[1];
        }
    }
}
