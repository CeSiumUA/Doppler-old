using System;
using System.Collections.Generic;
using System.Text;

namespace FeddosMessengerApp.DependencyInjections
{
    public interface IFireBaseComponent
    {
        bool CheckGooglePlayServicesAvailability();
        string GetFireBaseToken();
    }
}
