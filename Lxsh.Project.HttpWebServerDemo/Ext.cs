using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace System
{
    public delegate void BeginInvokeDelegate();
    public static class Ext
    {       
        public static void BeginInvoke(this Control ctrl, BeginInvokeDelegate action)
        {
            if (!ctrl.IsHandleCreated && ctrl.IsDisposed)
            {
                return;
            }
            if (ctrl.InvokeRequired)
            {
                ctrl.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
