using System.Collections.Generic;
using UnityEngine;

namespace Core.Progress
{
    public interface IProgressService
    {
        int Level { get; set; }
        int Score { get; }
        int Turns { get; }
        int Matches { get; }
        int Columns { get; }
        int Rows { get; }
        IReadOnlyList<Vector2Int> MatchItems { get; }
        
        void Initialize();
        void InitializeProgress(int column, int row);
        void AddMatchItems(List<Vector2Int> matches);
        void UpdateTurnsAndMatches(int turnsDelta, int matchesDelta);
        void SaveProgress();
    }
}