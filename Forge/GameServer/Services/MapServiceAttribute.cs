using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Services
{
    /// <summary>
    /// If you mark a class with this attribute, all map simulators will create an instance of it.
    /// Use this if the service needs to act on its own entity's.
    /// This only works for services with no dependencies.
    /// </summary>
    public class MapServiceAttribute : Attribute
    {
    }
}
