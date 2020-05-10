using System;
using System.Collections.Generic;
using System.Text;

namespace FeddosMessengerApp
{
    public interface IFireBaseComponent
    {
        bool CheckGooglePlayServicesAvailability();
        string GetFireBaseToken();
    }
}
