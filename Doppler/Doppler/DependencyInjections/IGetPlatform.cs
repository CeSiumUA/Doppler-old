using SharedTypes.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Doppler.DependencyInjections
{
    public interface IGetPlatform
    {
        PlatformType GetPlatform();
    }
}
