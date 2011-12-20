namespace ILC.Seve.Genetics
{
    public interface IMutateFunction
    {
        Individual Mutate(Individual individual, IBinarySerializer serializer);
    }
}
