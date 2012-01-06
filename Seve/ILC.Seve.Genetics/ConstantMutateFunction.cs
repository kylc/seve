using System;

namespace ILC.Seve.Genetics
{
    /// <summary>
    /// A mutation function that gives each individual a constant change of
    /// being mutated.
    /// </summary>
    public class ConstantMutateFunction : IMutateFunction
    {
        public int MutationPercent { get; set; }
        private Random Random;

        public ConstantMutateFunction(int mutationPercent)
        {
            MutationPercent = mutationPercent;
            Random = new Random();
        }

        public Individual Mutate(Individual individual, IBinarySerializer serializer)
        {
            if (Random.Next(100) < MutationPercent)
            {
                // TODO: Mutate
            }

            return individual; // throw new NotImplementedException();
        }
    }
}
