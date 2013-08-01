using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toolkit
{
    public static class BindingHelper
    {
        public static void ClearBindings(Control c)
        {
            Binding[] bindings = new Binding[c.DataBindings.Count];
            c.DataBindings.CopyTo(bindings, 0);
            c.DataBindings.Clear();

            foreach (Binding binding in bindings)
            {
                TypeDescriptor.Refresh(binding.DataSource);
            }
            foreach (Control cc in c.Controls)
            {
                ClearBindings(cc);
            }
        }
    }
}
