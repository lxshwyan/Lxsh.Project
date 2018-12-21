using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Entitys
{
    public class TriggerRequestEntity
    {
        public string TriggerName { get; set; }

        public string TriggerGroupName { get; set; }

        public string ForJobName { get; set; }

        public string CronExpress { get; set; }

        public string Description { get; set; }
    }
}