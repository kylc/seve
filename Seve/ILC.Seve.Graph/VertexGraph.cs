using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILC.Seve.Graph
{
    public class VertexGraph
    {
        public List<Vertex> Vertices { get; set; }

        public VertexGraph()
        {
            Vertices = new List<Vertex>();
        }
    }

    public class Vertex
    {
        public Guid Identifier;

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vertex(float X, float Y, float Z)
        {
            Identifier = Guid.NewGuid();

            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
    }
}
