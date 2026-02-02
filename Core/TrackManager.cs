using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFrameWork
{
    public class TrackManager
    {
        public List<Track> Tracks = new();

        private int groundY;
        private int spawnTimer = 0;

        public TrackManager(int groundY)
        {
            this.groundY = groundY;

            // Permanent ground track (index 0)
            Tracks.Add(new Track(groundY, 90));
        }

        public void Update()
        {
            spawnTimer++;

            // Spawn upper track every few seconds
            if (spawnTimer > 300)
            {
                spawnTimer = 0;

                // Max 3 upper tracks at a time
                if (Tracks.Count(t => t != Ground) < 3)
                {
                    float y = groundY - Random.Shared.Next(140, 320);
                    Tracks.Add(new Track(y, 70, lifeTime: 600));
                }
            }

            foreach (var t in Tracks)
                t.Update();

            // Remove expired upper tracks (ground never removed)
            Tracks.RemoveAll(t => t != Ground && !t.Active);
        }

        // Always available ground
        public Track Ground => Tracks[0];

        // 🔥 FIXED METHOD (supports RandomUpperTrack(true))
        public Track RandomUpperTrack(bool bossOnly = false)
        {
            var upperTracks = Tracks.Where(t => t != Ground).ToList();

            if (upperTracks.Count == 0)
                return Ground;

            // Boss enemies prefer highest track
            if (bossOnly)
                return upperTracks.OrderBy(t => t.Y).First();

            return upperTracks[Random.Shared.Next(upperTracks.Count)];
        }
    }
}




//using System;
//using System.Collections.Generic;

//namespace GameFrameWork
//{
//    public class TrackManager
//    {
//        public List<Track> Tracks = new();

//        public TrackManager(int groundY)
//        {
//            Tracks.Add(new Track(groundY, 90));                     // ground
//            Tracks.Add(new Track(groundY - 140, 70, moving: true));
//            Tracks.Add(new Track(groundY - 260, 70, crumble: true));
//            Tracks.Add(new Track(groundY - 380, 70, gap: true));    // boss lane
//        }

//        public Track RandomTrack(bool bossOnly = false)
//        {
//            if (bossOnly)
//                return Tracks[^1];

//            return Tracks[Random.Shared.Next(Tracks.Count)];
//        }

//        public void Update()
//        {
//            foreach (var t in Tracks)
//                t.Update();
//        }
//    }
//}
