using System;
using Providers;
using UI;
using UI.MatchCards;

namespace Core.GameStates
{
    public class MatchCardsState : IGameState
    {
        private readonly IUiManager _uiManager;
        private readonly IBoardConfigProvider _boardConfigProvider;
        private readonly ISpriteProvider _spriteProvider;
        private MatchCardsController _matchCardsController;
        
        public Action OnBackSelected;
        
        public MatchCardsState(
            IUiManager uiManager,
            IBoardConfigProvider boardConfigProvider,
            ISpriteProvider spriteProvider)
        {
            _uiManager = uiManager;
            _boardConfigProvider = boardConfigProvider;
            _spriteProvider = spriteProvider;
        }

        public void Start()
        {
            var data = _boardConfigProvider.GetBoardConfig(0);
            int columns = data.Columns;
            int rows = data.Rows;
            var mapping = data.GetMapping();
            
            _matchCardsController = _uiManager.ShowView<MatchCardsController>(ViewType.MatchCards);
            _matchCardsController.BoardController.Initialize(columns, rows, mapping, _spriteProvider);
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