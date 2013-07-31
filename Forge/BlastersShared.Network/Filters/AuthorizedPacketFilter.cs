using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Network.Filters
{
    /// <summary>
    /// Any packet marked with this attribute will be discarded unless the sending user is currently authorized
    /// </summary>
    public class AuthorizedPacketFilter : Attribute
    {
    }
}
