using System;
using System.Collections.Generic;
using System.Threading;
using BulletSharp;
using ILC.Seve.Graph;
using BulletSharp.SoftBody;

namespace ILC.Seve.Physics
{
    public class PhysicsEngine
    {
        public PhysicsWorld PhysicalWorld { get; set; }

        public PhysicsEngine(PhysicsWorld world)
        {
            PhysicalWorld = world;
        }

        /// <summary>
        /// Simulate the world physics for the specified amount of time.  Note
        /// that this method will block until the simulation is complete.
        /// </summary>
        /// <param name="seconds">how many seconds to run the simulation</param>
        public VertexGraph RunSimulation(int seconds)
        {
            for (int i = 0; i < seconds * 60; i++)
            {
                // Step at 60hz
                PhysicalWorld.StepSimulation(60);
            }

            // TODO: Generate graph from physics data
            return PhysicalWorld.GetState();
        }
    }
}
