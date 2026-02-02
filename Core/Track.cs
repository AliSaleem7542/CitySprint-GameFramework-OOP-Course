namespace GameFrameWork
{
    public class Track
    {
        public float Y;
        public float Height;

        public bool Active;

        // 🔥 FIXED FLAGS
        public bool HasGap;
        public bool IsCrumbling;

        public int LifeTime; // frames

        public Track(
            float y,
            float height,
            int lifeTime = -1,
            bool hasGap = false,
            bool isCrumbling = false)
        {
            Y = y;
            Height = height;
            LifeTime = lifeTime;

            HasGap = hasGap;
            IsCrumbling = isCrumbling;

            Active = true;
        }

        public void Update()
        {
            if (LifeTime > 0)
            {
                LifeTime--;
                if (LifeTime <= 0)
                    Active = false;
            }
        }

        // Player landing surface
        public float SurfaceY(float objHeight)
        {
            return Y - objHeight;
        }
    }
}



//using System.Drawing;

//namespace GameFrameWork
//{
//    public class Track
//    {
//        public float Y;
//        public float Height;
//        public bool IsMoving;
//        public bool IsCrumbling;
//        public bool HasGap;

//        private float moveDir = 1;

//        public Track(float y, float height, bool moving = false, bool crumble = false, bool gap = false)
//        {
//            Y = y;
//            Height = height;
//            IsMoving = moving;
//            IsCrumbling = crumble;
//            HasGap = gap;
//        }

//        public void Update()
//        {
//            if (IsMoving)
//            {
//                Y += moveDir * 0.5f;
//                if (Y < 200 || Y > 500)
//                    moveDir *= -1;
//            }
//        }

//        public float SurfaceY(float objHeight) => Y - objHeight;
//    }
//}
