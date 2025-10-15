using System.Collections.Generic;
using Board;
using Containers;
using UnityEngine;

namespace Providers
{
    public class BoardConfigProvider : IBoardConfigProvider
    {
        private const string Path = "Containers/BoardConfigContainer";
        private BoardConfigContainer _boardConfigContainer;
        
        public IReadOnlyList<MatchCombo> MatchComboConfig => _boardConfigContainer.MatchCombo;
        
        public void Initialize()
        {
            _boardConfigContainer = Resources.Load<BoardConfigContainer>(Path);
        }

        public BoardData GetBoardConfig(int index)
        {
            int configLength = _boardConfigContainer.Configs.Count;
            int targetIndex = index;
            if (index >= configLength)
            {
                targetIndex = index % configLength;
            }
            
            return _boardConfigContainer.Configs[targetIndex];
        }
    }
}