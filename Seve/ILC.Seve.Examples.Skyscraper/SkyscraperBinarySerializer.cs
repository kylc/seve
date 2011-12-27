using System.IO;
using ILC.Seve.Genetics;
using ILC.Seve.Graph;

namespace ILC.Seve.Examples.Skyscraper
{
    public class SkyscraperBinarySerializer : IBinarySerializer
    {
        public byte[] ToBinary(Individual _individual)
        {
            var individual = (SkyscraperIndividual)_individual;
            var graph = individual.Graph;

            var stream = new MemoryStream();

            using (stream)
            {
                var writer = new BinaryWriter(stream);

                // Write the length of the graph (int)
                writer.Write(graph.Vertices.Count);

                // Write the locations of each point on the graph (3 floats)
                foreach (var vertex in graph.Vertices)
                {
                    writer.Write(vertex.X);
                    writer.Write(vertex.Y);
                    writer.Write(vertex.Z);
                }
            }

            return stream.ToArray();
        }

        public Individual FromBinary(byte[] data)
        {
            var graph = new VertexGraph();
            var reader = new BinaryReader(new MemoryStream(data));

            var count = reader.ReadInt32();

            for (int i = 0; i < count; i++)
            {
                var vertex = new Vertex(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                graph.Vertices.Add(vertex);
            }

            var individual = new SkyscraperIndividual(graph);

            // Use ConnectNearest to restore connections.  This is less than ideal.
            individual.ConnectNearest(2);

            return individual;
        }
    }
}
