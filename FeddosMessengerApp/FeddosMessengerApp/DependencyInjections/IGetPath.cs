using System;
using System.Collections.Generic;
using System.Text;

namespace FeddosMessengerApp.DependencyInjections
{
    public interface IGetPath
    {
        string GetDataBasePath(string fileName);
    }
}
