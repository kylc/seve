using System;
using System.Text;
using System.Threading;
using BulletSharp;
using ILC.Seve.Physics;
using ILC.Seve.Web;

namespace ILC.Seve
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientBroadcaster server = new ClientBroadcaster();
            server.StartServer();

            // Don't run the simulation until someone connects (via browser)
            // TODO: Don't do this
            while (server.Connections.Count == 0)
            {
                Thread.Sleep(100);
            }

            Console.WriteLine("Running Simulation...");

            PhysicsDemo physics = new PhysicsDemo();
            physics.RunSimulation(10, () =>
            {
                // TODO: Come up with a real JSON solution, and make sure it's fast
                StringBuilder toSend = new StringBuilder();
                toSend.Append("[");

                foreach (RigidBody s in physics.Spheres)
                {
                    Matrix m = s.MotionState.WorldTransform;
                    // TODO: Is accessing the coordinates by the matrix indices the right way to do this?
                    toSend.Append("[ " + m.M41 + ", " + m.M42 + ", " + m.M43 + " ],");
                }

                toSend.Remove(toSend.Length - 1, 1); // Shave off the trailing comma
                toSend.Append("]");

                server.Broadcast(toSend.ToString());
            });
        }
    }
}
