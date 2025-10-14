using Providers;
using UI;

namespace Core.GameStates
{
    public class GameStateController : IGameStateController
    {
        private readonly IUiManager _uiManager;
        private readonly IBoardDataProvider _boardDataProvider;
        private IGameState _gameState;

        public GameStateController(IUiManager uiManager, IBoardDataProvider boardDataProvider)
        {
            _uiManager = uiManager;
            _boardDataProvider = boardDataProvider;
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
            _gameState?.End();
            var matchCardsState = new MatchCardsState(_uiManager, _boardDataProvider);
            matchCardsState.OnBackSelected += StartMainMenuState;
            _gameState = matchCardsState;
            _gameState.Start();
        }
    }
}