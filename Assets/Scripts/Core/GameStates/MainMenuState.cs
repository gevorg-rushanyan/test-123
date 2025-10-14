using System;
using UI;
using UI.MainMenu;

namespace Core.GameStates
{
    public class MainMenuState : IGameState
    {
        private readonly IUiManager _uiManager;
        private MainMenuController _mainMenuController;
        
        public Action OnPlaySelected;
        
        public MainMenuState(IUiManager uiManager)
        {
            _uiManager = uiManager;
        }

        public void Start()
        {
            _mainMenuController = _uiManager.ShowView<MainMenuController>(ViewType.MainMenu);
            _mainMenuController.OnClickPlay += OnClickPlay;
        }

        public void End()
        {
            _mainMenuController.OnClickPlay -= OnClickPlay;
            _uiManager.HideView(_mainMenuController);
        }

        private void OnClickPlay()
        {
            OnPlaySelected?.Invoke();
        }
    }
}