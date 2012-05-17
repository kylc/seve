namespace ILC.Seve.Web
{
    public interface WebSerializer
    {
        string Rewind();
        string Serialize(SimulationState state);
    }
}
