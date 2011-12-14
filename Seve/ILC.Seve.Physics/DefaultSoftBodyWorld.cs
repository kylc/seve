﻿using System;
using System.Linq;
using BulletSharp;
using BulletSharp.SoftBody;
using ILC.Seve.Graph;

namespace ILC.Seve.Physics
{
    public class DefaultSoftBodyWorld : PhysicsWorld
    {
        public SoftRigidDynamicsWorld DynamicsWorld { get; private set; }
        public SoftBodyWorldInfo WorldInfo { get; private set; }
        public RigidBody Ground { get; private set; }
        public SoftBody Individual { get; private set; }

        public VertexGraph LastState { get; private set; }

        public DefaultSoftBodyWorld(VertexGraph initialState)
        {
            LastState = initialState;

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

            Ground = PhysicsHelpers.MakePlane(new Vector3(0, 1, 0), 1);
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
                // TODO: Is ElementAt slow for this?
                var vertex = graph.Vertices.ElementAt(i);

                vertices[i] = new Vector3(vertex.X, vertex.Y, vertex.Z);
            }

            Individual = SoftBodyHelpers.CreateFromConvexHull(WorldInfo, vertices);

            // Now assign the proper GUIDs to each node.
            // TODO: Is there a better way to do this?  At creation (above), perhaps?
            foreach (Node node in Individual.Nodes)
            {
                Vector3 location = node.X;
                foreach (Vertex vertex in graph.Vertices)
                {
                    if (location.X == vertex.X
                        && location.Y == vertex.Y
                        && location.Z == vertex.Z)
                    {
                        node.Tag = vertex.Identifier;
                    }
                }
            }

            DynamicsWorld.AddSoftBody(Individual);
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