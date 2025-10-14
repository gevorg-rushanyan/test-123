using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    [CreateAssetMenu(fileName = "BoardData", menuName = "ScriptableObjects/BoardData")]
    public class BoardData : ScriptableObject
    {
        [SerializeField] [Min(2)] private int _width = 2;
        [SerializeField] [Min(2)] private int _height = 2;
        [SerializeField] private List<BoardItemData> _items;
        
        public int Width => _width;
        public int Height => _height;
        public IReadOnlyList<BoardItemData> Items => _items;
    }
}