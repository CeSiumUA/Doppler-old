using FeddosMessengerApp.MobileDataBase;
using FeddosMessengerApp.UWP.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(dbDependency))]
namespace FeddosMessengerApp.UWP.DataBase
{
    public class dbDependency : IGetPath
    {
        public string GetDataBasePath(string fileName)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, fileName);
        }
    }
}
