using System;
using System.Collections.Generic;
using System.Text;

namespace FeddosMessengerApp.FireBase
{
    public interface IFireBaseComponent
    {
        bool CheckGooglePlayServicesAvailability();
        string GetFireBaseToken();
    }
}
