using System.Drawing;

namespace GameFrameWork
{
    public class Enemy : GameObject
    {
        private const float MOVE_SPEED = 5f;
        public override void Update(GameTime gameTime)
        {
            Position = new PointF(Position.X - 6, Position.Y);

            if (Position.X < -100)
                IsActive = false;
        }


        public override void Draw(Graphics g)
        {
            if (Sprite != null)
                g.DrawImage(Sprite, Bounds);
            else
                g.FillRectangle(Brushes.DarkRed, Bounds);
        }
    }
}
