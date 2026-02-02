using EZInput;
using FirstDesktopApp.Extensions;
using System.Drawing;
using System.Media;
using System.Net.NetworkInformation;
using static GameFrameWork.PowerUp;
using GameFrameWork;

namespace GameFrameWork
{
    public class Player : GameObject
    {
        public float GroundY { get; set; }
        public float UpperLaneY { get; set; }

        private float targetY;
        private bool isUpperLane = false;

        // ===== BETTER JUMP =====
        private float velocityY = 0;
        private bool isJumping = false;

        private const float GRAVITY = 0.9f;     // slower fall
        private const float JUMP_FORCE = -24f;  // higher jump

        // ===== MAGNET =====
        public bool MagnetActive { get; private set; }
        private int magnetTimer = 0;
        public float MagnetRange => 220f;

        private SoundPlayer jumpPlayer;
        private SoundPlayer gameOverPlayer;

        private Image[] runFrames;
        private int frameIndex = 0;
        private int frameCounter = 0;

        private bool hasShield = false;
        private int powerUpTimer = 0;
        private int invincibleTimer = 0;
        private bool isDead = false;
        private CharacterType character;

        public Player(CharacterType type)
        {
            character = type;

            jumpPlayer = new SoundPlayer(FirstDesktopApp.Properties.Resources.jumpmusic);
            gameOverPlayer = new SoundPlayer(FirstDesktopApp.Properties.Resources.gameover);

            if (character == CharacterType.Male)
            {
                runFrames = new Image[]
                {
            FirstDesktopApp.Properties.Resources.runner,
            FirstDesktopApp.Properties.Resources.runner2,
            FirstDesktopApp.Properties.Resources.runner3,
            FirstDesktopApp.Properties.Resources.runner4
                };
            }
            else // FEMALE
            {
                runFrames = new Image[]
                {
            FirstDesktopApp.Properties.Resources.runner_f1,
            FirstDesktopApp.Properties.Resources.runner_f2,
            FirstDesktopApp.Properties.Resources.runner_f3,
             FirstDesktopApp.Properties.Resources.runner_f5,
              FirstDesktopApp.Properties.Resources.runner_f7,
             

                };
            }

            Sprite = runFrames[0];
        }




        //public Player()
        //{
        //    jumpPlayer = new SoundPlayer(FirstDesktopApp.Properties.Resources.jumpmusic);
        //    gameOverPlayer = new SoundPlayer(FirstDesktopApp.Properties.Resources.gameover);

        //    runFrames = new Image[]
        //    {
        //        FirstDesktopApp.Properties.Resources.runner,
        //        FirstDesktopApp.Properties.Resources.runner2,
        //        FirstDesktopApp.Properties.Resources.runner3,
        //        FirstDesktopApp.Properties.Resources.runner4
        //    };

        //    Sprite = runFrames[0];
        //}

        public override void Update(GameTime gameTime)
        {
            if (!IsActive || isDead) return;

            HandleLaneLerp();
            HandleJumpPhysics();
            AnimateRun();
            HandlePowerUpTimer();

            Position = new PointF(120, Position.Y);
        }

        private void HandleLaneLerp()
        {
            float lerpSpeed = 0.25f;
            Position = new PointF(
                Position.X,
                Position.Y + (targetY - Position.Y) * lerpSpeed
            );
        }

        public void MoveUpperLane()
        {
            if (isJumping) return;
            isUpperLane = true;
            targetY = UpperLaneY;
        }

        public void MoveGroundLane()
        {
            if (isJumping) return;
            isUpperLane = false;
            targetY = GroundY;
        }

        // ===== JUMP =====
        public void Jump()
        {
            if (isJumping) return;

            isJumping = true;
            velocityY = JUMP_FORCE;

            jumpPlayer.Stop();
            jumpPlayer.Play();
        }

