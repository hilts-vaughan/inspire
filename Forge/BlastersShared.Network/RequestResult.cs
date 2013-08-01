using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Network
{
    /// <summary>
    /// Represents a particular network request state. Returns succesful if was OK, otherwise returns failed.
    /// </summary>
    public enum RequestResult
    {
        Succesful,
        Failed
    }
}
