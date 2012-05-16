using System;

namespace ILC.Seve.Genetics
{
    /// <summary>
    /// A cross function that splits each individual at a random index.
    /// </summary>
    class RandomCrossFunction : ICrossFunction
    {
        private readonly Random Random = new Random();

        public Individual Cross(Individual father, Individual mother, BinarySerializer serializer)
        {
            byte[] fatherData = serializer.ToBinary(father);
            byte[] motherData = serializer.ToBinary(mother);

            if (fatherData.Length != motherData.Length)
            {
                throw new ArgumentException("Individuals must compose to binary representations of the same length to be crossed.");
            }

            byte[] childData = new byte[fatherData.Length];

            var index = Random.Next(fatherData.Length);

            Array.Copy(fatherData, childData, index);
            Array.Copy(motherData, index, childData, index, childData.Length - index);

            return serializer.FromBinary(childData);
        }
    }
}
