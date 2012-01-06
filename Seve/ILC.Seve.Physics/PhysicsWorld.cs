using ILC.Seve.Graph;

namespace ILC.Seve.Physics
{
    /// <summary>
    /// An abstracted physics layer.  This is responsible for taking an
    /// individual and providing a way to access that individual's current
    /// state in the simulated world.
    /// </summary>
    public abstract class PhysicsWorld
    {
        public abstract void StepSimulation(int frequency);

        public abstract VertexGraph GetState();
    }
}
