using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    [CreateAssetMenu(fileName = "BoardsList", menuName = "ScriptableObjects/BoardsList")]
    public class BoardsList : ScriptableObject
    {
        [SerializeField] private List<BoardData> _boardsData;
        
        public IReadOnlyList<BoardData> List => _boardsData;
    }
}