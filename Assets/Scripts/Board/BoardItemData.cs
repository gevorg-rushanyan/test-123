using System;
using UnityEngine;

namespace Board
{
    [Serializable]
    public struct PositionInt : IEquatable<PositionInt>
    {
        [SerializeField] private int _x;
        [SerializeField] private int _y;
        public int X => _x;
        public int Y => _y;

        public PositionInt(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public bool Equals(PositionInt other)
        {
            return _x == other._x && _y == other._y;
        }

        public override bool Equals(object obj)
        {
            return obj is PositionInt other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_x, _y);
        }
    }
    
    [Serializable]
    public struct BoardItemData
    {
        [SerializeField] private PositionInt _position;
        [SerializeField] private ItemType _type;
        
        public PositionInt Position => _position;

        public ItemType Type
        {
            get => _type; 
            set => _type = value;
        }
    }
}