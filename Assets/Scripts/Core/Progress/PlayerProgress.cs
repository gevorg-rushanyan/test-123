using System;
using System.Collections.Generic;
using Board;

namespace Core.Progress
{
    [Serializable]
    public class PlayerProgress
    {
        public int Level { get; set; }
        public int Score { get; set; }
        public int Matches { get; set; }
        public int Turns { get; set; }
        public List<PositionInt> MatchItems = new ();
    }
}