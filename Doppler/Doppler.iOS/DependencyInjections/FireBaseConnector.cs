using System;
using Doppler.DependencyInjections;
using Doppler.iOS.DependencyInjections;

[assembly: Xamarin.Forms.Dependency(typeof(GooglePlayServicesAvailablility))]
namespace Doppler.iOS.DependencyInjections
{
    public class FireBaseConnector:IFireBaseComponent
    {
        public FireBaseConnector()
        {
        }
        //TODO
        public bool CheckGooglePlayServicesAvailability()
        {
            return true;
        }

        public string GetFireBaseToken()
        {
            
        }
    }
}
