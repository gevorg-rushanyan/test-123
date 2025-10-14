using System.Collections.Generic;
using Board;
using UnityEngine;

namespace Containers
{
    [CreateAssetMenu(fileName = "BoardDataContainer", menuName = "ScriptableObjects/BoardDataContainer")]
    public class BoardDataContainer : ScriptableObject
    {
        [SerializeField] private List<BoardData> _boardsData;
        
        public IReadOnlyList<BoardData> DataList => _boardsData;
    }
}