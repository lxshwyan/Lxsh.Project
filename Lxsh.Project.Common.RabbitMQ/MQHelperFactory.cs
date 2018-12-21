﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Common.RabbitMQ
{
    public class MQHelperFactory {
        public static MQHelper CreateBus(string connectionString) =>
            new MQHelper(connectionString);
    }
}
