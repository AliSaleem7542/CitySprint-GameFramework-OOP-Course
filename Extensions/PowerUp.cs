using System.Drawing;

namespace GameFrameWork
{
    public class PowerUp : GameObject
    {
        public enum PowerUpType
        {
            Shield,
            Magnet
        }

        public PowerUpType Type;
        private int lifeTime = 600;

        public override void Update(GameTime gameTime)
        {
            lifeTime--;
            if (lifeTime <= 0)
                IsActive = false;
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player player)
            {
                player.ActivatePowerUp(Type);
                IsActive = false;
            }
        }
    }
}






//using System.Drawing;

//namespace GameFrameWork
//{
//    public class PowerUp : GameObject
//    {
//        public enum PowerUpType
//        {
//            Shield,
//            SpeedBoost
//        }

//        public PowerUpType Type;
//        private int lifeTime = 600;

//        public override void Update(GameTime gameTime)
//        {
//            lifeTime--;
//            if (lifeTime <= 0)
//                IsActive = false;
//        }

//        public override void OnCollision(GameObject other)
//        {
//            if (other is Player player)
//            {
//                player.ActivatePowerUp(Type);
//                IsActive = false;
//            }
//        }
//    }
//}

//using System.Drawing;

//namespace GameFrameWork
//{
//    public class PowerUp : GameObject
//    {
//        public enum PowerUpType
//        {
//            Shield,
//            SpeedBoost
//        }  
//        public PowerUpType Type;
//        private int lifeTime = 600; // screen par rehne ka time

//        public override void Update(GameTime gameTime)
//        {
//           lifeTime--;
//           if (lifeTime <= 0)
//           IsActive = false;
//        }

//        public override void OnCollision(GameObject other)
//        {
//           if (other is Player player)
//             {
//                player.ActivatePowerUp(Type);
//                IsActive = false;
//             }
//        }
//    }

//}

