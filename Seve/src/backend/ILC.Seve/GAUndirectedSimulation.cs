﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILC.Seve
{
    class GAUndirectedSimulation : GASimulation
    {
        public GAUndirectedSimulation()
        {
            Agents = new List<UndirectedNode>();
        }

        public override void Step()
        {
            
        }

        public override void Stop()
        {
            
        }

        public List<UndirectedNode> Agents { get; private set; }
    }
}