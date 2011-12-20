using System;

namespace ILC.Seve.Genetics
{
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

            throw new NotImplementedException();
        }
    }
}
