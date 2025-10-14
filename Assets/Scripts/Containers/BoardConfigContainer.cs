using System.Collections.Generic;
using Board;
using UnityEngine;

namespace Containers
{
    [CreateAssetMenu(fileName = "BoardConfigContainer", menuName = "ScriptableObjects/BoardConfigContainer")]
    public class BoardConfigContainer : ScriptableObject
    {
        [SerializeField] private List<BoardData> _configs;
        
        public IReadOnlyList<BoardData> Configs => _configs;
    }
}