        private void HandleJumpPhysics()
        {
            if (!isJumping) return;

            velocityY += GRAVITY;
            Position = new PointF(Position.X, Position.Y + velocityY);

            float baseY = isUpperLane ? UpperLaneY : GroundY;

            if (Position.Y >= baseY)
            {
                Position = new PointF(Position.X, baseY);
                isJumping = false;
                velocityY = 0;

                ParticleManager.SpawnJumpDust(
                    new PointF(Position.X + Size.Width / 2, Position.Y + Size.Height)
                );
            }
        }

        private void AnimateRun()
        {
            if (isJumping || isDead) return;

            frameCounter++;
            if (frameCounter >= 6)
            {
                frameIndex = (frameIndex + 1) % runFrames.Length;
                Sprite = runFrames[frameIndex];
                frameCounter = 0;
            }
        }

        private void HandlePowerUpTimer()
        {
            if (hasShield && --powerUpTimer <= 0)
                hasShield = false;

            if (MagnetActive && --magnetTimer <= 0)
                MagnetActive = false;

            if (invincibleTimer > 0)
                invincibleTimer--;
        }

        public void ActivatePowerUp(PowerUpType type)
        {
            if (type == PowerUpType.Shield)
            {
                hasShield = true;
                powerUpTimer = 300;
            }
            else if (type == PowerUpType.Magnet)
            {
                MagnetActive = true;
                magnetTimer = 400;
            }
        }

        public override void OnCollision(GameObject other)
        {
            if (invincibleTimer > 0) return;

            if (other is Enemy || other is BirdEnemy)
            {
                if (hasShield)
                {
                    hasShield = false;
                    invincibleTimer = 30;
                    return;
                }

                isDead = true;
                IsActive = false;
                gameOverPlayer.Play();
            }
        }
    }
}




//using EZInput;
//using FirstDesktopApp.Extensions;
//using System.Drawing;
//using System.Media;
//using static GameFrameWork.PowerUp;

//namespace GameFrameWork
//{
//    public class Player : GameObject
//    {
//        // ===== Lane System =====
//        public float GroundY { get; set; }
//        public float UpperLaneY { get; set; }

//        private float targetY;
//        private bool isUpperLane = false;

//        // ===== Jump System =====
//        private float velocityY = 0;
//        private bool isJumping = false;

//        private const float GRAVITY = 1.2f;
//        private const float JUMP_FORCE = -19f;

//        // ===== Sounds =====
//        private SoundPlayer jumpPlayer;
//        private SoundPlayer gameOverPlayer;

//        // ===== Animation =====
//        private Image[] runFrames;
//        private int frameIndex = 0;
//        private int frameCounter = 0;

//        // ===== PowerUps =====
//        private bool hasShield = false;
//        private int powerUpTimer = 0;
//        private int invincibleTimer = 0;
//        private bool isDead = false;

//        public Player()
//        {
//            jumpPlayer = new SoundPlayer(FirstDesktopApp.Properties.Resources.jumpmusic);
//            gameOverPlayer = new SoundPlayer(FirstDesktopApp.Properties.Resources.gameover);

//            runFrames = new Image[]
//            {
//                FirstDesktopApp.Properties.Resources.runner,
//                FirstDesktopApp.Properties.Resources.runner2,
//                FirstDesktopApp.Properties.Resources.runner3,
//                FirstDesktopApp.Properties.Resources.runner4
//            };

//            Sprite = runFrames[0];
//        }

//        // ================= UPDATE =================
//        public override void Update(GameTime gameTime)
//        {
//            if (!IsActive || isDead) return;

//            HandleLaneLerp();
//            HandleJumpPhysics();
//            AnimateRun();
//            HandlePowerUpTimer();

//            Position = new PointF(120, Position.Y); // runner style fixed X
//        }

//        // ================= LANE LERP =================
//        private void HandleLaneLerp()
//        {
//            float lerpSpeed = 0.2f;
//            Position = new PointF(
//                Position.X,
//                Position.Y + (targetY - Position.Y) * lerpSpeed
//            );
//        }

