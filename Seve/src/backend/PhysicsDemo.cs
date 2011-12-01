using System;
using System.Collections.Generic;
using System.Threading;
using BulletSharp;

namespace Project_SEVE
{
    // TODO: This is only a testing stage.  Probably need to rewrite all of this for the actual thing.
    public class PhysicsDemo
    {
        public DiscreteDynamicsWorld DynamicsWorld { get; private set; }
        public List<RigidBody> Spheres { get; private set; }
        public RigidBody Ground { get; private set; }

        public PhysicsDemo()
        {
            Spheres = new List<RigidBody>();

            // Broadphase algorithms are responsible for calculating bounding
            // boxes.  We should probably use an AABB Tree (DbvtBroadphase)
            // because they are generally good for worlds with lots of motion.
            // Sweep and Prune Broadphases are best when most of the world is
            // static.
            BroadphaseInterface broadphase = new DbvtBroadphase();
            DefaultCollisionConfiguration collisionConfiguration = new DefaultCollisionConfiguration();
            CollisionDispatcher dispatcher = new CollisionDispatcher(collisionConfiguration);
            SequentialImpulseConstraintSolver solver = new SequentialImpulseConstraintSolver();

            DynamicsWorld = new DiscreteDynamicsWorld(dispatcher, broadphase, solver, collisionConfiguration);
            // Bullet uses a right-handed coordinate system, so Y is up, X is "right", and z is "left"
            DynamicsWorld.Gravity = new Vector3(0F, -9.81F, 0F); // Y is the traditional Z axis

            Ground = MakePlane(new Vector3(0, 1, 0), 1);
            DynamicsWorld.AddRigidBody(Ground);

            Random r = new Random();
            for (int i = 0; i < 1000; i++)
            {
                RigidBody sphere = MakeSphere(r.Next(10), r.Next(100), r.Next(10));
                Spheres.Add(sphere);
                DynamicsWorld.AddRigidBody(sphere);
            }
        }

        public void RunSimulation(int seconds, Action tick)
        {
            for (int i = 0; i < seconds * 60; i++)
            {
                DynamicsWorld.StepSimulation(1.0F / 60.0F, 10);

                tick();

                // Don't sleep, run at maximum speed for the real simulation!
                Thread.Sleep(1000 / 60); // TODO: Account for calculation time
            }
        }

        private RigidBody MakePlane(Vector3 normal, int constant)
        {
            CollisionShape shape = new StaticPlaneShape(new Vector3(0, 1, 0), 1);
            Matrix startTransform = Matrix.Translation(new Vector3(0, 0, 0));
            DefaultMotionState motionState = new DefaultMotionState(startTransform);
            RigidBodyConstructionInfo rigidBodyCI = new RigidBodyConstructionInfo(0, motionState, shape, new Vector3(0, 0, 0));
            RigidBody plane = new RigidBody(rigidBodyCI);

            return plane;
        }

        private RigidBody MakeSphere(int x, int y, int z)
        {
            CollisionShape sphereShape = new SphereShape(1);
            Matrix sphereStartTransform = Matrix.Translation(new Vector3(x, y, z));
            DefaultMotionState sphereMotionState = new DefaultMotionState(sphereStartTransform);
            Vector3 sphereIntertia = sphereShape.CalculateLocalInertia(1);
            RigidBodyConstructionInfo sphereRigidBodyCI = new RigidBodyConstructionInfo(1, sphereMotionState, sphereShape, sphereIntertia);
            RigidBody sphere = new RigidBody(sphereRigidBodyCI);

            return sphere;
        }
    }
}
