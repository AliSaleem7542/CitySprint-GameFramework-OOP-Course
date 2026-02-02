using GameFrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameWork
{
    public class Hurdle
    {
        public RectangleF Bounds;
        public Image Sprite;
        public bool Active = true;

        public Hurdle(Image img)
        {
            Sprite = img;
            Bounds = new RectangleF(
                new Random().Next(150, 850),
                -120,
                80,
                80
            );
        }

        public void Update()
        {
            Bounds.Y += 8;

            if (Bounds.Y > 650)
                Active = false;
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(Sprite, Bounds);
        }
    }
    //public class Hurdle : GameObject
    //{
    //    float speed = 7f;

    //    public override void Update(GameTime gameTime)
    //    {
    //        Position.Y += speed;

    //        if (Position.Y > 600)
    //            IsActive = false;
    //    }

    //    public override void Draw(Graphics g)
    //    {
    //        g.DrawImage(Sprite, Bounds);
    //    }
    //}
}

