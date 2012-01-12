using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILC.Seve.Graph
{
    /// <summary>
    /// Represents a grouping of vertices of which an individual is composed.
    /// </summary>
    public class VertexGraph
    {
        public List<Vertex> Vertices { get; set; }

        public VertexGraph()
        {
            Vertices = new List<Vertex>();
        }
    }

    /// <summary>
    /// A vertex.  Each vertex has an x, y, and z coordinate in space.
    /// </summary>
    public class Vertex
    {
        /// <summary>
        /// A unique identifier used to link a Vertex to its associated
        /// BulletSharp node.
        /// </summary>
        public Guid Identifier;

        public byte X { get; set; }
        public byte Y { get; set; }
        public byte Z { get; set; }

        public Vertex(byte X, byte Y, byte Z)
        {
            Identifier = Guid.NewGuid();

            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
    }
}
