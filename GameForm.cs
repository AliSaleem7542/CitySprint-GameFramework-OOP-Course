using FirstDesktopApp;
using FirstDesktopApp.Extensions;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using static GameFrameWork.PowerUp;
using GameFrameWork;

namespace GameFrameWork
{
    public partial class GameForm : Form
    {
        public static GameForm Instance;

        enum GameState { Menu, CharacterSelect, Playing, GameOver }
        private GameState currentState = GameState.Menu;

        private Game game;
        private CollisionSystem collisionSystem;
        private System.Windows.Forms.Timer gameTimer;

        private Image bgImage, menuBg;
        private float bgX1, bgX2;
        private float bgSpeed = 2f;

        private const int TRACK_HEIGHT = 90;
        private int GROUND_Y;

        private Player player;

        private int score = 0;
        private int coins = 0;
        private int spawnCounter = 0;
        private bool paused = false;
        private int highScore = 0;

        private SoundPlayer bgPlayer;
        private SoundPlayer gameOverPlayer;

        // MENU BUTTONS
        private Rectangle startBtn;
        private Rectangle exitBtn;
        private Rectangle maleBtn;
        private Rectangle femaleBtn;
        private bool hoverStart = false;
        private bool hoverExit = false;
        private bool hoverMale = false;
        private bool hoverFemale = false;

        private CharacterType selectedCharacter = CharacterType.Male;

        public Player GetPlayer() => player;

        public GameForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Instance = this;

            InitSounds();
            InitMenu();
        }

        // ================= MENU =================
        private void InitMenu()
        {
            menuBg = FirstDesktopApp.Properties.Resources.menu_bg2;
            currentState = GameState.Menu;

            int btnWidth = 240;
            startBtn = new Rectangle(Width / 2 - btnWidth / 2, 240, btnWidth, 55);
            exitBtn = new Rectangle(Width / 2 - btnWidth / 2, 310, btnWidth, 55);

            Invalidate();
        }

        // ================= CHARACTER SELECT =================
        private void InitCharacterSelectMenu()
        {
            currentState = GameState.CharacterSelect;

            maleBtn = new Rectangle(Width / 2 - 220, Height / 2 - 100, 200, 200);
            femaleBtn = new Rectangle(Width / 2 + 20, Height / 2 - 100, 200, 200);

            Invalidate();
        }

        // ================= SOUND =================
        private void InitSounds()
        {
            bgPlayer = new SoundPlayer(FirstDesktopApp.Properties.Resources.bgmusic);
            gameOverPlayer = new SoundPlayer(FirstDesktopApp.Properties.Resources.gameover);
            bgPlayer.Load();
            gameOverPlayer.Load();
        }

        // ================= GAME INIT =================
        private void InitGame()
        {
            gameTimer?.Stop();
            bgPlayer.Stop();

            game = new Game();
            collisionSystem = new CollisionSystem();

            bgImage = FirstDesktopApp.Properties.Resources.bgfinall3;
            bgX1 = 0;
            bgX2 = bgImage.Width;

            GROUND_Y = Height - TRACK_HEIGHT;

            player = new Player(selectedCharacter)
            {
                Size = new SizeF(100, 110)
            };

            // Lane system
            player.GroundY = GROUND_Y - player.Size.Height;
            player.UpperLaneY = player.GroundY - 100;
            player.Position = new PointF(120, player.GroundY);
            player.MoveGroundLane();

            game.AddObject(player);

            score = 0;
            coins = 0;
            spawnCounter = 0;
            paused = false;

            LoadHighScore();
            bgPlayer.PlayLooping();

            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 16;
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            currentState = GameState.Playing;
        }

        // ================= GAME LOOP =================
        private void GameLoop(object sender, EventArgs e)
        {
            if (currentState != GameState.Playing) return;

            if (!paused)
            {
                score++;
                SpawnEnemies();
                SpawnCoins();
                SpawnPowerUps();
            }

            ScrollWorld();

            game.Update(new GameTime());
            collisionSystem.Check(game.Objects);
            game.Cleanup();
            ParticleManager.Update();

            if (!player.IsActive)
                GameOver();

            Invalidate();
        }

