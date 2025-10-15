using System;
using System.Collections.Generic;
using Board;
using UnityEngine;

namespace Core.Progress
{
    [Serializable]
    public class BoardProgress
    {
        public int Columns = 0;
        public int Rows = 0;
        public Dictionary<string, ItemType> Items = new ();
    }

    [Serializable]
    public class PlayerProgress
    {
        public int Level { get; set; }
        public int Score { get; set; }
        public BoardProgress Board { get; set; } = new ();
    }
}