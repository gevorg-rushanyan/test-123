using System.Collections.Generic;
using Board;
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
        
        void Initialize();
        void InitializeProgress(int column, int row, Dictionary<Vector2Int, BoardItemData> boardItems);
        IReadOnlyDictionary<Vector2Int, BoardItemData> GetBoardItems();
        void SetBoardItems(Dictionary<Vector2Int, BoardItemData> boardItems);
        void UpdateBoardItemsType(List<Vector2Int> boardItems, ItemType targetType);
        void UpdateTurnsAndMatches(int turnsDelta, int matchesDelta);

        void SaveProgress();
    }
}