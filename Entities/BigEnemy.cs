using System.Drawing;

namespace GameFrameWork
{
    public class BigEnemy : GameObject
    {
        public Track Track;
        private float speed = 4f;

        public override void Update(GameTime gameTime)
        {
            Position = new PointF(Position.X - speed, Track.SurfaceY(Size.Height));
            //GameForm.Instance.ShakeScreen();

            if (Position.X < -300)
                IsActive = false;
        }
    }
}






//using FirstDesktopApp.Extensions;
//using System;
//using System.Drawing;

//namespace GameFrameWork
//{
//    public class BigEnemy : GameObject
//    {
//        private float speed = 4f;
//        private int health = 5; // Takes 5 hits to destroy
//        private bool movingRight = true;

//        public BigEnemy()
//        {
//            Size = new SizeF(120, 120);
//        }

//        public override void Update(GameTime gameTime)
//        {
//            // Horizontal movement
//            if (movingRight)
//                Position = new PointF(Position.X + speed, Position.Y);
//            else
//                Position = new PointF(Position.X - speed, Position.Y);

//            // Reverse direction at bounds
//            if (Position.X <= 50)
//                movingRight = true;
//            if (Position.X + Size.Width >= 1000) // screen width approx
//                movingRight = false;

//            // Off screen check
//            if (Position.Y > 800 || Position.X < -200 || Position.X > 1200)
//                IsActive = false;
//        }

//        public override void Draw(Graphics g)
//        {
//            if (Sprite != null)
//                g.DrawImage(Sprite, Bounds);
//            else
//                g.FillRectangle(Brushes.DarkRed, Bounds);
//        }

//        public override void OnCollision(GameObject other)
//        {
//            if (other is Player)
//            {
//                // Player dies
//                other.IsActive = false;
//                IsActive = false;
//            }
//            else if (other is Coin)
//            {
//                // Coins pass through
//            }
//            else
//            {
//                // Hit reduces health
//                health--;
//                if (health <= 0)
//                {
//                    IsActive = false;
//                    ParticleManager.SpawnHitEffect(
//                        new PointF(Position.X + Size.Width / 2, Position.Y + Size.Height / 2)
//                    );
//                }
//            }
//        }
//    }
//}
