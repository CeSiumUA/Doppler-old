using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using FeddosMessengerApp.iOS.DataBase;
using FeddosMessengerApp.MobileDataBase;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(dbDependency))]
namespace FeddosMessengerApp.iOS.DataBase
{
    public class dbDependency:IGetPath
    {
        public string GetDataBasePath(string fileName)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", fileName);
        }
    }
}