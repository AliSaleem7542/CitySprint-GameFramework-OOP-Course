using System.Collections.Generic;
using System.Drawing;

namespace GameFrameWork
{
    public class PhysicsSystem
    {
        public float Gravity { get; set; } = 0.5f;

        public void Apply(List<GameObject> objects)
        {
            foreach (var obj in objects)
            {
                if (!obj.HasPhysics) continue;

                float appliedGravity = obj.CustomGravity ?? Gravity;

                if (obj is IMovable movable)
                {
                    movable.Velocity = new PointF(movable.Velocity.X, movable.Velocity.Y + appliedGravity);
                    obj.Position = new PointF(obj.Position.X + movable.Velocity.X, obj.Position.Y + movable.Velocity.Y);
                }
            }
        }
    }
}

//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;

//namespace GameFrameWork
//{
//    public class PhysicsSystem
//    {
//        // Global gravity value (can be positive or negative)
//        public float Gravity { get; set; } = 0.5f;

//        // Apply physics to objects that have physics enabled
//        //public void Apply(List<GameObject> objects)
//        //{
//        //    foreach (var obj in objects.OfType<IPhysicsObject>().Where(o => o.HasPhysics))
//        //    {
//        //        if (obj is IMovable movable)
//        //        {
//        //                // Use the object custom gravity if set, otherwise use the global gravity
//        //                float appliedGravity = obj.CustomGravity ?? Gravity;

//        //                // Update the velocity of the object by applying gravity.
//        //                // Note: This is simple Euler integration and illustrates the physics update responsibility.
//        //                movable.Velocity = new PointF(
//        //                    movable.Velocity.X, // Horizontal velocity remains unchanged
//        //                    movable.Velocity.Y + appliedGravity // Vertical velocity changes based on gravity
//        //                );

//        //                if (obj is GameObject gameObject)
//        //                {
//        //                    // Update the position of the object based on its (now-updated) velocity
//        //                    gameObject.Position = new PointF(
//        //                        gameObject.Position.X + movable.Velocity.X,
//        //                        gameObject.Position.Y + movable.Velocity.Y
//        //                    );
//        //                }
//        //        }
//        //    }
//        //}
//        public void Apply(List<GameObject> objects)
//        {
//            // Hill Climb Racing uses custom gravity
//            // Physics handled inside Player only
//        }
//    }
//}