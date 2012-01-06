namespace ILC.Seve.Genetics
{
    /// <summary>
    /// A mutate function takes an individual and probabilistically changes
    /// some bytes of that individual.
    /// </summary>
    public interface IMutateFunction
    {
        Individual Mutate(Individual individual, IBinarySerializer serializer);
    }
}
