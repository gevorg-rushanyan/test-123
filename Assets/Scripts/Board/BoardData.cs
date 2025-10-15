using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    [CreateAssetMenu(fileName = "BoardData", menuName = "ScriptableObjects/BoardData")]
    public class BoardData : ScriptableObject
    {
        [SerializeField] [Min(2)] private int _columns = 2;
        [SerializeField] [Min(2)] private int _rows = 2;
        [SerializeField] [Min(2)] private int _targetMatchCount = 2;
        [SerializeField] [Min(2)] private int _maxTurnCount = 2;
        [SerializeField] private List<BoardItemData> _items;
        
        public int Columns => _columns;
        public int Rows => _rows;
        public int TargetMatchCount => _targetMatchCount;
        public int MaxTurnCount => _maxTurnCount;
        public IReadOnlyList<BoardItemData> Items => _items;

        public Dictionary<Vector2Int, BoardItemData> GetMapping()
        {
            var map = new Dictionary<Vector2Int, BoardItemData>();
            if (_items == null || _items.Count == 0)
            {
                return map;
            }

            foreach (var item in _items)
            {
                map.Add(item.Position, item);
            }
            
            return map;
        }
    }
}