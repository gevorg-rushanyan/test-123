using UI;
using UI.MainMenu;

namespace Core.GameStates
{
    public class MainMenuState : IGameState
    {
        private readonly IUiManager _uiManager;
        private MainMenuController _mainMenuController;
        
        public MainMenuState(IUiManager uiManager)
        {
            _uiManager = uiManager;
        }

        public void Start()
        {
            _mainMenuController = _uiManager.ShowView<MainMenuController>(ViewType.MainMenu);
        }

        public void End()
        {
            
        }
    }
}