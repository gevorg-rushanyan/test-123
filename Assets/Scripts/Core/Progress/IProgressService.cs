using System.Collections.Generic;
using Board;
using UnityEngine;

namespace Core.Progress
{
    public interface IProgressService
    {
        int Level { get; }
        int Score { get; }
        int Turns { get; }
        int Matches { get; }
        IReadOnlyList<PositionInt> MatchItems { get; }
        
        void Initialize();
        void AddMatchItems(List<PositionInt> matches);
        void UpdateTurnsAndMatches(int turnsDelta, int matchesDelta, int scoreDelta);
        void LevelPassed();
        void ResetProgress();
        void SaveProgress();
    }
}