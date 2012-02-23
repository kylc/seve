using System;

namespace ILC.Seve.Genetics
{
    public class ConstantRatioCrossFunction : ICrossFunction
    {
        private readonly float Ratio;

        public ConstantRatioCrossFunction(float ratio)
        {
            this.Ratio = ratio;
        }

        public Individual Cross(Individual father, Individual mother, BinarySerializer serializer)
        {
            byte[] fatherData = serializer.ToBinary(father);
            byte[] motherData = serializer.ToBinary(mother);

            if (fatherData.Length != motherData.Length)
            {
                throw new ArgumentException("Individuals must compose to binary representations of the same length to be crossed.");
            }

            byte[] childData = new byte[fatherData.Length];

            var splitIndex = (long)(fatherData.Length * Ratio);
            Array.Copy(fatherData, childData, splitIndex);
            Array.Copy(motherData, splitIndex, childData, splitIndex, childData.Length - splitIndex);

            return serializer.FromBinary(childData);
        }
    }
}
