using System;
using System.Collections.Generic;
using Board;
using UnityEngine;

namespace Containers
{
    [Serializable]
    public struct MatchCombo
    {
        [SerializeField] [Min(1)] private int _matchCount;
        [SerializeField] [Min(1)] private int _multiplier;
        
        public int MatchCount => _matchCount;
        public int Multiplier => _multiplier;
    }

    [CreateAssetMenu(fileName = "BoardConfigContainer", menuName = "ScriptableObjects/BoardConfigContainer")]
    public class BoardConfigContainer : ScriptableObject
    {
        [SerializeField] private List<BoardData> _configs;

        [SerializeField] private List<MatchCombo> _matchCombo;
        
        public IReadOnlyList<BoardData> Configs => _configs;
        public IReadOnlyList<MatchCombo> MatchCombo => _matchCombo;
    }
}