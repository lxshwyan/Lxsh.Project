﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.DesignPattern_Builder
{
    public class FatPerson : AbstractPerson
    {
        public override void CreateBody()
        {
            Console.WriteLine("创建胖人的身体");
        }

        public override void CreateHand()
        {
            Console.WriteLine("创建胖人的手");
        }

        public override void CreateHead()
        {
            Console.WriteLine("创建胖人的头");
        }

        public override void CreateLeg()
        {
            Console.WriteLine("创建胖人的脚");
        }
    }
}