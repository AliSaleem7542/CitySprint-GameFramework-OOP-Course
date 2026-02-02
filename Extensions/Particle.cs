using System.Drawing;

namespace FirstDesktopApp.Extensions
{
    internal class Particle
    {
        public PointF Position;
        public PointF Velocity;
        public float Life;
        public Color Color;
        public float Size;
        private float maxLife;

        public Particle(PointF pos, PointF vel, Color color, float size = 6f, float life = 30f)
        {
            Position = pos;
            Velocity = vel;
            Color = color;
            Size = size;
            Life = life;
            maxLife = life;
        }

        public bool IsDead => Life <= 0;

        public void Update()
        {
            Position = new PointF(Position.X + Velocity.X, Position.Y + Velocity.Y);
            Velocity = new PointF(Velocity.X * 0.96f, Velocity.Y + 0.25f);
            Life--;
        }

        public void Draw(Graphics g)
        {
            float alpha = Life / maxLife;
            int a = (int)(alpha * 255);
            if (a < 0) a = 0;

            using Brush b = new SolidBrush(Color.FromArgb(a, Color));
            g.FillEllipse(b, Position.X, Position.Y, Size, Size);
        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FirstDesktopApp.Extensions
//{
//    internal class Particle
//    {
//        public PointF Position;
//        public PointF Velocity;
//        public float Life;
//        public Color Color;
//        public float Size;

//        private float maxLife;

//        public Particle(PointF pos, PointF vel, Color color, float size = 6f, float life = 30f)
//        {
//            Position = pos;
//            Velocity = vel;
//            Color = color;
//            Size = size;
//            Life = life;
//            maxLife = life;
//        }

//        public bool IsDead => Life <= 0;

//        public void Update()
//        {
//            Position = new PointF(
//                Position.X + Velocity.X,
//                Position.Y + Velocity.Y
//            );

//            // Gravity + friction
//            Velocity = new PointF(
//                Velocity.X * 0.96f,
//                Velocity.Y + 0.25f
//            );

//            Life--;
//        }

//        public void Draw(Graphics g)
//        {
//            float alpha = Life / maxLife;
//            int a = (int)(alpha * 255);
//            if (a < 0) a = 0;

//            using Brush b = new SolidBrush(
//                Color.FromArgb(a, Color)
//            );

//            g.FillEllipse(
//                b,
//                Position.X,
//                Position.Y,
//                Size,
//                Size
//            );
//        }
//    }
//}
