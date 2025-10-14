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
        private IBoardConfigProvider _boardConfigProvider;
        private ICommonResourceProvider _commonResourceProvider;
        
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _uiManager.SetLoadingState(true);
            
            _viewProvider = new ViewProvider();
            _boardConfigProvider = new BoardConfigProvider();
            _commonResourceProvider = new CommonResourceProvider();
            
            _viewProvider.Initialize();
            _boardConfigProvider.Initialize();
            _commonResourceProvider.Initialize();
            _uiManager.Initialize(_viewProvider);
            
            _gameStateController = new GameStateController(_uiManager, _boardConfigProvider);
            _gameStateController.SetState(GameState.MainMenu);
            
            _uiManager.SetLoadingState(false);
        }
    }
}
