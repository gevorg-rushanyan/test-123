using System;
using UnityEngine;

namespace Board
{
    [Serializable]
    public struct BoardItemData
    {
        [SerializeField] private Vector2Int _position;
        [SerializeField] private ItemType _type;
        
        public Vector2Int Position
        {
            get => _position;
            set => _position = value;
        }

        public ItemType Type
        {
            get => _type;
            set => _type = value;
        }
    }
}