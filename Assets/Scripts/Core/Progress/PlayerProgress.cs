using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Progress
{
    [Serializable]
    public class BoardProgress
    {
        public int Columns = 0;
        public int Rows = 0;
        public List<Vector2Int> MatchItems = new ();
    }

    [Serializable]
    public class PlayerProgress
    {
        public int Level { get; set; }
        public int Score { get; set; }
        public int Matches { get; set; }
        public int Turns { get; set; }
        public BoardProgress Board { get; set; } = new ();
    }
}