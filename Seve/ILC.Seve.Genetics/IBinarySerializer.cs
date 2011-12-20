using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILC.Seve.Genetics
{
    public interface IBinarySerializer
    {
        byte[] ToBinary(Individual individual);

        Individual FromBinary(byte[] data);
    }
}
