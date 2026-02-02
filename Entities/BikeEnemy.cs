using FirstDesktopApp.Extensions;
using System.Drawing;

namespace GameFrameWork
{
    public class BikeEnemy : GameObject
    {
        private float speed = 9f; // 🔥 faster than others

        public override void Update(GameTime gameTime)
        {
            Position = new PointF(Position.X - speed, Position.Y);

            if (Position.X + Size.Width < 0)
                IsActive = false;
        }

        public override void Draw(Graphics g)
        {
            if (Sprite != null)
                g.DrawImage(Sprite, Bounds);
            else
                g.FillRectangle(Brushes.Black, Bounds);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                other.IsActive = false;
                IsActive = false;

                ParticleManager.SpawnHitEffect(
                    new PointF(Position.X + Size.Width / 2, Position.Y + Size.Height / 2)
                );
            }
        }


    }
}