//        public void MoveUpperLane()
//        {
//            if (isJumping) return;
//            isUpperLane = true;
//            targetY = UpperLaneY;
//        }

//        public void MoveGroundLane()
//        {
//            if (isJumping) return;
//            isUpperLane = false;
//            targetY = GroundY;
//        }

//        // ================= JUMP =================
//        public void Jump()
//        {
//            if (isJumping) return;

//            isJumping = true;
//            velocityY = JUMP_FORCE;

//            jumpPlayer.Stop();
//            jumpPlayer.Play();
//        }

//        private void HandleJumpPhysics()
//        {
//            if (!isJumping) return;

//            velocityY += GRAVITY;
//            Position = new PointF(Position.X, Position.Y + velocityY);

//            float baseY = isUpperLane ? UpperLaneY : GroundY;

//            if (Position.Y >= baseY)
//            {
//                Position = new PointF(Position.X, baseY);
//                isJumping = false;
//                velocityY = 0;

//                ParticleManager.SpawnJumpDust(
//                    new PointF(Position.X + Size.Width / 2, Position.Y + Size.Height)
//                );
//            }
//        }

//        // ================= ANIMATION =================
//        private void AnimateRun()
//        {
//            if (isJumping || isDead) return;

//            frameCounter++;
//            if (frameCounter >= 6)
//            {
//                frameIndex = (frameIndex + 1) % runFrames.Length;
//                Sprite = runFrames[frameIndex];
//                frameCounter = 0;
//            }
//        }

//        // ================= POWER UPS =================
//        private void HandlePowerUpTimer()
//        {
//            if (hasShield)
//            {
//                powerUpTimer--;
//                if (powerUpTimer <= 0)
//                    hasShield = false;
//            }

//            if (invincibleTimer > 0)
//                invincibleTimer--;
//        }

//        public void ActivatePowerUp(PowerUpType type)
//        {
//            if (type == PowerUpType.Shield)
//            {
//                hasShield = true;
//                powerUpTimer = 300;
//            }
//        }

//        // ================= COLLISION =================
//        public override void OnCollision(GameObject other)
//        {
//            if (invincibleTimer > 0) return;

//            if (other is Enemy || other is BirdEnemy)
//            {
//                if (hasShield)
//                {
//                    hasShield = false;
//                    invincibleTimer = 30;

//                    ParticleManager.SpawnHitEffect(
//                        new PointF(Position.X + Size.Width / 2, Position.Y + Size.Height / 2)
//                    );
//                    return;
//                }

//                isDead = true;
//                IsActive = false;

//                ParticleManager.SpawnHitEffect(
//                    new PointF(Position.X + Size.Width / 2, Position.Y + Size.Height / 2)
//                );

//                gameOverPlayer.Stop();
//                gameOverPlayer.Play();
//            }
//        }
//    }
//}



//using EZInput;
//using FirstDesktopApp.Extensions;

//using System.Drawing;
//using System.Media;
//using static GameFrameWork.PowerUp;

//namespace GameFrameWork
//{
//    public class Player : GameObject
//    {  // ===== Jump System =====
//        private int jumpCount = 0;
//        private const int MAX_JUMPS = 2;
//        private bool isJumping = false;
//        private bool isDead = false;
//        private float jumpVelocity = 0f;

//        private const float GRAVITY = 1.2f;
//        private const float JUMP_FORCE = -19f;

//        public float GroundY { get; set; }

//        // ===== Sounds =====
//        private SoundPlayer jumpPlayer;
//        private SoundPlayer gameOverPlayer;

//        // ===== Animation =====
//        private Image[] runFrames;
//        private int frameIndex = 0;
//        private int frameCounter = 0;

//        // ===== PowerUps =====
//        private bool hasShield = false;
//        private int powerUpTimer = 0;
//        private int invincibleTimer = 0;

