using System;
using UnityEngine;

namespace Board
{
    [Serializable]
    public class BoardItemData
    {
        [SerializeField] private Vector2Int _position;
        [SerializeField] private ItemType _type;
        
        public Vector2Int Position => _position;

        public ItemType Type
        {
            get => _type; 
            set => _type = value;
        }
    }
}