using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Doppler.DependencyInjections;
using Doppler.Droid.DataBase;

[assembly: Xamarin.Forms.Dependency(typeof(dbDependency))]
namespace Doppler.Droid.DataBase
{
    public class dbDependency:IGetPath
    {
        public string GetDataBasePath(string filename)
        {
            return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filename);
        }
    }
}