using BulletSharp;

namespace ILC.Seve.Physics
{
    /// <summary>
    /// Some helpful methods for dealing with BulletSharp.
    /// </summary>
    public class PhysicsHelpers
    {
        public static RigidBody MakePlane(Vector3 normal, int constant)
        {
            var shape = new StaticPlaneShape(new Vector3(0, 1, 0), 1);
            var startTransform = Matrix.Translation(new Vector3(0, 0, 0));
            var motionState = new DefaultMotionState(startTransform);
            var rigidBodyCI = new RigidBodyConstructionInfo(0, motionState, shape, new Vector3(0, 0, 0));
            var plane = new RigidBody(rigidBodyCI);

            return plane;
        }

        public static RigidBody MakeSphere(int x, int y, int z)
        {
            var sphereShape = new SphereShape(1);
            var sphereStartTransform = Matrix.Translation(new Vector3(x, y, z));
            var sphereMotionState = new DefaultMotionState(sphereStartTransform);
            var sphereIntertia = sphereShape.CalculateLocalInertia(1);
            var sphereRigidBodyCI = new RigidBodyConstructionInfo(1, sphereMotionState, sphereShape, sphereIntertia);
            var sphere = new RigidBody(sphereRigidBodyCI);

            return sphere;
        }
    }
}
