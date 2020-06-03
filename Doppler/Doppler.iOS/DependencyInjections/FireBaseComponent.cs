using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Doppler.DependencyInjections;
using Doppler.iOS.DependencyInjections;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(FireBaseComponent))]
namespace Doppler.iOS.DependencyInjections
{
    public class FireBaseComponent : IFireBaseComponent
    {
        //TODO
        public bool CheckGooglePlayServicesAvailability()
        {
            return true;
        }

        public string GetFireBaseToken()
        {
            return "";
        }
    }
}