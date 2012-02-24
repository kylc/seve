using System;
using BulletSharp;
using ILC.Seve.Graph;

namespace ILC.Seve.Util
{
    public class VertexBoundRigidBody : RigidBody
    {
        public Vertex Binding { get; set; }

        public VertexBoundRigidBody(Vertex binding, RigidBodyConstructionInfo info)
            : base(info)
        {
            this.Binding = binding;
        }
    }
}
