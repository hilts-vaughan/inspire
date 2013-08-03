using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toolkit.Docking
{
    public static class FormExtensions
    {

        public static void Invoke(this Control control, MethodInvoker action)
        {
            control.Invoke(action);
        }
    }
}
