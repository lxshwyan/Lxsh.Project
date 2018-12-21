using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.DesignPattern_Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            StratoryContent stratoryContent;
            string loginfo = EunmLog.Error.ToString();
            switch (loginfo)
            {
                case "Error":
                    stratoryContent = new StratoryContent(new SendMsgLog() );
                    break;
                case "Info":
                    stratoryContent = new StratoryContent(new FileLog());
                    break;
                case "Waring":
                    stratoryContent = new StratoryContent(new DBLog());
                    break;
                default:
                    stratoryContent = new StratoryContent();
                    break;
            }
            stratoryContent.WriteLog(loginfo);
        }
    }
}
