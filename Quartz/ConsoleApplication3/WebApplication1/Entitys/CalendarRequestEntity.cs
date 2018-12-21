using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Entitys
{
    public class CalendarRequestEntity
    {
        public string calendarname { get; set; }

        public string calendartype { get; set; }

        public string triggerkey { get; set; }

        public string selectdate { get; set; }
    }
}