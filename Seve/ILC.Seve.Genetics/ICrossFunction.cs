namespace ILC.Seve.Genetics
{
    /// <summary>
    /// A cross function "breeds" two individuals to make another individual.
    /// </summary>
    public interface ICrossFunction
    {
        Individual Cross(Individual father, Individual mother, IBinarySerializer serializer);
    }
}
