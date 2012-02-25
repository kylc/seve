using System;
using System.Collections.Generic;

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

        public VertexGraph(List<Vertex> vertices)
        {
            Vertices = vertices;
        }

        public VertexGraph Copy()
        {
            VertexGraph graph = new VertexGraph();

            foreach (var vertex in Vertices)
            {
                graph.Vertices.Add(vertex.Copy());
            }

            return graph;
        }
    }

    /// <summary>
    /// A vertex.  Each vertex has an x, y, and z coordinate in space.
    /// </summary>
    public class Vertex
    {
        public readonly long Max;
        public readonly long ScaleFactor;

        public long X;
        public long Y;
        public long Z;

        public float ScaledX
        {
            get
            {
                return X / ScaleFactor;
            }
        }

        public float ScaledY
        {
            get
            {
                return Y / ScaleFactor;
            }
        }

        public float ScaledZ
        {
            get
            {
                return Z / ScaleFactor;
            }
        }

        public Vertex(long x, long y, long z, long max)
        {
            this.Max = max;
            this.ScaleFactor = GetScaleFactor(max);

            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static long GetScaleFactor(long max)
        {
            return long.MaxValue / max;
        }

        //Calculates the distance between two Vertices
        public long DistanceTo(Vertex other)
        {
            return (long) Math.Sqrt(Math.Pow(X - other.X, 2)
                                  + Math.Pow(Y - other.Y, 2)
                                  + Math.Pow(Z - other.Z, 2));
        }

        //Returns a copy of this Vertex
        public Vertex Copy()
        {
            return new Vertex(X, Y, Z, Max);
        }
    }
}
