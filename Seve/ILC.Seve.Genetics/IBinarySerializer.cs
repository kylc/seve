using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILC.Seve.Genetics
{
    /// <summary>
    /// Converts Individuals to and from binary data.  This is useful for
    /// crossing and mutating as we only have to mix a simple data structure,
    /// rather than a complex one (a graph, for instance).
    /// </summary>
    public interface IBinarySerializer
    {
        byte[] ToBinary(Individual individual);

        Individual FromBinary(byte[] data);
    }
}
