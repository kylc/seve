using System;

namespace ILC.Seve.Genetics
{
    /// <summary>
    /// A cross function that splits each individual at a constant index.  For
    /// example, if the constant was 50, it would take the first 50 bytes of
    /// the first individual and the rest of the data from the second
    /// individual.
    /// </summary>
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

            Array.Copy(fatherData, childData, Constant);
            Array.Copy(motherData, Constant, childData, Constant, childData.Length - Constant);

            return serializer.FromBinary(childData);
        }
    }
}
