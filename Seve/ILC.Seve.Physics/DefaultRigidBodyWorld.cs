using System;
using System.Collections.Generic;
using System.Linq;
using BulletSharp;
using ILC.Seve.Graph;
using ILC.Seve.Util;

namespace ILC.Seve.Physics
{
    public class DefaultRigidBodyWorld : PhysicsWorld
    {
        private static DbvtBroadphase Broadphase;
        private static DefaultCollisionConfiguration CollisionConfiguration;
        private static CollisionDispatcher Dispatcher;
        private static SequentialImpulseConstraintSolver Solver;
        private static DiscreteDynamicsWorld DynamicsWorld;
        private static RigidBody Ground;

        private List<VertexBoundRigidBody> Individual;

        static DefaultRigidBodyWorld()
        {
            // Broadphase algorithms are responsible for calculating bounding
            // boxes.  We should probably use an AABB Tree (DbvtBroadphase)
            // because they are generally good for worlds with lots of motion.
            // Sweep and Prune Broadphases are best when most of the world is
            // static.
            Broadphase = new DbvtBroadphase();
            CollisionConfiguration = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConfiguration);
            Solver = new SequentialImpulseConstraintSolver();

            DynamicsWorld = new DiscreteDynamicsWorld(Dispatcher, Broadphase, Solver, CollisionConfiguration);
            DynamicsWorld.Gravity = new Vector3(0F, 0F, -9.81F);

            Ground = PhysicsHelpers.MakePlane(new Vector3(0, 0, 1), 0);
            DynamicsWorld.AddRigidBody(Ground);
        }
        
        public DefaultRigidBodyWorld(VertexGraph initialState)
        {
            AddVertexGraph(initialState);
        }

        private void AddVertexGraph(VertexGraph graph)
        {
            Individual = graph.Vertices.Select(vertex =>
            {
                var mass = 10000;
                var motionState = new DefaultMotionState();
                var collisionShape = new BoxShape(1);

                var info = new RigidBodyConstructionInfo(mass, motionState, collisionShape);
                var rigidBody = new VertexBoundRigidBody(vertex, info);

                return rigidBody;
            }).ToList();

            foreach (var body in Individual)
            {
                // Select the 3 nearest vertices, excluding this one
                var nearest = Individual.OrderBy(a => a.Binding.DistanceTo(body.Binding))
                                         .Where(a => a != body)
                                         .Take(3);
                foreach (var other in nearest)
                {
                    // TODO: What are these matrices supposed to be?
                    var frameInA = body.MotionState.WorldTransform;
                    var frameInB = other.MotionState.WorldTransform;

                    // TODO: How do you specify the spring's springiness?
                    var constraint = new Generic6DofSpringConstraint(body, other, frameInA, frameInB, true);

                    // TODO: Now how do I apply this to the bodies?
                    body.AddConstraintRef(constraint);
                    other.AddConstraintRef(constraint);
                }
            }
        }

        public override void StepSimulation(int frequency)
        {
            DynamicsWorld.StepSimulation(1.0F / frequency, 10);
        }

        public override VertexGraph GetState()
        {
            var vertices = Individual.Select(body => body.Binding).ToList();

            return new VertexGraph(vertices);
        }
    }
}