        // ================= SPAWN METHODS =================
        private void SpawnEnemies()
        {
            spawnCounter++;
            if (spawnCounter < 90) return;
            spawnCounter = 0;

            int type = Random.Shared.Next(3);

            float groundY = player.GroundY + player.Size.Height - 90;
            float upperY = player.UpperLaneY + player.Size.Height - 90;
            float bikeUpperY = player.UpperLaneY - 10;
            float bikeGroundY = player.GroundY + player.Size.Height - 85;

            if (type == 0)
            {
                game.AddObject(new Enemy()
                {
                    Size = new SizeF(80, 100),
                    Position = new PointF(Width + 60, groundY),
                    Sprite = FirstDesktopApp.Properties.Resources.hurdlefinal
                });
            }
            else if (type == 1)
            {
                game.AddObject(new BirdEnemy()
                {
                    Size = new SizeF(80, 60),
                    Position = new PointF(Width + 60, upperY),
                    Sprite = FirstDesktopApp.Properties.Resources.hurdlefinal
                });
            }
            else
            {
                bool upperLane = Random.Shared.Next(2) == 0;
                float y = upperLane ? bikeUpperY : bikeGroundY;

                game.AddObject(new BikeEnemy()
                {
                    Size = new SizeF(100, 90),
                    Position = new PointF(Width + 100, y),
                    Sprite = FirstDesktopApp.Properties.Resources.bike
                });
            }
        }

        private void SpawnCoins()
        {
            if (Random.Shared.Next(120) != 1) return;

            bool upperLane = Random.Shared.Next(2) == 0;
            float y = upperLane ? player.UpperLaneY + 20 : player.GroundY - 20;

            game.AddObject(new Coin()
            {
                Size = new SizeF(40, 40),
                Position = new PointF(Width + 60, y),
                Sprite = FirstDesktopApp.Properties.Resources.coin
            });
        }

        private void SpawnPowerUps()
        {
            if (Random.Shared.Next(600) != 1) return;

            bool upperLane = Random.Shared.Next(2) == 0;
            float y = upperLane ? player.UpperLaneY - 30 : player.GroundY - 50;

            PowerUpType type = Random.Shared.Next(2) == 0 ? PowerUpType.Shield : PowerUpType.Magnet;
            Image sprite = type == PowerUpType.Shield
                ? FirstDesktopApp.Properties.Resources.powerUp
                : FirstDesktopApp.Properties.Resources.magnet;

            game.AddObject(new PowerUp()
            {
                Size = new SizeF(50, 50),
                Position = new PointF(Width + 60, y),
                Sprite = sprite,
                Type = type
            });
        }

        // ================= SCROLL =================
        private void ScrollWorld()
        {
            bgX1 -= bgSpeed;
            bgX2 -= bgSpeed;

            if (bgX1 <= -bgImage.Width) bgX1 = bgX2 + bgImage.Width;
            if (bgX2 <= -bgImage.Width) bgX2 = bgX1 + bgImage.Width;

            foreach (var obj in game.Objects)
                if (obj != player)
                    obj.Position = new PointF(obj.Position.X - 5f, obj.Position.Y);
        }

        // ================= GAME OVER =================
        private void GameOver()
        {
            currentState = GameState.GameOver;
            gameTimer.Stop();
            bgPlayer.Stop();
            gameOverPlayer.Play();
            SaveHighScore();
        }

        // ================= INPUT =================
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (currentState == GameState.Playing)
            {
                if (e.KeyCode == Keys.Up) player.MoveUpperLane();
                if (e.KeyCode == Keys.Down) player.MoveGroundLane();
                if (e.KeyCode == Keys.Space) player.Jump();
                if (e.KeyCode == Keys.P) paused = !paused;
            }

            if (currentState == GameState.GameOver && e.KeyCode == Keys.R)
                InitGame();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (currentState == GameState.Menu)
            {
                hoverStart = startBtn.Contains(e.Location);
                hoverExit = exitBtn.Contains(e.Location);
                Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (currentState == GameState.Menu)
            {
                if (startBtn.Contains(e.Location))
                {
                    InitCharacterSelectMenu(); // go to character select
                }
                else if (exitBtn.Contains(e.Location))
                {
                    Application.Exit();
                }
            }
            else if (currentState == GameState.CharacterSelect)
            {
                int offsetY = -120; // same as OnPaint
                Rectangle maleRect = new Rectangle(maleBtn.X, maleBtn.Y + offsetY, maleBtn.Width, maleBtn.Height);
                Rectangle femaleRect = new Rectangle(femaleBtn.X, femaleBtn.Y + offsetY, femaleBtn.Width, femaleBtn.Height);

                if (maleRect.Contains(e.Location))
                {
                    selectedCharacter = CharacterType.Male;
                    InitGame();
                }
                else if (femaleRect.Contains(e.Location))
                {
                    selectedCharacter = CharacterType.Female;
                    InitGame();
                }
            }

            Invalidate(); // redraw to show hover effect

        }

