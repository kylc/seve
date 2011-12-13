using ILC.Seve.Graph;

namespace ILC.Seve.Physics
{
    public abstract class PhysicsWorld
    {
        public abstract void StepSimulation(int frequency);

        public abstract VertexGraph GetState();
    }
}