//        public Player()
//        {
//            jumpPlayer = new SoundPlayer(FirstDesktopApp.Properties.Resources.jumpmusic);
//            gameOverPlayer = new SoundPlayer(FirstDesktopApp.Properties.Resources.gameover);

//            runFrames = new Image[]
//            {
//                 FirstDesktopApp.Properties.Resources.runner,

//                FirstDesktopApp.Properties.Resources.runner2,
//                FirstDesktopApp.Properties.Resources.runner3,
//                FirstDesktopApp.Properties.Resources.runner4
//            };

//            Sprite = runFrames[0];
//        }

//        public override void Update(GameTime gameTime)
//        {
//            if (!IsActive || isDead) return;

//            HandleJump();
//            AnimateRun();
//            HandlePowerUpTimer();

//            // Runner style fixed X
//            Position = new PointF(120, Position.Y);
//        }

//        // ================= POWER UPS =================
//        private void HandlePowerUpTimer()
//        {
//            if (hasShield)
//            {
//                powerUpTimer--;
//                if (powerUpTimer <= 0)
//                    hasShield = false;
//            }

//            if (invincibleTimer > 0)
//                invincibleTimer--;
//        }

//        // ================= JUMP =================
//        private void HandleJump()
//        {
//            // Jump input (press once)
//            if ((Keyboard.IsKeyPressed(Key.Space) || Keyboard.IsKeyPressed(Key.UpArrow))
//                && jumpCount < MAX_JUMPS
//                && !Keyboard.IsKeyPressed(Key.Shift)) // anti-spam
//            {
//                jumpVelocity = JUMP_FORCE;
//                isJumping = true;
//                jumpCount++;

//                jumpPlayer.Stop();
//                jumpPlayer.Play();
//            }

//            if (isJumping)
//            {
//                jumpVelocity += GRAVITY;
//                Position = new PointF(Position.X, Position.Y + jumpVelocity);

//                // Ground touch
//                if (Position.Y >= GroundY)
//                {
//                    Position = new PointF(Position.X, GroundY);
//                    isJumping = false;
//                    jumpVelocity = 0;
//                    jumpCount = 0;

//                    // 🌪 Dust landing particles
//                    ParticleManager.SpawnJumpDust(
//                        new PointF(Position.X + Size.Width / 2, Position.Y + Size.Height)
//                    );
//                }
//            }
//        }

//        // ================= RUN ANIMATION =================
//        private void AnimateRun()
//        {
//            if (isJumping || isDead) return;

//            frameCounter++;
//            if (frameCounter >= 6)
//            {
//                frameIndex = (frameIndex + 1) % runFrames.Length;
//                Sprite = runFrames[frameIndex];
//                frameCounter = 0;
//            }
//        }

//        // ================= POWER UP ACTIVATE =================
//        public void ActivatePowerUp(PowerUpType type)
//        {
//            if (type == PowerUpType.Shield)
//            {
//                hasShield = true;
//                powerUpTimer = 300;
//            }
//        }

//        // ================= COLLISION =================
//        public override void OnCollision(GameObject other)
//        {
//            if (invincibleTimer > 0) return;

//            if (other is Enemy || other is BirdEnemy)
//            {
//                // 🛡 Shield hit
//                if (hasShield)
//                {
//                    hasShield = false;
//                    invincibleTimer = 30;

//                    // 💥 Shield hit particles
//                    ParticleManager.SpawnHitEffect(
//                        new PointF(Position.X + Size.Width / 2, Position.Y + Size.Height / 2)
//                    );
//                    return;
//                }

//                // 💀 Player dead
//                isDead = true;
//                IsActive = false;

//                // 💥 Death particles
//                ParticleManager.SpawnHitEffect(
//                    new PointF(Position.X + Size.Width / 2, Position.Y + Size.Height / 2)
//                );

//                gameOverPlayer.Stop();
//                gameOverPlayer.Play();
//            }
//        }
//    }
//}