        // ================= DRAW =================
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (currentState == GameState.Menu)
            {
                g.DrawImage(menuBg, 0, 0, Width, Height);
                Font titleFont = new Font("Arial", 30, FontStyle.Bold);
                Font btnFont = new Font("Arial", 18, FontStyle.Bold);
                StringFormat center = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

                g.DrawString("CITY SPRINT", titleFont, Brushes.Yellow, new Rectangle(0, 120, Width, 60), center);

                g.FillRectangle(hoverStart ? Brushes.Orange : Brushes.Black, startBtn);
                g.FillRectangle(hoverExit ? Brushes.Red : Brushes.Black, exitBtn);

                g.DrawString("START", btnFont, Brushes.White, startBtn, center);
                g.DrawString("EXIT", btnFont, Brushes.White, exitBtn, center);

                return;
            }

            if (currentState == GameState.CharacterSelect)
            {
                g.DrawImage(menuBg, 0, 0, Width, Height);
                Font titleFont = new Font("Arial", 28, FontStyle.Bold);
                Font nameFont = new Font("Arial", 20, FontStyle.Bold);
                StringFormat center = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near };

                // Title
                g.DrawString("SELECT YOUR CHARACTER", titleFont, Brushes.LawnGreen, Width / 2, 60, new StringFormat() { Alignment = StringAlignment.Center });

                // Move images slightly up
                int offsetY = -120; // move up by 40 pixels
                Rectangle maleRect = new Rectangle(maleBtn.X, maleBtn.Y + offsetY, maleBtn.Width, maleBtn.Height);
                Rectangle femaleRect = new Rectangle(femaleBtn.X, femaleBtn.Y + offsetY, femaleBtn.Width, femaleBtn.Height);

                // Draw male image
                g.DrawImage(FirstDesktopApp.Properties.Resources.maleChar2, maleRect);
                // Draw border around male
                using (Pen border = new Pen(hoverMale ? Color.Gold : Color.GreenYellow, 4))
                {
                    g.DrawRectangle(border, maleRect);
                }
                // Draw name below male
                g.DrawString("Ali", nameFont, Brushes.GreenYellow, maleRect.X + maleRect.Width / 2, maleRect.Bottom + 5, center);

                // Draw female image
                g.DrawImage(FirstDesktopApp.Properties.Resources.femaleChar2, femaleRect);
                // Draw border around female
                using (Pen border = new Pen(hoverMale ? Color.Gold : Color.GreenYellow, 4))
                {
                    g.DrawRectangle(border, femaleRect);
                }
                // Draw name below female
                g.DrawString("Fatima", nameFont, Brushes.GreenYellow, femaleRect.X + femaleRect.Width / 2, femaleRect.Bottom + 5, center);

                return;
            }

            // DRAW GAME
            g.DrawImage(bgImage, bgX1, 0, bgImage.Width, Height);
            g.DrawImage(bgImage, bgX2, 0, bgImage.Width, Height);

            game.Draw(g);
            ParticleManager.Draw(g);

            Font font = new Font("Arial", 16, FontStyle.Bold);
            g.DrawString($"Score: {score}", font, Brushes.White, 20, 20);
            g.DrawString($"Coins: {coins}", font, Brushes.Gold, 20, 50);
            g.DrawString($"High Score: {highScore}", font, Brushes.White, 20, 80);

            if (paused)
                g.DrawString("PAUSED", new Font("Arial", 32, FontStyle.Bold), Brushes.Yellow, Width / 2 - 90, Height / 2);

            if (currentState == GameState.GameOver)
                g.DrawString("GAME OVER\nPress R to Restart", new Font("Arial", 32, FontStyle.Bold), Brushes.Red, Width / 2 - 160, Height / 2 - 80);
        }

        // ================= SAVE =================
        private void LoadHighScore()
        {
            if (System.IO.File.Exists("highscore.txt"))
                highScore = int.Parse(System.IO.File.ReadAllText("highscore.txt"));
        }

        private void SaveHighScore()
        {
            if (score > highScore)
                System.IO.File.WriteAllText("highscore.txt", score.ToString());
        }

        public void AddCoin() => coins++;
    }
}
