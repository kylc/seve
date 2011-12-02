using System.Collections.Generic;

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
