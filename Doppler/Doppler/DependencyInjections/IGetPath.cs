using System;
using System.Collections.Generic;
using System.Text;

namespace Doppler.DependencyInjections
{
    public interface IGetPath
    {
        string GetDataBasePath(string fileName);
    }
}
