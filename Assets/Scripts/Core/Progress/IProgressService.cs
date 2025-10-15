using System.Collections.Generic;
using UnityEngine;

namespace Core.Progress
{
    public interface IProgressService
    {
        int Level { get; }
        int Score { get; }
        int Turns { get; }
        int Matches { get; }
        IReadOnlyList<Vector2Int> MatchItems { get; }
        
        void Initialize();
        void AddMatchItems(List<Vector2Int> matches);
        void UpdateTurnsAndMatches(int turnsDelta, int matchesDelta);
        void LevelPassed();
        void ResetProgress();
        void SaveProgress();
    }
}