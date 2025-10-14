using Board;
using Containers;
using UnityEngine;

namespace Providers
{
    public class BoardDataProvider : IBoardDataProvider
    {
        private const string Path = "Containers/BoardsDataContainer";
        private BoardDataContainer _boardDataContainer;
        
        public void Initialize()
        {
            _boardDataContainer = Resources.Load<BoardDataContainer>(Path);
        }

        public BoardData GetBoardData(int index)
        {
            int configLength = _boardDataContainer.DataList.Count;
            int targetIndex = index;
            if (index >= configLength)
            {
                targetIndex = index % configLength;
            }
            
            return _boardDataContainer.DataList[targetIndex];
        }
    }
}