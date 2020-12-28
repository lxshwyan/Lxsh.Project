using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lxsh.Project.GDIDemo
{
   public class MyPannel:Panel
    {
        public MyPannel() 
        {

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            this.SetStyle(ControlStyles.UserPaint, true);

        }
    }
}
