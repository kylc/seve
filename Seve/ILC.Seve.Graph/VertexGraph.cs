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
        public List<Vertex> Connections { get; set; }

        public Guid Identifier;

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vertex(float X, float Y, float Z)
        {
            Connections = new List<Vertex>();

            Identifier = Guid.NewGuid();

            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public void ConnectTo(Vertex other)
        {
            if (!IsConnectedTo(other))
            {
                Connections.Add(other);
            }
            if(!other.IsConnectedTo(this))
            {
                other.Connections.Add(this);
            }
        }

        public bool IsConnectedTo(Vertex other)
        {
            return Connections.Contains(other);
        }

        public double DistanceTo(Vertex other)
        {
            return Math.Sqrt(Math.Pow(X - other.X, 2)
                           + Math.Pow(Y - other.Y, 2)
                           + Math.Pow(Z - other.Z, 2));
        }
    }
}
