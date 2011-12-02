using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILC.Seve
{
    class GADirectedSimulation : GASimulation
    {
        public GADirectedSimulation()
        {
            Agents = new List<DirectedNode>();
        }

        public override void Step()
        {
            
        }

        public override void Stop()
        {
            
        }

        public List<DirectedNode> Agents { get; private set; }
    }
}
