using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Doppler.DependencyInjections;
using Doppler.iOS.DependencyInjections;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(GetDatabasePath))]
namespace Doppler.iOS.DependencyInjections
{
    public class GetDatabasePath : IGetPath
    {
        public string GetDataBasePath(string fileName)
        {
            return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", fileName);
        }
    }
}