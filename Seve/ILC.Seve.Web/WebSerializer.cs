using ILC.Seve.Graph;

namespace ILC.Seve.Web
{
    public interface WebSerializer
    {
        string Serialize(VertexGraph graph);
    }
}
