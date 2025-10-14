using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    [CreateAssetMenu(fileName = "BoardData", menuName = "ScriptableObjects/BoardData")]
    public class BoardData : ScriptableObject
    {
        [SerializeField] [Min(2)] private int _columns = 2;
        [SerializeField] [Min(2)] private int _rows = 2;
        [SerializeField] private List<BoardItemData> _items;
        
        public int Columns => _columns;
        public int Rows => _rows;
        public IReadOnlyList<BoardItemData> Items => _items;
    }
}