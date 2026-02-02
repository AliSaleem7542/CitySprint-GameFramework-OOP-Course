using FirstDesktopApp.Extensions;
using System;
using System.Drawing;

namespace GameFrameWork
{
    public class ZigZagEnemy : GameObject
    {
        private float speedX = 4f;
        private float speedY = 3f;
        private bool movingDown = true;

        public ZigZagEnemy()
        {
            Size = new SizeF(80, 80);
        }

        public override void Update(GameTime gameTime)
        {
            // Horizontal left movement
            Position = new PointF(Position.X - speedX, Position.Y);

            // Vertical zigzag
            if (movingDown)
                Position = new PointF(Position.X, Position.Y + speedY);
            else
                Position = new PointF(Position.X, Position.Y - speedY);

            if (Position.Y >= 400) movingDown = false;
            if (Position.Y <= 300) movingDown = true;

            // Off screen
            if (Position.X + Size.Width < 0)
                IsActive = false;
        }

        public override void Draw(Graphics g)
        {
            if (Sprite != null)
                g.DrawImage(Sprite, Bounds);
            else
                g.FillRectangle(Brushes.Purple, Bounds);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                // Player dies
                other.IsActive = false;
                IsActive = false;

                ParticleManager.SpawnHitEffect(
                    new PointF(Position.X + Size.Width / 2, Position.Y + Size.Height / 2)
                );
            }
        }
    }
}
