using System.Collections.Generic;
using Board;
using UnityEngine;

namespace Core.Progress
{
    public interface IProgressService
    {
        int Level { get; set; }
        int Score { get; }
        int Columns { get; }
        int Rows { get; }
        
        void Initialize();
        void UpdateBoardData(int column, int row, Dictionary<Vector2Int, BoardItemData> boardItems);
        IReadOnlyDictionary<Vector2Int, BoardItemData> GetBoardItems();
        void SetBoardItems(Dictionary<Vector2Int, BoardItemData> boardItems);
    }
}