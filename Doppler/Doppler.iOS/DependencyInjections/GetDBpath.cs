using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Doppler.DependencyInjections;
using Doppler.iOS.DependencyInjections;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(GetDBpath))]
namespace Doppler.iOS.DependencyInjections
{
    public class GetDBpath : IGetPath
    {
        public string GetDataBasePath(string fileName)
        {
            return Directory.GetCurrentDirectory() + $@"\{fileName}";
        }
    }
}