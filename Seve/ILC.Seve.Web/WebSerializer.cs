using System;
using System.Collections.Generic;
using ILC.Seve.Genetics;

namespace ILC.Seve.Web
{
    public interface WebSerializer
    {
        string Rewind();
        string Serialize(List<Individual> state);
    }
}
