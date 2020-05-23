using SharedTypes.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeddosMessengerApp.DependencyInjections
{
    public interface IGetPlatform
    {
        PlatformType GetPlatform();
    }
}
