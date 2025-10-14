using System;
using Providers;
using UI;
using UI.MatchCards;

namespace Core.GameStates
{
    public class MatchCardsState : IGameState
    {
        private readonly IUiManager _uiManager;
        private readonly IBoardDataProvider _boardDataProvider;
        private MatchCardsController _matchCardsController;
        
        public Action OnBackSelected;
        
        public MatchCardsState(IUiManager uiManager, IBoardDataProvider boardDataProvider)
        {
            _uiManager = uiManager;
            _boardDataProvider = boardDataProvider;
        }

        public void Start()
        {
            var data = _boardDataProvider.GetBoardData(0);
            int columns = data.Columns;
            int rows = data.Rows;
            
            _matchCardsController = _uiManager.ShowView<MatchCardsController>(ViewType.MatchCards);
            _matchCardsController.BoardController.Initialize(columns, rows);
            _matchCardsController.OnClickBack += OnClickBack;
        }

        public void End()
        {
            _matchCardsController.OnClickBack -= OnClickBack;
            _uiManager.HideView(_matchCardsController);
        }

        private void OnClickBack()
        {
            OnBackSelected?.Invoke();
        }
    }
}