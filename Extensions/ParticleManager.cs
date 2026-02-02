using System.Collections.Generic;
using System.Drawing;

namespace FirstDesktopApp.Extensions
{
    internal static class ParticleManager
    {
        private static readonly List<Particle> particles = new();

        public static void SpawnJumpDust(PointF pos)
        {
            for (int i = 0; i < 10; i++)
            {
                particles.Add(new Particle(pos,
                    new PointF(Random.Shared.Next(-4, 4), Random.Shared.Next(-6, -2)),
                    Color.SandyBrown, 6f, 25f));
            }
        }

        public static void SpawnCoinEffect(PointF pos)
        {
            for (int i = 0; i < 8; i++)
            {
                particles.Add(new Particle(pos,
                    new PointF(Random.Shared.Next(-5, 6), Random.Shared.Next(-5, 6)),
                    Color.Gold, 5f, 30f));
            }
        }

        public static void SpawnHitEffect(PointF pos)
        {
            for (int i = 0; i < 14; i++)
            {
                particles.Add(new Particle(pos,
                    new PointF(Random.Shared.Next(-6, 7), Random.Shared.Next(-6, 2)),
                    Color.Red, 7f, 35f));
            }
        }

        public static void Update()
        {
            for (int i = particles.Count - 1; i >= 0; i--)
            {
                particles[i].Update();
                if (particles[i].IsDead)
                    particles.RemoveAt(i);
            }
        }

        public static void Draw(Graphics g)
        {
            foreach (var p in particles)
                p.Draw(g);
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
//    internal static class ParticleManager
//    {
//        private static readonly List<Particle> particles = new();

//        // ================== JUMP DUST ==================
//        public static void SpawnJumpDust(PointF pos)
//        {
//            for (int i = 0; i < 10; i++)
//            {
//                particles.Add(new Particle(
//                    pos,
//                    new PointF(
//                        Random.Shared.Next(-4, 4),
//                        Random.Shared.Next(-6, -2)
//                    ),
//                    Color.SandyBrown,
//                    size: 6f,
//                    life: 25f
//                ));
//            }
//        }

//        // ================== COIN SPARKLE ==================
//        public static void SpawnCoinEffect(PointF pos)
//        {
//            for (int i = 0; i < 8; i++)
//            {
//                particles.Add(new Particle(
//                    pos,
//                    new PointF(
//                        Random.Shared.Next(-5, 6),
//                        Random.Shared.Next(-5, 6)
//                    ),
//                    Color.Gold,
//                    size: 5f,
//                    life: 30f
//                ));
//            }
//        }

//        // ================== HIT / DEATH EFFECT ==================
//        public static void SpawnHitEffect(PointF pos)
//        {
//            for (int i = 0; i < 14; i++)
//            {
//                particles.Add(new Particle(
//                    pos,
//                    new PointF(
//                        Random.Shared.Next(-6, 7),
//                        Random.Shared.Next(-6, 2)
//                    ),
//                    Color.Red,
//                    size: 7f,
//                    life: 35f
//                ));
//            }
//        }

//        // ================== UPDATE ==================
//        public static void Update()
//        {
//            for (int i = particles.Count - 1; i >= 0; i--)
//            {
//                particles[i].Update();

//                if (particles[i].IsDead)
//                    particles.RemoveAt(i);
//            }
//        }

//        // ================== DRAW ==================
//        public static void Draw(Graphics g)
//        {
//            foreach (var p in particles)
//                p.Draw(g);
//        }
//    }
//}
