using Core.Progress;
using Core.Sound;
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
        private readonly ISoundSystem _soundSystem;
        private IGameState _gameState;

        public GameStateController(
            IProgressService progressService,
            IUiManager uiManager,
            IBoardConfigProvider boardConfigProvider,
            ISpriteProvider spriteProvider,
            ISoundSystem soundSystem)
        {
            _progressService = progressService;
            _uiManager = uiManager;
            _boardConfigProvider = boardConfigProvider;
            _spriteProvider = spriteProvider;
            _soundSystem = soundSystem;
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
            var matchCardsState = new MatchCardsState(_progressService, _uiManager, _boardConfigProvider, _spriteProvider, _soundSystem);
            matchCardsState.OnBackSelected += StartMainMenuState;
            _gameState = matchCardsState;
            _gameState.Start();
        }
    }
}