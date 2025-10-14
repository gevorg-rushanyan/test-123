using UI;

namespace Core.GameStates
{
    public class GameStateController : IGameStateController
    {
        private readonly IUiManager _uiManager;
        private IGameState _gameState;

        public GameStateController(IUiManager uiManager)
        {
            _uiManager = uiManager;
        }

        public void SetState(GameState state)
        {
            switch (state)
            {
                case GameState.MainMenu:
                    StartMainMenuState();
                    break;
                case GameState.MatchCards:
                    StartCardMatchState();
                    break;
            }
        }

        private void StartMainMenuState()
        {
            _gameState?.End();
            var mainMenuState = new MainMenuState(_uiManager);
            mainMenuState.OnPlaySelected += StartCardMatchState;
            _gameState = mainMenuState;
            _gameState.Start();
        }
        
        private void StartCardMatchState()
        {
        }
    }
}