using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Doppler.DependencyInjections;
using Doppler.Droid.DependencyInjections;
using SharedTypes.Tokens;
using Xamarin.Forms;

[assembly: Dependency(typeof(GetCurrentPlatform))]
namespace Doppler.Droid.DependencyInjections
{
    public class GetCurrentPlatform : IGetPlatform
    {
        PlatformType IGetPlatform.GetPlatform()
        {
            return PlatformType.Android;
        }
    }
}