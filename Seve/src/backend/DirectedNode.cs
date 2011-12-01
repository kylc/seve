using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_SEVE
{
    class DirectedNode
    {
        public DirectedNode(DirectedNode parent)
        {
            Parent = parent;
            Children = new List<DirectedNode>();
        }

        public bool IsLeaf()
        {
            return (Children.Count == 0);
        }

        public void GrowChlid()
        {
            DirectedNode child = new DirectedNode(this);
            Children.Add(child);
        }

        public DirectedNode Parent { get; private set; }
        public List<DirectedNode> Children { get; private set; }
    }
}
