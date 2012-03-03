using ILC.Seve.Genetics;
using ILC.Seve.Graph;
using ILC.Seve.Web;

namespace ILC.Seve.Examples.Skyscraper
{
    public class SkyscraperSimulation
    {
        public const int PopulationSize = 20;
        public const int RunForGenerations = 100;

        public const float CrossConstantRatio = 0.5F;
        public const int MutatePercent = 5;

        public const int VertexCount = 20;
        public const long Max = 3000;

        static void Main(string[] args)
        {
            var clientBroadcaster = new ClientBroadcaster();
            var webSerializer = new JSONWebSerializer();

            var constructor = new SkyscraperBinarySerializer(VertexCount, Max);

            var crossFunction = new ConstantRatioCrossFunction(CrossConstantRatio);
            var mutateFunction = new ConstantMutateFunction(MutatePercent);

            var algorithm = new Algorithm(PopulationSize, constructor, crossFunction, mutateFunction);
            var simulation = new SerialSimulation(algorithm, state =>
            {
                var data = webSerializer.Serialize(state);
                clientBroadcaster.Broadcast(data);
            });

            clientBroadcaster.StartServer();
            simulation.RunSimulation();
        }
    }
}
