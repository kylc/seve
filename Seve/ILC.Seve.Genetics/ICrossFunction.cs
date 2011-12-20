namespace ILC.Seve.Genetics
{
    public interface ICrossFunction
    {
        Individual Cross(Individual father, Individual mother, IBinarySerializer serializer);
    }
}
