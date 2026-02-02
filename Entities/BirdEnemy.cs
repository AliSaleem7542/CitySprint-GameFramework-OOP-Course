using System.Drawing;

namespace GameFrameWork
{
    public class BirdEnemy : GameObject
    {
        private float speed = 7f;

        public BirdEnemy()
        {
            Velocity = PointF.Empty;
        }

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
                g.FillEllipse(Brushes.Gray, Bounds);
        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GameFrameWork
//{
//    public class BirdEnemy : GameObject
//    {
//        private float speed = 7f;

//        public BirdEnemy()
//        {
//            // Velocity GameForm ke scroll ke sath double apply na ho
//            Velocity = PointF.Empty;
//        }

//        public override void Update(GameTime gameTime)
//        {
//            // Sirf left move (GameForm already scroll kar raha)
//            Position = new PointF(Position.X - speed, Position.Y);

//            // Screen se bahar → remove
//            if (Position.X + Size.Width < 0)
//                IsActive = false;
//        }

//        public override void Draw(Graphics g)
//        {
//            if (Sprite != null)
//                g.DrawImage(Sprite, Bounds);
//            else
//                g.FillEllipse(Brushes.Gray, Bounds);
//        }
//    }
//}
