using System.IO;

namespace ILC.Seve.Genetics
{
    /// <summary>
    /// Converts Individuals to and from binary data.  This is useful for
    /// crossing and mutating as we only have to mix a simple data structure,
    /// rather than a complex one (a graph, for instance).
    /// </summary>
    public abstract class BinarySerializer
    {
        public abstract byte[] ToBinary(Individual individual);

        public abstract Individual FromBinary(Stream stream);

        public Individual FromBinary(byte[] data)
        {
            return FromBinary(new MemoryStream(data));
        }
    }
}
