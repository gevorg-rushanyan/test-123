using Core.Progress;
using Providers;
using UI;

namespace Core.GameStates
{
    public class GameStateController : IGameStateController
    {
        private readonly IProgressService _progressService;
        private readonly IUiManager _uiManager;
        private readonly IBoardConfigProvider _boardConfigProvider;
        private readonly ISpriteProvider _spriteProvider;
        private IGameState _gameState;

        public GameStateController(
            IProgressService progressService,
            IUiManager uiManager,
            IBoardConfigProvider boardConfigProvider,
            ISpriteProvider spriteProvider)
        {
            _progressService = progressService;
            _uiManager = uiManager;
            _boardConfigProvider = boardConfigProvider;
            _spriteProvider = spriteProvider;
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
            var matchCardsState = new MatchCardsState(_progressService, _uiManager, _boardConfigProvider, _spriteProvider);
            matchCardsState.OnBackSelected += StartMainMenuState;
            _gameState = matchCardsState;
            _gameState.Start();
        }
    }
}