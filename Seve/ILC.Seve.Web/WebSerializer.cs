using System.Collections.Generic;
using ILC.Seve.Genetics;

namespace ILC.Seve.Web
{
    public interface WebSerializer
    {
        string Serialize(List<Individual> individuals);
    }
}
