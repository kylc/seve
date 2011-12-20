using System;

namespace ILC.Seve.Genetics
{
    class ConstantCrossFunction : ICrossFunction
    {
        public readonly int Constant;

        public ConstantCrossFunction(int constant)
        {
            Constant = constant;
        }

        public Individual Cross(Individual father, Individual mother, IBinarySerializer serializer)
        {
            byte[] fatherData = serializer.ToBinary(father);
            byte[] motherData = serializer.ToBinary(mother);

            if (fatherData.Length != motherData.Length)
            {
                throw new ArgumentException("Individuals must compose to binary representations of the same length to be crossed.");
            }

            byte[] childData = new byte[fatherData.Length];

            // TODO: High probability of an off-by-one error here
            Array.Copy(fatherData, childData, Constant);
            Array.Copy(motherData, Constant, childData, Constant, childData.Length - Constant);

            return serializer.FromBinary(childData);
        }
    }
}
