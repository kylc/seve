using System;

namespace ILC.Seve.Genetics
{
    /// <summary>
    /// A mutation function that gives each individual a constant change of
    /// being mutated.
    /// </summary>
    public class ConstantMutateFunction : IMutateFunction
    {
        public readonly int MutationPercent;

        private static Random Random;

        public ConstantMutateFunction(int mutationPercent)
        {
            MutationPercent = mutationPercent;
            Random = new Random();
        }

        public Individual Mutate(Individual individual, IBinarySerializer serializer)
        {
            byte[] data = serializer.ToBinary(individual);

            if (Random.Next(100) < MutationPercent)
            {
                var mutateIndex = Random.Next(data.Length);

                data[mutateIndex] = (byte)Random.Next(256);
            }

            return serializer.FromBinary(data);
        }
    }
}
