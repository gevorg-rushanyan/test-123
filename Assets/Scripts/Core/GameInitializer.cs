using Core.GameStates;
using Providers;
using UI;
using UnityEngine;

namespace Core
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private UiManager _uiManager;
        private GameStateController _gameStateController;
        private IViewProvider _viewProvider;
        
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _uiManager.SetLoadingState(true);
            
            _viewProvider = new ViewProvider();
            _viewProvider.Initialize();
            _uiManager.Initialize(_viewProvider);
            
            _gameStateController = new GameStateController(_uiManager);
            _gameStateController.SetState(GameState.MainMenu);
            
            _uiManager.SetLoadingState(false);
        }
    }
}
