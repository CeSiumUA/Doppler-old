using System;
using System.Collections.Generic;
using System.Text;

namespace Doppler.DependencyInjections
{
    public interface IFireBaseComponent
    {
        bool CheckGooglePlayServicesAvailability();
        string GetFireBaseToken();
    }
}
