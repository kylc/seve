using BulletSharp;
using BulletSharp.SoftBody;
using ILC.Seve.Graph;

namespace ILC.Seve.Physics
{
    /// <summary>
    /// A basic implementation of a PhysicsWorld.  This implementation utilizes
    /// Bullet's soft body dynamics engine to simulate individuals in a very
    /// simplistic manner.
    /// </summary>
    public class DefaultSoftBodyWorld : PhysicsWorld
    {
        private SoftRigidDynamicsWorld DynamicsWorld;
        private SoftBodyWorldInfo WorldInfo;
        private RigidBody Ground;
        private SoftBody Individual;

        private VertexGraph LastState;

        public DefaultSoftBodyWorld(VertexGraph initialState)
        {
            // Broadphase algorithms are responsible for calculating bounding
            // boxes.  We should probably use an AABB Tree (DbvtBroadphase)
            // because they are generally good for worlds with lots of motion.
            // Sweep and Prune Broadphases are best when most of the world is
            // static.
            var broadphase = new DbvtBroadphase();
            var collisionConfiguration = new SoftBodyRigidBodyCollisionConfiguration();
            var dispatcher = new CollisionDispatcher(collisionConfiguration);
            var solver = new SequentialImpulseConstraintSolver();

            DynamicsWorld = new SoftRigidDynamicsWorld(dispatcher, broadphase, solver, collisionConfiguration);
            // Bullet uses a right-handed coordinate system, so Y is up, X is "right", and z is "left"
            DynamicsWorld.Gravity = new Vector3(0F, -9.81F, 0F); // Y is the traditional Z axis

            Ground = PhysicsHelpers.MakePlane(new Vector3(0, 1, 0), 0);
            DynamicsWorld.AddRigidBody(Ground);

            WorldInfo = new SoftBodyWorldInfo();
            WorldInfo.AirDensity = 1.2F;
            WorldInfo.Gravity = new Vector3(0, -9.81F, 0);
            WorldInfo.Dispatcher = dispatcher;
            WorldInfo.Broadphase = broadphase;
            WorldInfo.SparseSdf.Initialize();

            AddVertexGraph(initialState);
        }

        private void AddVertexGraph(VertexGraph graph)
        {
            LastState = graph;

            var vertices = new Vector3[graph.Vertices.Count];

            // First, turn all of the vertices into Vector3s that can be used
            // by Bullet.
            for (var i = 0; i < vertices.Length; i++)
            {
                var vertex = graph.Vertices[i];

                vertices[i] = new Vector3(vertex.X, vertex.Y, vertex.Z);
            }

            Individual = SoftBodyHelpers.CreateFromConvexHull(WorldInfo, vertices);

            DynamicsWorld.AddSoftBody(Individual);

            // Now assign the proper GUIDs to each node.
            // TODO: Is there a better way to do this?  At creation (above), perhaps?
            foreach (Node node in Individual.Nodes)
            {
                Vector3 location = node.X;
                foreach (Vertex vertex in graph.Vertices)
                {
                    // TODO: Floating point comparisons are failing here, but integers aren't accurate enough...
                    if ((int)location.X == (int)vertex.X
                        && (int)location.Y == (int)vertex.Y
                        && (int)location.Z == (int)vertex.Z)
                    {
                        node.Tag = vertex.Identifier;
                    }
                }
            }
        }

        public override void StepSimulation(int frequency)
        {
            DynamicsWorld.StepSimulation(1.0F / frequency, 10);
        }

        public override VertexGraph GetState()
        {
            foreach (Vertex vertex in LastState.Vertices)
            {
                foreach (Node node in Individual.Nodes)
                {
                    if (vertex.Identifier.Equals(node.Tag))
                    {
                        vertex.X = node.X.X;
                        vertex.Y = node.X.Y;
                        vertex.Z = node.X.Z;
                    }
                }
            }

            return LastState;
        }
    }
}
