using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FeddosMessengerApp.DependencyInjections;
using FeddosMessengerApp.Droid;
using Firebase.Iid;

[assembly: Xamarin.Forms.Dependency(typeof(GooglePlayServiceAvailability))]
namespace FeddosMessengerApp.Droid
{
    public class GooglePlayServiceAvailability:IFireBaseComponent
    {
        public bool CheckGooglePlayServicesAvailability()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(Application.Context);
            string x = "";
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    x = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                }
                else
                {

                }
                return false;
            }
            else
            {
                return true;
            }
        }
        public string GetFireBaseToken()
        {
            return FirebaseInstanceId.Instance.Token;
        }
    }
}