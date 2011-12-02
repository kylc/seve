using System.Collections.Generic;

namespace ILC.Seve
{
    class UndirectedNode
    {
        public UndirectedNode()
        {
            Siblings = new List<UndirectedNode>();
        }

        public void GrowSibling()
        {
            UndirectedNode sibling = new UndirectedNode();
            Siblings.Add(sibling);
        }

        public void AttachTo(UndirectedNode node)
        {
            Siblings.Add(node);
            node.Siblings.Add(this);
        }

        public List<UndirectedNode> Siblings { get; private set; }
    }
}
