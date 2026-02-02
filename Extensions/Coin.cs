using FirstDesktopApp.Extensions;
using System.Drawing;

namespace GameFrameWork
{
    public class Coin : GameObject
    {
        private const float SPEED = 5f;

        public override void Update(GameTime gameTime)
        {
            Player player = GameForm.Instance?.GetPlayer();

            if (player != null && player.MagnetActive)
            {
                float dx = player.Position.X - Position.X;
                float dy = player.Position.Y - Position.Y;
                float dist = (float)System.Math.Sqrt(dx * dx + dy * dy);

                if (dist < player.MagnetRange)
                {
                    Position = new PointF(
                        Position.X + dx * 0.12f,
                        Position.Y + dy * 0.12f
                    );
                    return;
                }
            }

            Position = new PointF(Position.X - SPEED, Position.Y);

            if (Position.X + Size.Width < 0)
                IsActive = false;
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                ParticleManager.SpawnCoinEffect(
                    new PointF(Position.X + Size.Width / 2, Position.Y + Size.Height / 2)
                );

                GameForm.Instance?.AddCoin();
                IsActive = false;
            }
        }
    }
}



//using FirstDesktopApp.Extensions;
//using System.Drawing;

//namespace GameFrameWork
//{
//    public class Coin : GameObject
//    {
//        private const float SPEED = 5f;

//        public override void Update(GameTime gameTime)
//        {
//            Position = new PointF(Position.X - SPEED, Position.Y);

//            if (Position.X + Size.Width < 0)
//                IsActive = false;
//        }

//        public override void OnCollision(GameObject other)
//        {
//            if (other is Player)
//            {
//                ParticleManager.SpawnCoinEffect(
//                    new PointF(Position.X + Size.Width / 2, Position.Y + Size.Height / 2)
//                );

//                GameForm.Instance?.AddCoin();
//                IsActive = false;
//            }
//        }
//    }
//}

//using FirstDesktopApp.Extensions;
//using GameFrameWork;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Media;
//using System.Security.Policy;
//using System.Text;
//using System.Threading.Tasks;


//namespace GameFrameWork
//    {
//        public class Coin : GameObject
//        {
//            private const float SPEED = 5f;


//            public override void Update(GameTime gameTime)
//            {
//                // Move left
//                Position = new PointF(Position.X - SPEED, Position.Y);

//                // Destroy if out of screen
//                if (Position.X + Size.Width < 0)
//                    IsActive = false;
//            }

//            public override void OnCollision(GameObject other)
//            {
//                if (other is Player)
//                {
//                    // ✨ Coin particle effect (centered)
//                    ParticleManager.SpawnCoinEffect(
//                        new PointF(
//                            Position.X + Size.Width / 2,
//                            Position.Y + Size.Height / 2
//                        )
//                    );

//                    // 💰 Increase coin count
//                    GameForm.Instance.AddCoin();

//                    // Remove coin
//                    IsActive = false;
//                }
//            }
//        }
//    }


