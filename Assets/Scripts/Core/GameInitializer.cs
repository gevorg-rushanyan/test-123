using Core.GameStates;
using Core.Progress;
using Core.Sound;
using Providers;
using UI;
using UnityEngine;

namespace Core
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private UiManager _uiManager;
        [SerializeField] private SoundSystem _soundSystem;
        private GameStateController _gameStateController;
        private IViewProvider _viewProvider;
        private IBoardConfigProvider _boardConfigProvider;
        private CommonResourceProvider _resourceProvider;
        private IProgressService _progressService;
        
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _uiManager.SetLoadingState(true);
            
            _viewProvider = new ViewProvider();
            _boardConfigProvider = new BoardConfigProvider();
            _resourceProvider = new CommonResourceProvider();
            _progressService = new ProgressService();
            
            _progressService.Initialize();
            _viewProvider.Initialize();
            _boardConfigProvider.Initialize();
            _resourceProvider.Initialize();
            _soundSystem.Initialize(_resourceProvider);
            _uiManager.Initialize(_viewProvider);
            
            _gameStateController = new GameStateController(_progressService, _uiManager, _boardConfigProvider, _resourceProvider, _soundSystem);
            _gameStateController.SetState(GameState.MainMenu);
            
            _uiManager.SetLoadingState(false);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                _progressService.SaveProgress();
            }
        }
    }
}
