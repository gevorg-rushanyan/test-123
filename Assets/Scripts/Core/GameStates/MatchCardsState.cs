using System;
using UI;
using UI.MatchCards;

namespace Core.GameStates
{
    public class MatchCardsState : IGameState
    {
        private readonly IUiManager _uiManager;
        private MatchCardsController _matchCardsController;
        
        public Action OnBackSelected;
        
        public MatchCardsState(IUiManager uiManager)
        {
            _uiManager = uiManager;
        }

        public void Start()
        {
            _matchCardsController = _uiManager.ShowView<MatchCardsController>(ViewType.MatchCards);
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